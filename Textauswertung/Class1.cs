using System.Collections.Generic;
using System.IO;
using System.Net;
namespace Borys.Nachrichten
{
  public static class cTextauswertung
  {
    public static readonly Dictionary<string, string>
      Ablauffolge = new Dictionary<string, string>
      { { "P3Wörter","P31WörterZählen"} ,{ "P31WörterZählen","P45WörterVergleichen"} };
    public static readonly string CREATEWICHTIGE =
          "CREATE TABLE `news`.`wichtige` (" +
      "`id` INT NULL DEFAULT NULL AUTO_INCREMENT , " +
      "`wort` TINYTEXT NULL DEFAULT NULL , " +
      "`faktor` DOUBLE NULL DEFAULT NULL , " +
      "PRIMARY KEY (`id`)) ENGINE = InnoDB COMMENT = 'wichtige Wortfolgen in den neuesten Meldungen'; ";
    public static readonly string CREATETASKS =
          "CREATE TABLE `tasks` (" +
      "`id` INT NULL DEFAULT NULL AUTO_INCREMENT , " +
      "`programm` TINYTEXT NULL DEFAULT NULL, " +
      "`aufgabe` TINYINT NOT NULL DEFAULT '0' , " +
      "PRIMARY KEY(`id`)) ENGINE = InnoDB COMMENT='nächster notwendiger Programmschritt'; ";
    public static readonly string CREATEMELDUNGEN =
          "CREATE TABLE `meldungen` (" +
      "`hash` INT(11) NOT NULL, `datum` timestamp NOT NULL DEFAULT current_timestamp()," +
      "`quelle` tinytext DEFAULT NULL COMMENT 'Quelle der Meldung', " +
      "`category` tinytext DEFAULT NULL, `titel` TINYTEXT NOT NULL, `meldung` text NOT NULL, " +
      "`link` tinytext DEFAULT NULL, `w1`  tinyint(1) NOT NULL DEFAULT 0 COMMENT 'Einzelwörter erfasst', " +
      //    "`w2`  tinyint(1) NOT NULL DEFAULT 0 COMMENT '2-Wörter erfasst', " +
      "`w3`  tinyint(1) NOT NULL DEFAULT 0 COMMENT '3-Wörter erfasst', " +
      //   "`w4`  tinyint(1) NOT NULL DEFAULT 0 COMMENT '4-Wörter erfasst', " +
      "`w5`  tinyint(1) NOT NULL DEFAULT 0 COMMENT '5-Wörter erfasst', " +
      @"`entr` FLOAT NOT NULL COMMENT 'Meldungsentropie', " +
      "buchstaben tinyint(3)  DEFAULT 0, " +
      "`woerter`  tinyint(3)  DEFAULT 0, " +
      "PRIMARY KEY (`hash`)) " +
      "ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci";
    public static readonly string CREATEWOERTERBASIS = "(" +
" `wort` tinytext DEFAULT NULL COMMENT 'Wort/Wortfolge'," +
" `hash` int (11) NOT NULL COMMENT 'Hash über Wortfolge'," +
" `anzahl` int (11) unsigned DEFAULT 0 COMMENT 'absolute Anzahl'," +
      "`gesamt` int (10) unsigned DEFAULT NULL COMMENT 'Gesamtzahl, auf die sich rel. H. bezieht'," +
" `rang` int (11) unsigned DEFAULT NULL COMMENT 'Rang nach Anzahl'," +
" `relh` double unsigned DEFAULT NULL COMMENT 'relative Häufigkeit'," +
" `entr` float unsigned DEFAULT NULL COMMENT 'Wort-Entropie'," +
     " `beitrag` double unsigned DEFAULT NULL COMMENT 'Beitrag zum mittl. Wortentropie (pro Wortkette)'," +
 " `zeichen` tinyint(3) unsigned DEFAULT NULL COMMENT 'Anz. Zeichen in Kette'," +
" PRIMARY KEY(`hash`)," +
" UNIQUE KEY `wort` (`wort`) USING HASH" +
") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE = utf8mb4_general_ci COMMENT='abs. Häufigkeit Wörter'";
    public static readonly string CREATE1WOERTER = "CREATE TABLE `woerter1` " + CREATEWOERTERBASIS;
    public static readonly string CREATE2WOERTER = "CREATE TABLE `woerter2` " + CREATEWOERTERBASIS;
    public static readonly string CREATE3WOERTER = "CREATE TABLE `woerter3` " + CREATEWOERTERBASIS;
    public static readonly string CREATE4WOERTER = "CREATE TABLE `woerter4` " + CREATEWOERTERBASIS;
    public static readonly string CREATE5WOERTER = "CREATE TABLE `woerter5` " + CREATEWOERTERBASIS;
    public static readonly string CREATE10WOERTER = "CREATE TABLE `woerter10` " + CREATEWOERTERBASIS;

