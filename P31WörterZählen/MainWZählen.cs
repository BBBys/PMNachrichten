using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
//using Textauswertung;
//using static cTextauswertung;
namespace Borys.Nachrichten
{
  internal partial class WörterZählen
  {
    /// <summary>
    /// in Tabelle woerter[neu]<n> zählen und rel.Häufigkeiten eintragen
    /// </summary>
    /// <param name="args">" 
    ///     1. Parameter:    DB-Host
    /// </param>
    private const string PARAMFEHLER = "Fehler:\nmaximal 3 Argumente";
    private const string DBTTASKS = "tasks";
    // private const string DBTMELDUNGEN = "meldungen";
    private const string DBT1WOERTER = "woerter1";
    private const string DBT2WOERTER = "woerter2";
    private const string DBT3WOERTER = "woerter3";
    private const string DBT4WOERTER = "woerter4";
    private const string DBT5WOERTER = "woerter5";
    private const string DBT10WOERTER = "woerter10";
    private const string DB = "news", DBPORT = "3306";
    private const string DBUSER = "news", DBPWD = "WImGfKxkx2CQ0B9";
    private const int LIMIT =
#if DEBUG
      10;
#else
1800;
#endif
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
      Console.WriteLine(fvi.Comments);
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
      //string CONSTRING =
      // $"Server=   {DBHOST}; " +
      // $"database= {DB};" +
      // $"user=     {DBUSER};" +
      // $"port=      {DBPORT}; " +
      // $"password=  '{DBPWD}' ";
      //string CREATESTRING = null;
      Console.WriteLine($"Datenbank //{DBHOST}:{DB}");
      Stopwatch stw = new Stopwatch();
      using (MySqlConnection con = Hilfe.MySQLHilfe.ConnectToDB(DBHOST))
      using (MySqlCommand SQL = new MySqlCommand(
        $"SELECT id,parameter,flag FROM {Hilfe.MySQLHilfe.DBTBB} WHERE programm='{titel}'",
        con))
      {
        try
        {
          uint AufgabenId;
          uint länge;
          string wtab, kennstr, param;
          con.Open();
          MySqlDataReader reader = SQL.ExecuteReader();
          if (!reader.Read())
            Console.WriteLine($"Tabelle {Hilfe.MySQLHilfe.DBTBB} keine Einträge für {titel} ");
          else
          {
            bool neu;
            string nächstes;
            int flag;
            AufgabenId = reader.GetUInt32("id");
            param = reader.GetString("parameter").ToUpper();
            neu = reader.GetBoolean("flag");
            reader.Close();
            stw.Start();
            (länge, wtab, kennstr, _) = Aufgaben[param];
            wtab = neu ? wtab.Replace("oerter", "oerterNeu") : wtab;
            flag = neu ? 1 : 0;
            if (!WStatistik(DBHOST, länge, wtab))
            {
              // Worttab ist leer
              nächstes = "P3Woerter";
              Console.Error.WriteLine($"Tabelle {wtab} ist leer, {nächstes},{param},{flag} wird angefordert");
              SQL.CommandText =
                neu ?
                $@"INSERT INTO {Hilfe.MySQLHilfe.DBTBB} 
              (programm,parameter,flag) VALUES ('P41neuWoerter','{param}',{flag});" :
                $@"INSERT INTO {Hilfe.MySQLHilfe.DBTBB} 
              (programm,parameter,flag) VALUES ('P3Woerter','{param}',{flag});";
              _ = SQL.ExecuteNonQuery();
            }
            else
            {
              MldHilfe.Laufzeit(stw);
              nächstes = cTextauswertung.Ablauffolge[titel];
              SQL.CommandText =
                $@"INSERT INTO {Hilfe.MySQLHilfe.DBTBB} 
              (programm,parameter,flag) VALUES ('{nächstes}','{param}',{flag});
              DELETE FROM {Hilfe.MySQLHilfe.DBTBB} WHERE programm='{titel}' and flag={flag};";
              _ = SQL.ExecuteNonQuery();
              Console.Error.WriteLine("Aufgabe erledigt");
            }
          }
        }//try aussen
        catch (MySqlException ex)
        {
          if (!Borys.Hilfe.MySQLHilfe.IfMySQLTabelleFehltExBeheben(ex,con))
          {
            Console.Error.WriteLine(ex.Message);
          }
        }//catch
        catch (Exception ex)
        {
          Console.Beep(880, 300);
          Console.Error.WriteLine($"{ex.Message}");
        }
        Console.Beep(440, 400);
        Console.WriteLine("...fertig");
        _ = Console.ReadKey();
        return;
      }
    }

    private static double WörterÜbersicht(string dBTabelle, MySqlCommand cmd)
    {
      uint types, token;
      double rfToken, abd;
      //Anzahl Wörter
      cmd.CommandText = $"SELECT COUNT(anzahl) AS Types, SUM(anzahl) AS Token FROM {dBTabelle} WHERE anzahl>0";
      using (MySqlDataReader reader = cmd.ExecuteReader())
      {
        _ = reader.Read();
        types = reader.GetUInt32("Types");
        Console.WriteLine(
$@"-----     Wortstatistik {dBTabelle.ToUpper()}     -----
Types  {types}");
        if (types < 1)
        {
          Console.WriteLine($"keine Wörter in Tabelle {dBTabelle.ToUpper()}");
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

    private static bool WStatistik(
      string dbHost,
      uint länge,
      string dBTabelle)
    {
      double rfToken;
      using (MySqlConnection con =Hilfe.MySQLHilfe.ConnectToDB(dbHost), 
        con2 = Hilfe.MySQLHilfe.ConnectToDB(dbHost))
      using (MySqlCommand cmd = new MySqlCommand(string.Empty, con),
        cmd2 = new MySqlCommand(string.Empty, con2))
      {
        con.Open();
        rfToken = WörterÜbersicht(dBTabelle, cmd);
if (rfToken < 0)return false;
        MldHilfe.BearbeiteAlleWörter(dBTabelle, länge, cmd, con2, cmd2, LIMIT);
        con.Close();
      }//using
      return true;
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