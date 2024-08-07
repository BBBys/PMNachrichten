using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
namespace Borys.Nachrichten
{
  internal partial class WoerterVergleichn
  {
    /// <summary>
    /// aus Tabelle Meldungen (titel, description) in DB Wörter extrahieren und 
    /// in Tabelle eintragen
    /// </summary>
    /// <param name="args">" 
    ///     1. Parameter:    DB-Host
    /// </param>
    private const string PARAMFEHLER = "Fehler:\nmaximal 1 Argument";
    //private const string DBTTASKS = "tasks";
    //private const string DBT1WOERTER = "woerter1";
    //private const string DBT3WOERTER = "woerter3";
    //private const string DBT5WOERTER = "woerter5";
    //private const string DBTWICHTIGE = "wichtige";
    //private const string DB = "news", DBPORT = "3306";
    //private const string DBUSER = "news", DBPWD = "WImGfKxkx2CQ0B9";
    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    /// <exception cref="Exception"></exception>
    private static void Main(string[] args)
    {
      Dictionary<string, (uint, string, string)>
         Aufgaben = new Dictionary<string, (uint, string, string)>
         {
          { "W1", (1,Hilfe.MySQLHilfe.DBT1WÖRTER, "w1") },
          { "W3", (3, Hilfe.MySQLHilfe.DBT3WÖRTER, "w3") },
          { "W5", (5, Hilfe.MySQLHilfe.DBT5WÖRTER, "w5") }
         };
      Assembly assembly = Assembly.GetExecutingAssembly();
      FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
      string productVersion = fvi.ProductVersion;
      string titel = fvi.FileDescription; //Assemblyinfo -> Titel
      Console.WriteLine($"{titel} V{productVersion}");
      Console.Title = titel;
      string DBHOST = args.Length > 0 ? args[0] : "localhost";
      if (args.Length > 1)
      {
        Console.Error.WriteLine(PARAMFEHLER);
        Console.Beep(440, 300);
        Console.Beep(880, 200);
        _ = Console.ReadKey();
        throw new Exception(PARAMFEHLER);
      }
      Stopwatch stw = new Stopwatch();
      using (MySqlConnection con = Hilfe.MySQLHilfe.ConnectToDB(DBHOST))
      using (MySqlCommand SQL = new MySqlCommand(string.Empty, con))
      {
        Debug.WriteLine($"Datenbank //{con.DataSource}:{con.Database}");
        try
        {
          uint startid ;
          MySqlDataReader reader;
          string wtab, kennstr;
          uint länge;
          string param;
          con.Open();
          SQL.CommandText =
            $"SELECT parameter FROM {Hilfe.MySQLHilfe.DBTBB} WHERE programm='P48ex';";
          reader = SQL.ExecuteReader();startid = 0;
          if (reader.Read())
          {
            string strStart = reader.GetString("parameter");
            //die Suche nach offenen Aufgaben beginnt bei startid
            startid = Convert.ToUInt32(strStart);
          }
          reader.Close();
          SQL.CommandText =
              $@"SELECT id,parameter,flag FROM {Hilfe.MySQLHilfe.DBTBB} 
              WHERE programm='{titel}' and id>{startid};";
          reader = SQL.ExecuteReader();
          if (!reader.Read())
          {
            reader.Close();
            Console.WriteLine(
              $"Tabelle {Hilfe.MySQLHilfe.DBTBB} keine Einträge für {titel} ab id={startid} ");
          }
          else
          {
            uint id = reader.GetUInt32("id");
            param = reader.GetString("parameter");
            bool flag = reader.GetBoolean("flag");
            reader.Close();
            SQL.CommandText =
              $@"SELECT parameter,flag FROM {Hilfe.MySQLHilfe.DBTBB} 
              WHERE programm='{titel}' AND parameter='{param}' AND flag IS NOT {flag};";
            reader = SQL.ExecuteReader();
            if (!reader.Read())
            {
              reader.Close();
              Console.WriteLine(
$@"Tabelle {Hilfe.MySQLHilfe.DBTBB} kein weiterer Eintrag für 
programm='{titel}' 
AND parameter='{param}' 
AND flag IS NOT {flag}");
              SQL.CommandText =
              $@"delete from {Hilfe.MySQLHilfe.DBTBB} where programm='P48ex';
              insert into {Hilfe.MySQLHilfe.DBTBB}
              (programm,parameter) 
              VALUES ('P48ex','{id}');";
              SQL.ExecuteNonQuery();
            }
            else
            {
              reader.Close();
              stw.Start();
              (länge, wtab, kennstr) = Aufgaben[param];
              string wtab2 = wtab.Replace("rter", "rterNeu");
              bool erfolg = WVergleich(DBHOST, wtab, wtab2,Hilfe.MySQLHilfe.DBTWICHTIGE, param);
              MldHilfe.Laufzeit(stw);
              if (erfolg)
              {
                SQL.CommandText =
                $@"INSERT INTO {Hilfe.MySQLHilfe.DBTBB} 
                (programm,parameter) VALUES ('P48WichtigeMld','{param}');";
                SQL.ExecuteNonQuery();
              }
              SQL.CommandText =
                $@"DELETE FROM {Hilfe.MySQLHilfe.DBTBB} WHERE programm='{titel}' AND parameter='{param}';
                DELETE FROM {Hilfe.MySQLHilfe.DBTBB} WHERE programm = 'P48ex'";
              SQL.ExecuteNonQuery();

            }
          }
        }//try aussen
        catch (MySqlException ex)
        {
          if (!Hilfe.MySQLHilfe.IfMySQLTabelleFehltExBeheben(ex, con))
          {
            Console.Error.WriteLine(ex.Message);
          }
        }//catch
        catch (Exception ex)
        {
          Console.Beep(880, 300);
          Console.Error.WriteLine(
$@"{ex.Message}
-----     Trace    -----
{ex.StackTrace}");
        }
      }
      Console.Beep(440, 400);
      Console.WriteLine("...fertig");
      _ = Console.ReadKey();
      return;
    }

    private static (uint, uint, double) WörterÜbersicht(string dBTabelle, MySqlCommand SQL, string nulltest = "")
    {
      string testen;
      MySqlDataReader reader;
      uint types, token;
      double rfToken, abd;
      testen = (nulltest.Length > 1) ? $"AND '{nulltest}' IS NOT null" : "";
      //Anzahl Wörter
      //cmd.CommandText = "SELECT COUNT(`anzahl`) as Types FROM `woerterNeu3` WHERE anzahl> 0 and `relh` IS NOT null;";
      SQL.CommandText =
        $@"SELECT COUNT(anzahl) as isnull FROM {dBTabelle} WHERE relh IS NULL";

      reader = SQL.ExecuteReader();
      _ = reader.Read();
      uint hatNull = reader.GetUInt32("isnull");
      reader.Close();
      SQL.CommandText =
     $@"SELECT COUNT(anzahl) AS Types, 
                  SUM(anzahl)   AS Token 
        FROM {dBTabelle} WHERE (anzahl>0 {testen})";
      reader = SQL.ExecuteReader();
      types = token = 0;
      abd = 0;
      if (reader.Read())
      {
        types = reader.GetUInt32("Types");
        if (types > 0)
        {
          token = reader.GetUInt32("Token");
          abd = types / (double)token;
        }
      }
      reader.Close();

      Console.WriteLine(
$@"-----     Wortstatistik {dBTabelle.ToUpper()}     -----
{types,5}   Types
{token,5}   Token
{abd,7:P0}  Wiederholungen");
      if (types < 1)
      {
        Console.WriteLine($"keine Wörter in Tabelle {dBTabelle}");
        return (0, 0, 0);
      }//entspricht if (reader.HasRows)
      rfToken = 1.0 / token;
      return (types, hatNull, rfToken);
    }


    //private static void NullOrEmtpyTest(string ConnectionString, string dBTabelle)
    //{
    //  if (string.IsNullOrEmpty(ConnectionString))
    //  {
    //    throw new ArgumentException($"\"{nameof(ConnectionString)}\" kann nicht NULL oder leer sein.", nameof(ConnectionString));
    //  }
    //  if (string.IsNullOrEmpty(dBTabelle))
    //  {
    //    throw new ArgumentException($"\"{nameof(dBTabelle)}\" kann nicht NULL oder leer sein.", nameof(dBTabelle));
    //  }
    //}


  }
}