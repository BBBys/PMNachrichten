using MySqlConnector;
using System;
using System.Diagnostics;
//using Textauswertung;
//ing static cTextauswertung

namespace Borys.Nachrichten
{
  internal partial class NeuWoerter
  {
    /// <summary>
    /// Statistik Einzelwörter aus  DB
    /// </summary>
    /// <param name="strCon">DB Host, User, Password usw.</param>
    /// <param name="dBTabelle">Name der Wörter-Tabelle</param>
    /// <exception cref="Exception">Tabelle leer</exception>
    /// <exception cref="ArgumentException"></exception>
    /// <summary>
    /// neue Meldungen (Datum in den letzten TAGE Tagen) aus Tabelle Meldungen  lesen, 
    /// in Wörter zerlegen und Wörter in die Tabellen wörterneu<n> eintragen
    /// </summary>
    /// <param name="conMld">fertiger Connection-String für die Datenbank mit Host, User, Password</param>
    /// <param name="dBTDaten">Tabelle mit Meldungen</param>
    /// <param name="dBTWoerter">Tabelle für Wörter</param>
    /// <exception cref="Exception">keine Einträge mit Meldungen gefunden</exception>
    private static void WörterAusNeuenMld(
      string DBHOST,
      MySqlConnection con,
      string dbTMeldung,
      string dBTDaten,
      string dBTWoerter,
      MySqlCommand SQLMld,
      string kennung,
      uint länge)
    {
      const int NEUSTUNDEN = 
#if DEBUG
        24*5
#else
        MldHilfe.NEUISTSTUNDEN;
#endif
        ;
      TimeSpan tsTage = new TimeSpan(0, NEUSTUNDEN, 0, 0);
      DateTime anfang = DateTime.Now - tsTage;
      Console.WriteLine($"es werden nur die Meldungen der letzten {NEUSTUNDEN/24.0} Tage ausgewertet");
      Console.WriteLine($"ab\t{anfang} ");
      #region Tabelle löschen
      SQLMld.CommandText = $"TRUNCATE {dBTWoerter};";
      SQLMld.ExecuteNonQuery();
      Console.WriteLine($"{dBTWoerter} gelöscht - wird neu befüllt");
      #endregion
      SQLMld.CommandText =
        $@"SELECT datenid FROM {dbTMeldung}
        WHERE datum>'{anfang:yyyy-M-dd HH:mm:ss}' ORDER BY datum ASC";
      using (MySqlDataReader rMeld = SQLMld.ExecuteReader())
        if (rMeld.HasRows)
        {
          uint neuWort, nMld, nToken;
          (neuWort, nMld, nToken) = WörterAusMeldungen(
                  DBHOST, dBTDaten, dBTWoerter, rMeld,
                  länge);
          rMeld.Close();
        }
        else
        {
          rMeld.Close();
          Console.WriteLine("keine Einträge im Zeitraum ");
          Console.WriteLine($"in\t{dBTDaten.ToUpper()}");
          Console.WriteLine($"mit\t{SQLMld.CommandText}");
          Console.WriteLine("---vielleicht neue Meldungen einlesen");
        }
      return;
    }

    private static (uint, uint, uint) WörterAusMeldungen(
      string DBHOST,
      string dbTDaten,
      string dBTWoerter,
      MySqlDataReader rMeld, uint länge)
    {
      string text, txtOriginal;
      uint neuWort = 0, nMld = 0, nToken = 0;
      Stopwatch timer = Stopwatch.StartNew();
      using (MySqlConnection conWrt = Hilfe.MySQLHilfe.ConnectToDB(DBHOST), 
        conDaten = Hilfe.MySQLHilfe.ConnectToDB(DBHOST))
      {
        conWrt.Open();
        conDaten.Open();
        using (
          MySqlCommand SQLWrt = new MySqlCommand(string.Empty, conWrt), 
          SQLdaten = new MySqlCommand(string.Empty, conDaten))
          while (rMeld.Read())
          {
            MySqlDataReader rDaten;
            uint pNeu, pTok,datenid;
            nMld++;
            datenid = rMeld.GetUInt32("datenid");
            SQLdaten.CommandText = $"select stopfrei from {dbTDaten} where id={datenid}";
            rDaten = SQLdaten.ExecuteReader();
            if (!rDaten.Read())
            {
              Console.Error.WriteLine(
                $"für die Meldung mit DatenID {datenid} fehlt der Eintrag in in {dbTDaten}");
              throw new Exception("Eintrag fehlt");
            }
            txtOriginal = rDaten.GetString("stopfrei");rDaten.Close();
            text = cTextauswertung.Strukturieren(txtOriginal);
            (pNeu, pTok) = MldHilfe.WörterAusMld(SQLWrt, dBTWoerter, text, länge);
            neuWort += pNeu;
            nToken += pTok;
            if (nMld % 10 == 0)
            {
              Console.Write(".");
              if (nMld % 200 == 0)
                if (MldHilfe.Laufzeit(timer, nMld, nToken, neuWort))
                  break;
            }
          }//while eine Meldung
           //rMeld.Close();  kommt im aufrufenden Modul
        conWrt.Close();//vlt. überfl. wg. using
      }
      return (neuWort, nMld, nToken);
    }
  }
}