using MySqlConnector;
using System;

namespace Borys.Nachrichten
{
  internal partial class WartMain
  {
    private static void DBW1(MySqlConnection con, string dBTMeldungen, string dBTWörter)
    {
      using (MySqlCommand cmd = new MySqlCommand("", con))
      {
        cmd.CommandText = $"UPDATE {dBTMeldungen} SET w1=0 ";
        con.Open();
        cmd.ExecuteNonQuery();
        cmd.CommandText = $"TRUNCATE TABLE {dBTWörter}";
        cmd.ExecuteNonQuery();
      }
      con.Close();
    }

    /// <summary>
    /// Meldungen aus DB lesen, in Wörter zerlegen und Wörter in DB eintragen
    /// </summary>
    /// <param name="conEin">fertiger Connection-String für die Datenbank mit Host, User, Password</param>
    /// <param name="dBTMeldungen">Tabelle mit Meldungen</param>
    /// <param name="dBTWoerter">Tabelle für Wörter</param>
    /// <exception cref="Exception">keine Einträge mit Meldungen gefunden</exception>
    private static void DBReHash(MySqlConnection conEin, string dBTMeldungen)
    {
      if (conEin is null)
      {
        throw new ArgumentNullException(nameof(conEin));
      }
      if (string.IsNullOrEmpty(dBTMeldungen))
      {
        throw new ArgumentException($"\"{nameof(dBTMeldungen)}\" kann nicht NULL oder leer sein.", nameof(dBTMeldungen));
      }
      int nMld = 0, nNeu = 0;
      using (MySqlConnection conAus = new MySqlConnection(conEin.ConnectionString))
      using (
        MySqlCommand cmdEin = new MySqlCommand("", conEin),
                      cmdAus = new MySqlCommand("", conAus))
      {
        cmdEin.CommandText = $"SELECT hash FROM {dBTMeldungen} ;";
        conEin.Open();
        conAus.Open();
        using (MySqlDataReader rMeld = cmdEin.ExecuteReader())
        {
          string text;
          int ret;
          long hashAlt, hashNeu = 0;
          if (rMeld.HasRows)
          {
            while (rMeld.Read())
            {
              MySqlDataReader rPrüf;
              nMld++;
              hashAlt = Convert.ToInt64(rMeld["hash"]);
              hashNeu++;
              cmdAus.CommandText =
              $"UPDATE {dBTMeldungen} SET hash={hashNeu} WHERE (hash = {hashAlt} )";
              ret = cmdAus.ExecuteNonQuery();
              if (nMld % 200 == 0)
              {
                Console.Write(".");
                if (nMld % 1000 == 0)
                {
                  Console.WriteLine($"\n{nMld} Meldungen geändert ");
                }
              }
            }//while eine Meldung
          }
          else
          {
            throw new Exception($"keine Einträge in {cmdEin.CommandText}");
          }
        }//using
        conEin.Close();
      }
      Console.WriteLine($"fertig\nausgewertet\t{nMld} Meldungen und geändert");
    }
  }
}