    public static readonly char[] WortTrennzeichen =
      new char[] { ' ', '.', ',', ';', '+', ':', '-', '!', '?', '»', '«', '#', '"', '(', ')',
      '[', ']', '{', '}', '/', '–' };
    /// <summary>
    /// global
    /// </summary>
    public enum eWoInRSS
    {
      Aussen, Item, Text, Description
    };
    private static void MeldungSpeichern(List<string> lTexte, StreamWriter aus)
    {
      if (lTexte == null)
        return;
      if (aus == null)
        throw new System.ArgumentNullException(nameof(aus));

      foreach (string item in lTexte)
      {
        string meldung = "<meldung> " + item.Trim() + " </meldung>";
        TextSpeichern(meldung, aus);
      }
    }

    /// <summary>
    /// gespeicherte RSS-Server lesen 
    /// und als Stringliste ausgeben
    /// </summary>
    /// global
    /// <param name="EinStream"></param>
    /// <returns>Liste</returns>
    public static List<string>
        NachrichtenLesen(StreamReader EinStream)
    {
      List<string> lNachrichten = new List<string>();
      string sLine = "";
      while (sLine != null)
      {
        sLine = EinStream.ReadLine();
        lNachrichten.Add(sLine);
      }
      return lNachrichten;
    }
    /// <summary>
    /// komplette Antwort vom RSS-Server lesen und als Stringliste zurückgeben
    /// </summary>
    /// global
    /// <param name="EinStream"></param>
    /// <returns></returns>
    public static List<string> NachrichtenLesen(string sURL)
    {
      List<string> lNachrichten = new List<string>();
      WebRequest wrGETURL = WebRequest.Create(sURL);
      Stream objStream = null;
      try
      {
        objStream = wrGETURL.GetResponse().GetResponseStream();
        using (StreamReader objReader = new StreamReader(objStream))
        {
          string sLine = "";
          while (sLine != null)
          {
            sLine = objReader.ReadLine();
            lNachrichten.Add(sLine);
          }
        }
      }
      finally
      {
        objStream?.Dispose();
      }
      return lNachrichten;
    }

    /// <summary>
    /// RSS-Texte von Datei lesen und als Stringliste ausgeben
    /// </summary>
    /// global
    /// <param name="pfad"></param>
    /// <returns></returns>
#pragma warning disable IDE0051 // Nicht verwendete private Member entfernen
    private static List<string> DateiLesen(string pfad)
#pragma warning restore IDE0051 // Nicht verwendete private Member entfernen
    {
      List<string> lNachrichten = new List<string>();
      using (StreamReader objReader = new StreamReader(pfad))
      {
        string sLine;
        sLine = objReader.ReadLine();
        while (sLine != null)
        {
          lNachrichten.Add(sLine);
          sLine = objReader.ReadLine();
        }
      }

      return lNachrichten;
    }

    /// <summary>
    /// Wort-Typen-Objekte auf Datei schreiben
    /// </summary>
    /// <param name="items">die Liste</param>
    /// <param name="pfad"></param>
    /// <param name="append">true: anhängen</param>
    private static List<WortType> WTDateiLesen(List<WortType> items, string pfad)
    {
      if (items == null)
      {
        throw new System.ArgumentNullException(nameof(items));
      }

      using (StreamReader objWriter = new StreamReader(pfad))
      {
        //throw new Exception( "hier fehlt was");
      }

      return items;
    }

    /// <summary>
    /// Wort-Typen-Objekte in unterschiedliche Dateien schreiben
    /// </summary>
    /// <param name="items">die Liste</param>
    /// <param name="pfadFormat">Formatierung für Pfad mit Länge: zum Beispiel "/daten/wörter{0}.txt"</param>
    /// <param name="länge">zahl der Wörter je Type</param>
    /// <param name="append">true: anhängen</param>
    private static void WTDateiSchreiben(List<WortType> items, string pfadFormat, int länge, bool append)
    {
      if (items != null)
      {
        _ = string.Format(pfadFormat, länge);
        WTDateiSchreiben(items, pfadFormat, append);
      }
      return;
    }

