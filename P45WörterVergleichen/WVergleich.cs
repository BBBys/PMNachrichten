using MySqlConnector;
using System;
using System.Collections.Generic;
using Textauswertung;
//using Textauswertung;
//using static cTextauswertung;
namespace Borys.Nachrichten
{
  internal partial class WoerterVergleichn
  {
    /// <summary>
    /// Wörter aus den Tabellen dbTWGesamt und dbTWNeu  vergleichen, 
    /// bemerkenswert häufige in neuer Tabelle speichern, 
    /// häufige und seltene ausgeben
    /// </summary>
    /// <param name="DBHOST"></param>
    /// <param name="dbTWGesamt"></param>
    /// <param name="dbTWNeu"></param>
    /// <param name="dbTWichtige"></param>
    /// <returns></returns>
    /// <param name="param"></param>
    /// 
    /// 
    private static bool WVergleich(
      string DBHOST,
      string dbTWGesamt,
      string dbTWNeu,
      string dbTWichtige,
      string param)
    {
      uint types1, types2;
      using (MySqlConnection con = Hilfe.MySQLHilfe.ConnectToDB(DBHOST))
      using (MySqlCommand SQL = new MySqlCommand(string.Empty, con))
      {
#if DEBUG
        const string maxRelh = "0.00275"; //es muss DezimalPUNKT sein
        const int minAnzahl = 2;
#else
        const string maxRelh = "0.00275"; //es muss DezimalPUNKT sein
        const int minAnzahl = 3;
#endif
        const double minMehr = Kriterien.MINDESTZUWACHSWICHTIGE;
        const double maxMehr = 3.0 / minMehr;
        string wort = string.Empty;
        double rhNeu, rhAlle;
        con.Open();
        try
        {
          uint hatNull1, hatNull2;
          //häufige Wörter weglassen, sehr seltene auch weglassen
          SQL.CommandText =
            $@"DELETE FROM {dbTWNeu} WHERE {dbTWNeu}.relh > {maxRelh}; 
              DELETE FROM {dbTWNeu} WHERE {dbTWNeu}.anzahl < {minAnzahl}; ";
          _ = SQL.ExecuteNonQuery();
          (types1, hatNull1, _) = WörterÜbersicht(dbTWGesamt, SQL, "relh");
          (types2, hatNull2, _) = WörterÜbersicht(dbTWNeu, SQL, "relh");
          if (types1 < 1 || types2 < 1 || (hatNull1 + hatNull2 > 0))
          {
            Console.Error.WriteLine(
$@"{dbTWNeu}  {types2,5}  Worttypes und {hatNull2} Nullwerte in neuen Meldungen
{dbTWGesamt}  {types1,5}  Worttypes und {hatNull1} Nullwerte zum Vergleichen
--- es reicht nicht!");
            //Fehlerbehebung
            if (types1 < 1)
            { Console.Error.WriteLine("Schweres Problem - Tabelle {dbTWGesamt} ist leer");
              SQL.CommandText =
                $@"INSERT INTO {Hilfe.MySQLHilfe.DBTBB} 
                (programm,parameter) VALUES ('P31Wörter','{param}')";
              _ = SQL.ExecuteNonQuery();
            }
            if (types2 < 1)
            {
              Console.Error.WriteLine($"Tabelle {dbTWNeu} ist leer");
              SQL.CommandText =
                $@"INSERT INTO {Hilfe.MySQLHilfe.DBTBB} 
                (programm,parameter) VALUES ('P41neuWörter','{param}')";
              _ = SQL.ExecuteNonQuery();
            }
            if (hatNull1 > 0)
            {
              Console.Error.WriteLine($"Tabelle {dbTWGesamt} hat Nullwerte");
              SQL.CommandText =
                $@"INSERT INTO {Hilfe.MySQLHilfe.DBTBB} 
                (programm,parameter,flag) VALUES ('P31WörterZählen','{param}',0)";
              _ = SQL.ExecuteNonQuery();
            }
            if (hatNull2 > 0)
            {
              Console.Error.WriteLine($"Tabelle {dbTWNeu} hat Nullwerte");
              SQL.CommandText =
                $@"INSERT INTO {Hilfe.MySQLHilfe.DBTBB} 
                (programm,parameter,flag) VALUES ('P31WörterZählen','{param}',1)";
              _ = SQL.ExecuteNonQuery();
            }
            return false;
          }
          SQL.CommandText = $@"SELECT 
            {dbTWGesamt}.wort AS Wort,
            {dbTWNeu}.relh    AS RhNeu,
            {dbTWGesamt}.relh AS RhGesamt 
            FROM {dbTWGesamt} JOIN {dbTWNeu} 
            ON {dbTWGesamt}.wort={dbTWNeu}.wort ; ";
          SQL.CommandTimeout = 60;
          MySqlDataReader reader = SQL.ExecuteReader();
          if (reader.HasRows)
          {
            using (MySqlConnection con2 =Hilfe.MySQLHilfe.ConnectToDB(DBHOST))
            using (MySqlCommand SQL2 = new MySqlCommand(string.Empty, con2))
            {
              double maxZuwachs = 0;
              con2.Open();
              Dictionary<string, double> weniger = new Dictionary<string, double>();
              Dictionary<string, double> mehr = new Dictionary<string, double>();
              while (reader.Read())
              {
                double faktor;
                wort = reader.GetString("Wort");
                rhNeu = reader.GetDouble("RhNeu");
                rhAlle = reader.GetDouble("RhGesamt");
                if (rhAlle > 0)
                {
                  faktor = rhNeu / rhAlle;
                  if(faktor>maxZuwachs)
                    maxZuwachs = faktor;
                  if (faktor > Kriterien.MINDESTZUWACHSWICHTIGE)
                  {
                    string sFaktor = Convert.ToString(faktor, MldHilfe.DEZIMALPUNKT);
                    SQL2.CommandText =
                      $"INSERT INTO {dbTWichtige} (wort,faktor) VALUES ('{wort}',{sFaktor})";
                    _ = SQL2.ExecuteNonQuery();
                    mehr.Add(wort, faktor);
                  }
                  if (faktor < maxMehr)
                    weniger.Add(wort, faktor);
                }
              }
              Console.WriteLine($"maximal {maxZuwachs:P} mehr\n***\t{mehr.Count} mehr\t***");
              SQL2.CommandText = $"SELECT wort,faktor FROM {dbTWichtige} ORDER BY faktor DESC LIMIT 20";
              reader = SQL2.ExecuteReader();
              if (reader.HasRows)
              while (reader.Read())
                  Console.WriteLine($"{reader.GetDouble("faktor"):N2}\t{reader.GetString("wort")}");
              con2.Close();
            }
          }
        }
        catch (MySqlException ex)
        {
          if (Hilfe.MySQLHilfe.IfMySQLTabelleFehltExBeheben(ex, con))
          {
            Console.Error.WriteLine(ex.Message);
            return false;
          }
          Console.Error.WriteLine(SQL.CommandText);
          return false;
        }//catch
        catch (Exception ex)
        {
          Console.Error.WriteLine($"{wort}\n{ex.Message}");
          if (ex.HResult == (-2147467262))
          {
            Console.Error.WriteLine("-----\tTrace\t-----\n{ex.StackTrace}\n-----\tTrace\t-----\n{ex.Message}");
            if (wort != null && wort.Length > 0)
            {
              Console.Error.WriteLine($@"{wort} Häufigkeit ist null?
P31WörterZählen für {dbTWGesamt} und {dbTWNeu} ausführen");
              SQL.CommandText =
              $@"update {Hilfe.MySQLHilfe.DBTBB} 
              SET (programm,parameter,flag) VALUES 
              ('P31WörterZählen','{param}',0),
              ('P31WörterZählen','{param}',1)";
              _ = SQL.ExecuteNonQuery();
            }
          }
          con.Close();
          return false;
        }
        con.Close();
      }//using SQL, con
      return true;
    }
  }
}
