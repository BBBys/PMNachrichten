using MySqlConnector;
using System;
using System.Diagnostics;
//using Textauswertung;
//ing static cTextauswertung

namespace Borys.Nachrichten
{
  internal partial class WoerterZaehlen
  {
    /// <summary>
    /// Statistik Einzelwörter aus  DB
    /// </summary>
    /// <param name="ConnectionString">DB Host, User, Password usw.</param>
    /// <param name="dBTabelle">Name der Wörter-Tabelle</param>
    /// <exception cref="Exception">Tabelle leer</exception>
    /// <exception cref="ArgumentException"></exception>
    //private static void W1Statistik(string ConnectionString, string dBTabelle)
    //{
    //  NullOrEmtpyTest(ConnectionString, dBTabelle);
    //  double rfToken;
    //  using (MySqlConnection con = new MySqlConnection(ConnectionString))
    //  using (MySqlConnection con2 = new MySqlConnection(ConnectionString))
    //  using (MySqlCommand cmd = new MySqlCommand("", con))
    //  using (MySqlCommand cmd2 = new MySqlCommand("", con2))
    //  {
    //    con.Open();

    //    rfToken = WörterÜbersicht(dBTabelle, cmd);
    //    //rel. Daten
    //    if (rfToken < 0)
    //    {
    //      return;
    //    }
    //    //rel. Daten
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
//    private static void DBAuswerten(
//      string ConnectionString,
//      string dBTMeldungen,
//      string dBTWoerter,
//      string kennung,
//      uint länge, bool neu)
//    {

//      uint neuWort = 0, nMld = 0, nToken = 0;
//      MldHilfe.NullOrEmpty(ConnectionString, dBTMeldungen, dBTWoerter, kennung);
//      using (MySqlConnection conWrt = new MySqlConnection(ConnectionString))
//      using (MySqlConnection conMld = new MySqlConnection(ConnectionString))
//      using (MySqlCommand SQLMld = new MySqlCommand("", conMld))
//      using (MySqlCommand SQLWrt = new MySqlCommand("", conWrt))
//      {
//        conMld.Open();
//        conWrt.Open();
//        {
//          MySqlDataReader rMeld;
//          (uint alles, uint offen, DateTime erste, DateTime letzte) = MldHilfe.Abzählen(dBTMeldungen, SQLMld, kennung);
//          Console.WriteLine(
//            $"Meldungen auswerten für {kennung.ToUpper()}: noch {offen} von {alles} Meldungen ({offen / (double)alles:P0})");
//          Console.WriteLine($"von\t{erste} bis {letzte})");
//          if (neu)
//          {
//            TimeSpan dif = new TimeSpan(10, 0, 0, 0);
//            DateTime anfang = DateTime.Now - dif;
//            Console.WriteLine($"auswerten ab\t{anfang} bis {letzte})");
//            SQLMld.CommandText =
//$"SELECT hash AS Hash, titel AS Titel, meldung AS Meldung FROM {dBTMeldungen} WHERE datum>'{anfang:yyyy-M-dd HH:mm:ss}' ORDER BY datum ASC";
//          }
//          else
//          {
//            SQLMld.CommandText = $"SELECT hash AS Hash, titel AS Titel, meldung AS Meldung FROM {dBTMeldungen} WHERE NOT {kennung} OR {kennung} IS NULL";
//          };

//          rMeld = SQLMld.ExecuteReader();
//          if (rMeld.HasRows)
//          {
//            (neuWort, nMld, nToken) = WörterAusMeldungen(dBTMeldungen, dBTWoerter, kennung, länge, SQLWrt, rMeld);
//          }
//          else
//          {
//            Console.WriteLine($"keine offenen Einträge in {SQLMld.CommandText}");
//            return;
//          }
//        }//using
//        conMld.Close();
//        conWrt.Close();
//      }
//      Console.WriteLine($"fertig\nausgewertet\t{nMld} Meldungen\ndarin\t\t{nToken} Wörter, davon\nneu\t\t{neuWort} Wörter");
//    }