    /// <summary>
    /// Wort-Typen-Objekte alle in eine Dateien schreiben
    /// </summary>
    /// <param name="items">die Liste</param>
    /// <param name="pfad">Pfad: zum Beispiel "/daten/wörter.txt"</param>
    /// <param name="append">true: anhängen</param>
    private static void WTDateiSchreiben(List<WortType> items, string pfad, bool append)
    {
      if (items != null)
      {
        using (StreamWriter objWriter = new StreamWriter(pfad, append))
        {
          foreach (WortType item in items)
          {
            objWriter.WriteLine(item.ToString());
          }
        }
      }

      return;
    }

    public static bool TextSpeichern(string typ, string s1, TextWriter aus)
    {
      if (aus == null)
      {
        throw new System.ArgumentNullException(nameof(aus));
      }
      aus.WriteLine(typ);
      aus.WriteLine(s1);
      aus.WriteLine("/" + typ);
      return true;
    }
    public static void TextSpeichern(string s1, TextWriter aus)
    {
      if (aus == null)
      {
        throw new System.ArgumentNullException(nameof(aus));
      }

      aus.WriteLine(s1);
    }

    /// <summary>
    /// erstztz Punkt, Komma usw. durch Tags
    /// </summary>
    /// <param name="meldung"> das Original</param>
    /// <returns>geänderte Meldung</returns>
    public static string Strukturieren(string meldung)
    {
#if true
      meldung = meldung.Replace("&amp;", " und ");
      meldung = meldung.Replace(".", " ");
      meldung = meldung.Replace(":", " ");
      meldung = meldung.Replace("!", " ");
      meldung = meldung.Replace("?", "  ");
      meldung = meldung.Replace(",", " ");
      meldung = meldung.Replace("–", " ");
      meldung = meldung.Replace("-", " ");
      meldung = meldung.Replace("-", " ");
      meldung = meldung.Replace(";", " ");
      meldung = meldung.Replace("(", " ");
      meldung = meldung.Replace(")", " ");
      meldung = meldung.Replace("[", " ");
      meldung = meldung.Replace("]", " ");
      meldung = meldung.Replace("\"", " ");
      meldung = meldung.Replace("»", " ");
      meldung = meldung.Replace("«", " ");
#else
    meldung = meldung.Replace("&amp;", " und ");
    meldung = meldung.Replace(".", " <satz> ");
    meldung = meldung.Replace(":", " <satz> ");
    meldung = meldung.Replace("!", " <satz> ");
    meldung = meldung.Replace("?", " <satz> ");
    meldung = meldung.Replace("<satz><satz>", " <satz> ");
    meldung = meldung.Replace(",", " <nebensatz> ");
    meldung = meldung.Replace(" - ", " <nebensatz> ");
    meldung = meldung.Replace("-", " ");
    meldung = meldung.Replace(";", " <nebensatz> ");
    meldung = meldung.Replace("(", " <einschub> ");
    meldung = meldung.Replace(")", " <einschub> ");
    meldung = meldung.Replace("[", " <einschub> ");
    meldung = meldung.Replace("]", " <einschub> ");
    meldung = meldung.Replace(" \"", " <zitat> ");
    meldung = meldung.Replace("\" ", " <zitat> ");
    meldung = meldung.Replace(" »", " <zitat> ");
    meldung = meldung.Replace("« ", " <zitat> ");
#endif
      meldung = meldung.Replace("  ", " ");
      meldung = meldung.Trim();
      return meldung.ToLower();
    }

    /// <summary>
    /// entfernt was aus woraus
    /// </summary>
    /// <param name="was"></param>
    /// <param name="woraus"></param>
    /// <returns></returns>
    private static string Entfernen(string was, string woraus)
    {
      if (string.IsNullOrEmpty(was))
      {
        return woraus;
      }

      if (string.IsNullOrWhiteSpace(woraus))
      {
        return "";
      }

      int p = woraus.IndexOf(was);
      int len = was.Length;
      while (p > -1)
      {
        woraus = woraus.Remove(p, len);
        p = woraus.IndexOf(was);
      }

      return woraus;
    }
  }
}