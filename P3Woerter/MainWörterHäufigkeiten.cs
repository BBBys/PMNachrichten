using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
//using Textauswertung;
//using static cTextauswertung;
namespace Borys.Nachrichten
{
  internal partial class MainWörterHäufigkeiten
  {
    /// <summary>
    /// aus Tabelle Meldungen (titel, description) in DB Wörter extrahieren und in Tabelle eintragen
    /// extrahieren
    /// </summary>
    /// <param name="args">" 
    ///     1. Parameter:    DB-Host
    /// </param>
    private const string PARAMFEHLER = "Fehler:\nmaximal 1 Argument";
    private const string DBT1WOERTER = "woerter1";
    //private const string DBT2WOERTER = "woerter2";
    private const string DBT3WOERTER = "woerter3";
    //private const string DBT4WOERTER = "woerter4";
    private const string DBT5WOERTER = "woerter5";
    //private const string DBT10WOERTER = "woerter10";
    private const string DB = "news", DBPORT = "3306";
    private const string DBUSER = "news", DBPWD = "WImGfKxkx2CQ0B9";
    private const int LIMIT =
#if DEBUG
      10
#else
      1800  //  30 Min
#endif
          ;
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
          { "W1", (1, DBT1WOERTER, "w1", cTextauswertung.CREATE1WOERTER) },
          { "W3", (3, DBT3WOERTER, "w3", cTextauswertung.CREATE3WOERTER) },
          { "W5", (5, DBT5WOERTER, "w5", cTextauswertung.CREATE5WOERTER) }
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
      string CONSTRING =
       $"Server=   {DBHOST}; " +
       $"database= {DB};" +
       $"user=     {DBUSER};" +
       $"port=      {DBPORT}; " +
       $"password=  '{DBPWD}' ";
      string CREATESTRING = string.Empty;
      Console.WriteLine($"Datenbank //{DBHOST}:{DB}");
      Stopwatch stw = new Stopwatch();
      using (MySqlConnection con = Hilfe.MySQLHilfe.ConnectToDB(DBHOST))
      using (MySqlCommand SQL = new MySqlCommand(
        $"SELECT id,parameter FROM {Hilfe.MySQLHilfe.DBTBB} WHERE programm='{titel}'",
        con))
      {
        try
        {
          bool vorzeitigesEnde = false;
          uint AufgabenId;
          uint länge;
          string wtab, kennstr, param;
          con.Open();
          MySqlDataReader reader = SQL.ExecuteReader();
          if (!reader.Read())
            Console.WriteLine($"Tabelle {Hilfe.MySQLHilfe.DBTBB} keine Einträge für {titel} ");
          else
          {
            AufgabenId = reader.GetUInt32("id");
            param = reader.GetString("parameter").ToUpper();
            reader.Close();
            stw.Start();
            (länge, wtab, kennstr, CREATESTRING) = Aufgaben[param];
            vorzeitigesEnde = DBAuswerten(CONSTRING, Hilfe.MySQLHilfe.DBTDATEN, wtab, kennstr, länge, false);
            MldHilfe.Laufzeit(stw);
            if (!vorzeitigesEnde)
            {
              string nächtes = cTextauswertung.Ablauffolge[titel];
              SQL.CommandText =
              $@"INSERT INTO {Hilfe.MySQLHilfe.DBTBB} (programm,parameter,flag) VALUES 
              ('{nächtes}','{param}',0);
              DELETE FROM {Hilfe.MySQLHilfe.DBTBB} WHERE programm='{titel}';";
              _ = SQL.ExecuteNonQuery();
              Console.Error.WriteLine("Aufgabe erledigt");
            }
            else
              Console.Error.WriteLine("noch nicht fertig!");
          }
        }//try 
        catch (MySqlException ex)
        {
          if (!Hilfe.MySQLHilfe.IfMySQLTabelleFehltExBeheben(ex, con))
          {
            Console.Error.WriteLine(ex.Message);
            throw ex;
          }
          else
          { string das = string.Empty;
            string welche = Hilfe.MySQLHilfe.WelcheTabelleFehlt(ex);
            switch (welche)
            {
              case "woerter1":
                das = "W1";
                break;
              case "woerter3":
                das = "W3";
                break;
              case "woerter5":
                das = "W5";
                break;
              default:
                break;
            }
            if (das.Length > 0)
            {
              SQL.CommandText = $"update {Hilfe.MySQLHilfe.DBTDATEN} set {das}=0;";
              SQL.ExecuteNonQuery();
            }
          }
        }//catch
        catch (Exception ex)
        {
          Console.Beep(880, 300);
          Console.Error.WriteLine($"{ex.Message}\n" +
          $"Connect String: {CONSTRING}");
        }
      }//using SQL
      Console.Beep(440, 400);
      Console.WriteLine("...fertig");
      _ = Console.ReadKey();
      return;
    }

    private static double WörterÜbersicht(string dBTabelle, MySqlCommand cmd)
    {
      MySqlDataReader reader;
      uint types, token;
      double rfToken, abd;
      //Anzahl Wörter
      cmd.CommandText = $"SELECT COUNT(anzahl) AS Types, SUM(anzahl) AS Token FROM {dBTabelle} WHERE anzahl>0";
      reader = cmd.ExecuteReader();
      _ = reader.Read();
      types = reader.GetUInt32("Types");
      Console.WriteLine($"----- Wortstatistik {dBTabelle.ToUpper()} -----\nTypes\t{types}");
      if (types < 1)
      {
        Console.WriteLine($"keine Wörter in Tabelle {dBTabelle}");
        return -1.0;
      }//entspricht if (reader.HasRows)
       //Summe Wörter
      token = reader.GetUInt32("Token");
      abd = types / (double)token;
      Console.WriteLine($"Token\t{token}\nAbdeckung\t{abd:P0}");
      rfToken = 1.0 / token;
      reader.Close();
      return rfToken;
    }

    private static void NullOrEmtpyTest(string ConnectionString, string dBTabelle)
    {
      if (string.IsNullOrEmpty(ConnectionString))
      {
        throw new ArgumentException($"\"{nameof(ConnectionString)}\" kann nicht NULL oder leer sein.", nameof(ConnectionString));
      }
      if (string.IsNullOrEmpty(dBTabelle))
      {
        throw new ArgumentException($"\"{nameof(dBTabelle)}\" kann nicht NULL oder leer sein.", nameof(dBTabelle));
      }
    }


  }
}