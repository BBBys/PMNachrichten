using Borys.Hilfe;
using MySqlConnector;
using System;
//using Textauswertung;
//using static cTextauswertung;
namespace Borys.Nachrichten
{
  internal partial class MldWählen
  {
    /// <summary>
    /// Wörter aus den Tabellen dbTWGesamt und dbTWNeu  vergleichen, 
    /// bemerkenswert häufige in neuer Tabelle speichern, 
    /// häufige und seltene ausgeben
    /// </summary>
    /// <param name="SQL1"></param>
    /// <param name="SQL2"></param>
    /// <param name="dbTWichtige"></param>
    /// <param name="dbTMeldungen"></param>
    /// <param name="ConnectionString"></param>
    /// <returns></returns>
    private static bool Wählen(MySqlCommand SQL1, MySqlCommand SQL2, string dbTWichtige, string dbTMeldungen, string ConnectionString)
    {
      string wort = string.Empty;
      try
      {
        MySqlDataReader readerWort;
        SQL1.CommandText = $"SELECT wort FROM {dbTWichtige} ORDER BY faktor DESC LIMIT 10; ";
        readerWort = SQL1.ExecuteReader();
        if (readerWort.HasRows)
        {
          SQL2.CommandText = $"UPDATE {dbTMeldungen} SET wichtig=0";
          _ = SQL2.ExecuteNonQuery();
          while (readerWort.Read())
          {
            //int hash, n;
            wort = readerWort.GetString("wort");
            SQL2.CommandText = 
              $"update {dbTMeldungen} set wichtig = 1,kriterium={wort} WHERE meldung LIKE '%{wort}%'";
            _ = SQL2.ExecuteNonQuery();
            //SQL2.CommandText = $"SELECT hash AS id,meldung as mld FROM {dbTMeldungen} WHERE meldung LIKE '%{wort}%'";
            //readerMld = SQL2.ExecuteReader();
            //if (readerMld.HasRows)
            //{
            //  n = 0;
            //  while (readerMld.Read())
            //  {
            //    n++;
            //    string mld = readerMld.GetString("mld");
            //    hash = readerMld.GetInt32("id");
            //    SQL3.CommandText = $"UPDATE {dbTMeldungen} SET wichtig=1 WHERE hash={hash}";
            //    SQL3.ExecuteNonQuery();
            //  }
            //  Console.WriteLine($"{n}x\t{wort}");
          }
        }
        else
        {
          Console.WriteLine($"keine wichtigen Wörter");
          return false;
        }
      }
      catch (MySqlException ex)
      {
        if (Hilfe.MySQLHilfe.IfMySQLTabelleFehltExBeheben(ex, ConnectionString, cTextauswertung.CREATEWICHTIGE))
        {
          Console.Error.WriteLine(ex.Message);
        }
        Console.Error.WriteLine(SQL1.CommandText);
        return false;
      }//catch
      catch (Exception ex)
      {
        Console.Error.WriteLine($"{wort}\n{ex.Message}");
        if (ex.HResult == (-2147467262))
        {
          Console.Error.WriteLine($"{wort} Häufigkeit ist null?");
        }
        throw ex;
      }

      return true;
    }

  }
}