    private static (uint, uint, uint) WörterAusMeldungen(
      string dBTMeldungen,
      string dBTWoerter,
      string kennung,
      uint länge,
      MySqlCommand SQLWrt, MySqlDataReader rMeld)
    {
      string text, txtOriginal;
      long MldHash;
      uint neuWort = 0, nMld = 0, nToken = 0;
      //SQLWrt.CommandText = $"update {dBTWoerter} set relh=(-1);";
      //_ = SQLWrt.ExecuteNonQuery();

      Stopwatch timer = Stopwatch.StartNew();
      while (rMeld.Read())
      {
        uint pNeu, pTok;
        nMld++;
        txtOriginal = (string)rMeld["Titel"] + "." + (string)rMeld["Meldung"];
        //Console.WriteLine(text0);
        MldHash = rMeld.GetInt64("Hash");
        text = cTextauswertung.strukturieren(txtOriginal);

        (pNeu, pTok) = MldHilfe.WörterAusMld(SQLWrt, dBTWoerter, text, länge);

        neuWort += pNeu;
        nToken += pTok;
        SQLWrt.CommandText = $"UPDATE {dBTMeldungen} SET {kennung}=TRUE WHERE hash={MldHash}";
        if (SQLWrt.ExecuteNonQuery() != 1)//es muss genau einen Record geben
        {
          throw new Exception($"Updatefehler {SQLWrt.CommandText}");
        }
        if (nMld % 10 == 0)
        {
          Console.Write(".");
          if (nMld % 50 == 0)
          {
            if (MldHilfe.Laufzeit(timer, nMld, nToken, neuWort, LIMIT))
            {
              break;
            }
          }
        }
      }//while eine Meldung
      return (neuWort, nMld, nToken);
    }


    /// <summary>
    /// aus Wortliste aus Meldung die Tabelle Einzelwörter erweitern
    /// </summary>
    /// <param name="SQL"></param>
    /// <param name="dBTWoerter"></param>
    /// <param name="WörterEinerMeldung"></param>
    //    private static (int, int) W1ausMld(MySqlCommand SQL, string dBTWoerter, string[] WörterEinerMeldung)
    //    {
    //      int neuWort = 0;
    //      int nToken = 0;
    //      try
    //      {
    //        foreach (string wort in WörterEinerMeldung)
    //        {
    //          MySqlDataReader rWort;
    //          if (wort.Length < 1)
    //          {
    //            continue;
    //          }

    //          nToken++;
    //          //Console.WriteLine(wort);
    //          SQL.CommandText = $"SELECT anzahl FROM {dBTWoerter} WHERE (wort = '{wort}')";
    //          rWort = SQL.ExecuteReader();//da w1 Schlüssel ist kann es nur einen Record geben
    //          if (rWort.HasRows)
    //          {
    //            int nNeu;
    //            _ = rWort.Read();
    //            nNeu = Convert.ToInt32(rWort["anzahl"]) + 1;
    //            rWort.Close();
    //            SQL.CommandText = $"UPDATE {dBTWoerter} SET anzahl = {nNeu} WHERE (wort = '{wort}')";
    //          }
    //          else
    //          {
    //            neuWort++;
    //            rWort.Close();
    //            SQL.CommandText = $"INSERT INTO {dBTWoerter} (wort,anzahl) VALUES ('{wort}',1)";
    //          }
    //          _ = SQL.ExecuteNonQuery();
    //        }//foreach wort

    //      }
    //      catch (MySqlException ex)
    //      {
    //        // if (ex.Message.EndsWith("t exist"))
    //        if (ex.Number == 1146)
    //        {
    //          Console.Error.WriteLine(ex.Message);
    //          SQL.CommandText = cTextauswertung.CREATE1WOERTER;
    //          _ = SQL.ExecuteNonQuery();
    //          Console.Error.WriteLine("Tabelle erzeugt");
    //          throw new Exception("Neustart notwendig");
    //        }
    //        else
    //        {
    //          throw ex;//2147467259 IPForWatsonBuckets = 0x0263d918 Number = 1054
    //        }
    //      }//catch
    //      return (neuWort, nToken);
    //    }//W1ausMld
  }
}