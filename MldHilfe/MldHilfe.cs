using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace Borys.Nachrichten
{
  public static class MldHilfe
  {
    //    private const int LIMIT =
    //#if DEBUG
    //      10;
    //#else
    //900;
    //#endif
    /// <summary>
    /// verwendung in
    ///  str = Convert.ToString(wert, cTextauswertung.DEZIMALPUNKT);
    /// </summary>
    public static readonly System.Globalization.CultureInfo DEZIMALPUNKT = new System.Globalization.CultureInfo("en-US");
    public static readonly uint LAUFZEITSEKUNDEN = 32 * 60;
    public  const int NEUISTSTUNDEN= 14*24;


    public static void Laufzeit(Stopwatch timer) => Laufzeit(timer, 0, 0, 0, 0);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="timer"></param>
    /// <param name="Limit"></param>
    /// <returns>true, wenn Abbruch</returns>
    public static bool Laufzeit(Stopwatch timer, uint Limit) => Laufzeit(timer, 0, 0, 0, Limit);
    /// <summary>
    /// gesamte und relative Laufzeit AUSGEBEN
    /// </summary>
    /// <param name="timer"></param>
    /// <param name="nMld">Anz Meld</param>
    /// <param name="nWörter">Anz Wörter</param>
    /// <param name="nNeu">Anz neue Wörter</param>
    public static bool Laufzeit(Stopwatch timer, uint nMld, uint nWörter, uint nNeu, uint Limit = 0)
    {
      double sec;
      string laufzeit;
      (laufzeit, sec) = LaufzeitFormat(timer);
      if (nMld > 0)
      {
        laufzeit = $"{laufzeit}\n{nMld} Meldungen, {sec / nMld:N2} s/Meldung";
      }

      if (nWörter > 0)
      {
        laufzeit = $"{laufzeit}\t{nWörter} Wörter, {sec / nWörter:N2} s/Wort";
      }

      if (nNeu > 0)
      {
        laufzeit = $"{laufzeit}\t{nNeu} neu";
      }

      Console.WriteLine(laufzeit);
      return Limit > 0 && sec > Limit;
    }

    /// <summary>
    /// Timer abfragen und Laufzeit formatieren - keine Ausgabe
    /// </summary>
    /// <param name="timer"></param>
    /// <returns>(formatierte Laufzeit, Anazl Sekunden</returns>
    public static (string, double) LaufzeitFormat(Stopwatch timer)
    {
      string gesamt;
      double sec;
      TimeSpan timespan = timer.Elapsed;
      sec = timespan.TotalMilliseconds * .001;
      gesamt = sec < 5
        ? $"{sec:N2} s" :
        sec < 110
        ? $"{sec,2:N1} s"
        : sec < 90 * 60 ? $"{timespan.Minutes,2:N0}:{timespan.Seconds,2:N0} min" : $":N0{timespan.Hours}:{timespan.Minutes,2:N0} Std";

      return (gesamt, sec);
    }


    public static void BearbeiteAlleWörter(
      string dBTabelle,
      uint länge,
      MySqlCommand cmdLesen,
      MySqlConnection con2, MySqlCommand cmdSchreiben, uint LIMIT)
    {
      uint anders, types, token, nTypes;
      double entr;
      MySqlDataReader reader;
      Stopwatch timer;
      con2.Open();
      #region Übersicht
      cmdLesen.CommandText = $"SELECT COUNT(wort) AS types, SUM(anzahl) AS token FROM {dBTabelle}";
      reader = cmdLesen.ExecuteReader();
      _ = reader.Read();
      types = reader.GetUInt32("types");
      token = reader.GetUInt32("token");
      //Console.WriteLine($"{types} Types mit {token} Token zu bearbeiten");
      reader.Close();
      cmdLesen.CommandText =
        $@"SELECT COUNT(wort) AS anders FROM {dBTabelle} 
        WHERE (NOT gesamt = {token}) OR gesamt IS NULL";
      reader = cmdLesen.ExecuteReader();
      _ = reader.Read();
      anders = reader.GetUInt32("anders");
      Console.WriteLine(
        $"{types} Types mit {token} Token enthalten, {anders} ({anders / (double)types:p0}) zu bearbeiten");
      reader.Close();
      #endregion
      #region Einzelwörter
      cmdLesen.CommandText =
        $"SELECT id,anzahl FROM {dBTabelle} WHERE (NOT gesamt = {token}) OR gesamt IS NULL";
      reader = cmdLesen.ExecuteReader();
      //if (reader.HasRows) wurde oben schon geprüft
      nTypes = 0;
      timer = Stopwatch.StartNew();
      while (reader.Read())
      {
        SetRelhEntr(dBTabelle, token, reader, cmdSchreiben);
        nTypes++;
        if (nTypes % 250 == 0)
        {
          Console.Write(".");
          if (nTypes % 5000 == 0)
          {
            if (Laufzeit(timer, 0, nTypes, 0, LIMIT))
            {
              break;
            }
          }
        }
      }
      reader.Close();
      #endregion
      cmdLesen.CommandText = 
        $"SELECT id,relh AS Häufigkeit,entr AS Entropie FROM {dBTabelle} WHERE gesamt = {token}";
      reader = cmdLesen.ExecuteReader();
      if (reader.HasRows)
      {
        string str;
        while (reader.Read())
        {

          double relh, beitrag;     
          long idWort;
          idWort = reader.GetInt64("id");
          relh = reader.GetDouble("Häufigkeit");
          entr = reader.GetDouble("Entropie");
          beitrag = relh * entr;
          str = Convert.ToString(beitrag, DEZIMALPUNKT);
          cmdSchreiben.CommandText = 
            $"UPDATE {dBTabelle} SET beitrag={str} WHERE id = {idWort}";
          _ = cmdSchreiben.ExecuteNonQuery();
        }
        reader.Close();
        cmdLesen.CommandText = $"SELECT sum(beitrag) AS Entropie, AVG(zeichen) AS mLänge FROM {dBTabelle} ";
        reader = cmdLesen.ExecuteReader();
        if (reader.HasRows)
        {
          double mLänge;
          _ = reader.Read();
          entr = reader.GetDouble("Entropie") / länge;
          mLänge = reader.GetDouble("mLänge");
          Console.WriteLine($"Entropie von {länge}-Wort-Ketten (mittl. Länge {mLänge:N2} Zeichen):\n\t{entr:N2} Bit/Wort\n\t{entr / (double)mLänge:N2} Bit/Zeichen");
        }
        reader.Close();
        cmdLesen.CommandText = 
          $@"SELECT wort AS Wort,anzahl AS Anzahl FROM {dBTabelle} 
          WHERE anzahl>1 ORDER BY anzahl DESC LIMIT 10";
        reader = cmdLesen.ExecuteReader();
        int i = 0, anz;
        if (reader.HasRows)
        {
          while (reader.Read())
          {
            str = reader.GetString("Wort");
            anz = reader.GetInt32("Anzahl");
            i++;
            Console.WriteLine($"{i,3}\t{anz,5}x {str}");
          }
        }
        else
        {
          Console.Error.WriteLine("keine Wörter auszugeben");
        }
      }
      else
      {
        Console.Error.WriteLine("Entropiebeiträge nicht zu berechnen");
      }
      //con2.Close();
    }
    /// <summary>
    /// nachdem der Eintrag in der Wörter-Tabelle gelesen wurde:
    /// rel. H und Entr berechnen ud in Tabelle beim Wortstring eintragen
    /// </summary>
    /// <param name="dBTabelle"></param>
    /// <param name="token"></param>
    /// <param name="reader">muss schon den Eintrag gelesen haben</param>
    /// <param name="cmd"></param>
    public static void SetRelhEntr(
      string dBTabelle,
      uint token,
      MySqlDataReader reader,
      MySqlCommand cmd)
    {
      uint anzahl;
      double relh, entr;
      string strRelh, strEntr;
      int idWort = reader.GetInt32("id");
      anzahl = reader.GetUInt32("Anzahl");
      relh = anzahl / (double)token;
      entr = -Math.Log(relh, 2);
      strRelh = Convert.ToString(relh, DEZIMALPUNKT);
      strEntr = Convert.ToString(entr, DEZIMALPUNKT);
      cmd.CommandText = 
        $"UPDATE {dBTabelle} SET relh={strRelh},entr={strEntr},gesamt={token} WHERE id={idWort}";
      _ = cmd.ExecuteNonQuery();
      return;
    }

    public static (uint, uint) WörterAusMld(MySqlCommand SQL, string dBTWoerter, string sentence, uint länge)
    {
      uint neuWort = 0, nToken = 0;
      List<string> sequences = TeileText(sentence, länge);
      Dictionary<string, int> sequenceCounts = new Dictionary<string, int>();
      foreach (string seq in sequences)
      {
        if (sequenceCounts.ContainsKey(seq))
          sequenceCounts[seq]++;
        else
          sequenceCounts[seq] = 1;
      }
      foreach (KeyValuePair<string, int> kvp in sequenceCounts)
      {
        int zeichen;
        int nNeu;
        string wort = kvp.Key;
        zeichen = wort.Length;
        nToken++;
        SQL.CommandText = $"SELECT anzahl FROM {dBTWoerter} WHERE (wort = '{kvp.Key}')";
        MySqlDataReader rWort = SQL.ExecuteReader();//da w2 Schlüssel ist kann es nur einen Record geben
        if (rWort.HasRows)
        {//bekanntes Wort
          _ = rWort.Read();
          nNeu = Convert.ToInt32(rWort["anzahl"]) + kvp.Value;
          rWort.Close();
          SQL.CommandText = $"UPDATE {dBTWoerter} SET anzahl = {nNeu} WHERE  (wort = '{kvp.Key}')";
        }
        else
        {//neues Wort          
          nNeu = kvp.Value;
          neuWort++;
          rWort.Close();
          SQL.CommandText =
            $@"INSERT INTO {dBTWoerter} 
            (wort,anzahl,zeichen) 
            VALUES ('{wort}','{nNeu}',{zeichen})";
        }
        try
        {
          _ = SQL.ExecuteNonQuery();
        }
        catch (MySqlException ex)
        {
          Console.Error.WriteLine($"MySQL-Exception #{ex.Number}");
          Console.Error.WriteLine(SQL.CommandText);
          Console.Error.WriteLine(ex.Message);
          throw ex;
        }
        catch (Exception ex)
        {
          Console.Error.WriteLine(ex.Message);
          throw ex;
        }
      }//foreach
      return (neuWort, nToken);
    }

    public static List<string> TeileText(string sentence, uint länge)
    {
      // Split the sentence into words
      string[] words = Regex.Split(sentence, @"\W+");

      // Create 3-word sequences
      List<string> sequences = new List<string>();
      switch (länge)
      {
        case 1:
          for (int i = 0; i < words.Length; i++)
          {
            sequences.Add($"{words[i]}");
          }

          break;
        case 2:
          for (int i = 0; i < words.Length - 1; i++)
          {
            sequences.Add($"{words[i]}  {words[i + 1]}");
          }

          break;
        case 3:
          for (int i = 0; i < words.Length - 2; i++)
          {
            sequences.Add($"{words[i]} {words[i + 1]} {words[i + 2]}");
          }

          break;
        case 4:
          for (int i = 0; i < words.Length - 3; i++)
          {
            sequences.Add($"{words[i]} {words[i + 1]} {words[i + 2]} {words[i + 3]}");
          }

          break;
        case 5:
          for (int i = 0; i < words.Length - 4; i++)
          {
            sequences.Add($"{words[i]} {words[i + 1]} {words[i + 2]} {words[i + 3]} {words[i + 4]}");
          }

          break;
        case 10:
          for (int i = 0; i < words.Length - 9; i++)
          {
            sequences.Add($"{words[i]} {words[i + 1]} {words[i + 2]} {words[i + 3]} {words[i + 4]} " +
              $"{words[i + 5]} {words[i + 6]} {words[i + 7]} {words[i + 8]} {words[i + 9]}");
          }

          break;
        default:
          throw new Exception($"für die Länge {länge} fehlt der Code!");
      }

      return sequences;
    }

    public static void LaufzeitAusgeben(int neuWort, int nMld, int nToken, double secLaufzeit)
    {
      string laufzeit = $"gesamt {secLaufzeit:N0} s, {secLaufzeit / nMld:N2} s/Meldungs, {secLaufzeit / nToken:N3} s/Token";
      Console.WriteLine($"{laufzeit}\n{nMld} Meldungen\t{nToken} Wörter\t{neuWort} neu");
    }

    public static (uint, uint, DateTime, DateTime) Abzählen(
      string dBTabelle,
      MySqlCommand SQL,
      string kriterium)
    {
      DateTime erste = DateTime.MinValue, letzte = DateTime.Now;
      MySqlDataReader rMeld;
      uint alles, offen;
      if (dBTabelle.Equals("meldungen"))
      {
        SQL.CommandText =
        $"SELECT min(datum) as Erste,max(datum) as Letzte FROM {dBTabelle} ";
        rMeld = SQL.ExecuteReader();
        _ = rMeld.Read();
        erste = rMeld.GetDateTime("Erste");
        letzte = rMeld.GetDateTime("Letzte");
        rMeld.Close();
      }
      SQL.CommandText =
        $"SELECT COUNT(*) AS Anzahl FROM {dBTabelle} ";
      rMeld = SQL.ExecuteReader();
      _ = rMeld.Read();
      alles = rMeld.GetUInt32("Anzahl");
      rMeld.Close();
      SQL.CommandText = $"SELECT COUNT(*) FROM {dBTabelle} WHERE NOT {kriterium} OR {kriterium} IS NULL";
      rMeld = SQL.ExecuteReader();
      _ = rMeld.Read();
      offen = rMeld.GetUInt32(0);
      rMeld.Close();
      return (alles, offen, erste, letzte);
    }

    public static void NullOrEmpty(string ConnectionString, string dBTMeldungen, string dBTWoerter, string kennung)
    {
      if (string.IsNullOrEmpty(ConnectionString))
      {
        throw new ArgumentException($"\"{nameof(ConnectionString)}\" kann nicht NULL oder leer sein.", nameof(ConnectionString));
      }
      if (string.IsNullOrEmpty(dBTMeldungen))
      {
        throw new ArgumentException($"\"{nameof(dBTMeldungen)}\" kann nicht NULL oder leer sein.", nameof(dBTMeldungen));
      }

      if (string.IsNullOrEmpty(dBTWoerter))
      {
        throw new ArgumentException($"\"{nameof(dBTWoerter)}\" kann nicht NULL oder leer sein.", nameof(dBTWoerter));
      }
      if (string.IsNullOrEmpty(kennung))
      {
        throw new ArgumentException($"\"{nameof(kennung)}\" kann nicht NULL oder leer sein.", nameof(kennung));
      }
    }
  }
}