using Borys.Hilfe;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
//using Textauswertung;
//using static cTextauswertung;
namespace Borys.Nachrichten
{
  internal partial class NeuWoerter
  {
    /// <summary>
    /// aus Tabelle nur neue Meldungen (titel, description) in DB Wörter extrahieren 
    /// und in Tabelle eintragen
    /// </summary>
    /// <param name="args">" 
    ///     1. Parameter:    DB-Host
    /// </param>
    private const string PARAMFEHLER = "Fehler:\nmaximal 1 Argument";
    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    /// <exception cref="Exception"></exception>
    private static void Main(string[] args)
    {
      Dictionary<string, (uint, string, string, string)>
            Aufgaben = new Dictionary<string, (uint, string, string, string)>
            {
          { "W1", (1,Hilfe.MySQLHilfe.DBT1WÖRTER, "w1", cTextauswertung.CREATE1WOERTER.Replace("rter","rterNeu")) },
          { "W3", (3, Hilfe.MySQLHilfe.DBT3WÖRTER,"w3", cTextauswertung.CREATE3WOERTER.Replace("rter", "rterNeu")) },
          { "W5", (5, Hilfe.MySQLHilfe.DBT5WÖRTER, "w5", cTextauswertung.CREATE5WOERTER.Replace("rter", "rterNeu")) }
            };
      bool dennoch=false;
      Assembly assembly = Assembly.GetExecutingAssembly();
      FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
      string productVersion = fvi.ProductVersion;
      string titel = fvi.FileDescription; //Assemblyinfo -> Titel
      Console.WriteLine($"{titel} V{productVersion}");
      Console.WriteLine(fvi.Comments);
      Console.Title = titel;
      string DBHOST = args.Length > 0 ? args[0] : "localhost";
      if (args.Length > 1)
      {if (args[1].StartsWith("-dennoch"))
          dennoch = true;
        else
        {
          Console.Error.WriteLine(PARAMFEHLER);
          Console.Beep(440, 300);
          Console.Beep(880, 200);
          _ = Console.ReadKey();
          throw new Exception(PARAMFEHLER);
        }
      }
      //string CONSTRING =
      // $"Server=   {DBHOST}; " +
      // $"database= {DB};" +
      // $"user=     {DBUSER};" +
      // $"port=      {DBPORT}; " +
      // $"password=  '{DBPWD}' ";
     // string CREATESTRING = null;
      using (MySqlConnection con = Hilfe.MySQLHilfe.ConnectToDB(DBHOST))
      using (MySqlCommand SQL = new MySqlCommand(
        $"SELECT id,parameter FROM news.{Hilfe.MySQLHilfe.DBTBB} WHERE programm='{titel}'",
        con),
        SQL2 = new MySqlCommand("", con))
      {
        Console.WriteLine($"Datenbank //{con.DataSource}:{con.Database}");
        try
        {
          uint AufgabenId;
          uint länge;
          string wtab, kennstr, param;
          con.Open();
          MySqlDataReader reader = SQL.ExecuteReader();
#if false
AufgabenId = 0;
          param = "W5";
          { 
#else
if (!reader.HasRows)              Console.WriteLine($"Tabelle {Hilfe.MySQLHilfe.DBTBB} keine Einträge für {titel} ");
          else
          {
            reader.Read();
            AufgabenId = reader.GetUInt32("id");
            param = reader.GetString("parameter").ToUpper();
#endif
          reader.Close();
            (länge, wtab, kennstr, _) = Aufgaben[param];
            wtab = wtab.Replace("rter", "rterNeu");
            WörterAusNeuenMld(
              DBHOST,
              con,
              Hilfe.MySQLHilfe.DBTMELDUNGEN,
              Hilfe.MySQLHilfe.DBTDATEN,
              wtab,
              SQL,
              kennstr,
              länge);
            _ = WörterÜbersicht(wtab, SQL);
            SQL.CommandText =
            $@"INSERT INTO {Hilfe.MySQLHilfe.DBTBB} (programm,parameter,flag) 
            VALUES ('P31WörterZählen','{param}',1);
            DELETE FROM {Hilfe.MySQLHilfe.DBTBB} WHERE programm='{titel}' AND parameter='{param}';";
            _ = SQL.ExecuteNonQuery();
            Console.Error.WriteLine("Aufgabe erledigt");
          }
        }//try aussen
        catch (MySqlException ex)
        {
          if (!Hilfe.MySQLHilfe.IfMySQLTabelleFehltExBeheben(ex, con))
            Console.Error.WriteLine(ex.Message);
        }//catch
        catch (Exception ex)
        {
          Console.Beep(880, 300);
          Console.Error.WriteLine($"Exception\t{ex.Message}\n\tTrace\n{ex.StackTrace}\n\tTrace");
        }
      }
      Console.Beep(440, 400);
      Console.WriteLine("...fertig");
      _ = Console.ReadKey();
      return;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dBTabelle"></param>
    /// <param name="SQL"></param>
    /// <returns></returns>
    private static double WörterÜbersicht(string dBTabelle, MySqlCommand SQL)
    {
      uint types, token;
      double rfToken, abd;
      //Anzahl Wörter
      SQL.CommandText = $"SELECT COUNT(anzahl) AS Types, SUM(anzahl) AS Token FROM {dBTabelle} WHERE anzahl>0";
      using (MySqlDataReader reader = SQL.ExecuteReader())
      {
        _ = reader.Read();
        types = reader.GetUInt32("Types");
        Console.WriteLine($"----- Wortstatistik {dBTabelle.ToUpper()} -----\nTypes\t{types}");
        if (types < 1)
        {
          Console.WriteLine($"keine Wörter in Tabelle {dBTabelle}");
          rfToken = -1.0;
        }//entspricht if (reader.HasRows)
        else
        {
          //Summe Wörter
          token = reader.GetUInt32("Token");
          abd = types / (double)token;
          Console.WriteLine($"Token\t{token}\nAbdeckung\t{abd:P0}");
          rfToken = 1.0 / token;
        }
        reader.Close();
      }
      return rfToken;
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