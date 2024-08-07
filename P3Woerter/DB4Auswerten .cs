using MySqlConnector;
using System;
//using Textauswertung;
//using static cTextauswertung;

namespace Borys.Nachrichten
{
  internal partial class MainWörterHäufigkeiten
  {
    //private static void W4Statistik(string ConnectionString, string dBTabelle)
    //{
    //  NullOrEmtpyTest(ConnectionString, dBTabelle);
    //  double rfToken;
    //  using (MySqlConnection con = new MySqlConnection(ConnectionString))
    //  using (MySqlCommand cmd = new MySqlCommand("", con))
    //  using (MySqlConnection con2 = new MySqlConnection(ConnectionString))
    //  using (MySqlCommand cmd2 = new MySqlCommand("", con2))
    //  {
    //    con.Open();
    //    rfToken = WörterÜbersicht(dBTabelle, cmd);
    //    if (rfToken < 0)
    //    {
    //      return;
    //    }

    //    MldHilfe.BearbeiteAlleWörter(dBTabelle, TODO, cmd, con2, cmd2);
    //    con2.Close();
    //  }//using
    //}


    /// <summary>
    /// Meldungen aus DB lesen, in Wörter zerlegen und Wörter in DB eintragen
    /// </summary>
    /// <param name="conMld">fertiger Connection-String für die Datenbank mit Host, User, Password</param>
    /// <param name="dBTMeldungen">Tabelle mit Meldungen</param>
    /// <param name="dBTWoerter">Tabelle für Wörter</param>
    /// <exception cref="Exception">keine Einträge mit Meldungen gefunden</exception>
    //private static void DB4Auswerten(string ConnectionString, string dBTMeldungen, string dBTWoerter, string kennung)
    //{
    //  MldHilfe.NullOrEmpty(ConnectionString, dBTMeldungen, dBTWoerter, kennung);

    //  int neuWort = 0, nMld = 0, nToken = 0;
    //  using (MySqlConnection conWrt = new MySqlConnection(ConnectionString), conMld = new MySqlConnection(ConnectionString))
    //  using (
    //    MySqlCommand SQLMld = new MySqlCommand("", conMld),
    //                  SQLWrt = new MySqlCommand("", conWrt))
    //  {
    //    SQLMld.CommandText = $"SELECT hash,titel,meldung FROM {dBTMeldungen} WHERE NOT {kennung}";
    //    conMld.Open();
    //    conWrt.Open();
    //    {
    //      MySqlDataReader rMeld;
    //      (uint alles, uint offen) = MldHilfe.Abzählen(dBTMeldungen, SQLMld, kennung);
    //      Console.WriteLine($"{kennung.ToUpper()}: noch {offen} von {alles} ({100.0 * offen / alles:N0} %)");
    //      SQLMld.CommandText = $"SELECT hash,titel,meldung FROM {dBTMeldungen} WHERE NOT {kennung}";
    //      rMeld = SQLMld.ExecuteReader();
    //      if (rMeld.HasRows)
    //      {
    //        (neuWort, nMld, nToken) = WörterAusMeldungen(dBTMeldungen, dBTWoerter, kennung, 4, SQLWrt, rMeld);
    //      }
    //      else
    //      {
    //        Console.WriteLine($"keine Einträge in {SQLMld.CommandText}");
    //        return;
    //      }

    //    }//using
    //    conMld.Close();
    //    conWrt.Close();
    //  }
    //  Console.WriteLine($"fertig\nausgewertet\t{nMld} Meldungen\ndarin\t\t{nToken} Wörter, davon\nneu\t\t{neuWort} Wörter");
    //}

    /// <summary>
    /// aus Wortliste aus Meldung die Tabelle Einzelwörter erweitern
    /// </summary>
    /// <param name="SQL"></param>
    /// <param name="dBTWoerter"></param>
    /// <param name="WörterEinerMeldung"></param>
    private static (int, int) W4ausMld(MySqlCommand SQL, string dBTWoerter, string[] WörterEinerMeldung)
    {
      int neuWort = 0;
      int nToken = 0;
      string wort2 = null, wort3 = null, wort4 = null;
      try
      {
        foreach (string wort in WörterEinerMeldung)
        {
          string wortkette;
          MySqlDataReader rWort;
          if (wort.Length < 1)
          {
            continue;
          }

          string wort1 = wort2;
          wort2 = wort3;
          wort3 = wort4;
          wort4 = wort;
          if (wort1 == null)
          {
            continue;
          }

          wortkette = wort1 + " " + wort2 + " " + wort3 + " " + wort4;
          nToken++;
          //Console.WriteLine(wort);
          SQL.CommandText = $"SELECT anzahl FROM {dBTWoerter} WHERE (wort='{wortkette}')";
          rWort = SQL.ExecuteReader();//da wort Schlüssel ist kann es nur einen Record geben
          if (rWort.HasRows)
          {
            int nNeu;
            _ = rWort.Read();
            nNeu = Convert.ToInt32(rWort["anzahl"]) + 1;
            rWort.Close();
            SQL.CommandText = $"UPDATE {dBTWoerter} SET anzahl = {nNeu} WHERE  (wort = '{wortkette}')";
          }
          else
          {
            neuWort++;
            rWort.Close();
            SQL.CommandText = $"INSERT INTO {dBTWoerter} (wort,anzahl) VALUES ('{wortkette}',1)";
          }
          _ = SQL.ExecuteNonQuery();
        }//foreach wort

      }
      catch (MySqlException ex)
      {
        // if (ex.Message.EndsWith("t exist"))
        if (ex.Number == 1146)
        {
          Console.Error.WriteLine(ex.Message);
          SQL.CommandText = cTextauswertung.CREATE4WOERTER;
          _ = SQL.ExecuteNonQuery();
          Console.Error.WriteLine("Tabelle erzeugt");
          throw new Exception("Neustart notwendig");
        }
        else
        {
          throw ex;//2147467259 IPForWatsonBuckets = 0x0263d918 Number = 1054
        }
      }//catch
      return (neuWort, nToken);
    }//W1ausMld
  }
}