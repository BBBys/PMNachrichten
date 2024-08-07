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
    /// aus Tabelle markierte Meldungen (titel, description) extrahieren 
    /// und in HTML-Seite aufbauen
    /// </summary>
    /// <param name="args">" 
    ///     1. Parameter:    DB-Host
    /// </param>
    private const string PARAMFEHLER = "Fehler:\nmaximal 1 Argument";
    private const string DBTTASKS = "tasks";
    private const string DBTMELDUNGEN = "meldungen", AusDatei = "news.html";
    //private const string DBT1WOERTER = "woerter1";
    //private const string DBT2WOERTER = "woerter2";
    private const string DBT3WOERTER = "woerter3";
    private const string DBT4WOERTER = "woerter4";
    private const string DBT5WOERTER = "woerter5";
    //private const string DBT10WOERTER = "woerter10";
    private const string DB = "news", DBPORT = "3306";
    private const string DBUSER = "news", DBPWD = "WImGfKxkx2CQ0B9";
    private const int LIMIT =
#if DEBUG
      10;
#else
600;
#endif
    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    /// <exception cref="Exception"></exception>
    private static void Main(string[] args)
    {
      #region vorbereiten
      Dictionary<uint, (uint, string, string, string)> Aufgaben = new Dictionary<uint, (uint, string, string, string)>();
      bool neustart = false;
      string DBHOST = "localhost";
      //Aufgaben.Add(1, (1, DBT1WOERTER.Replace("oerter", "oerterNeu"), "w1", cTextauswertung.CREATE1WOERTER.Replace("oerter", "oerterNeu")));
      Aufgaben.Add(2, (3, DBT3WOERTER.Replace("oerter", "oerterNeu"), "w3", cTextauswertung.CREATE3WOERTER.Replace("oerter", "oerterNeu")));
      Aufgaben.Add(1, (5, DBT5WOERTER.Replace("oerter", "oerterNeu"), "w5", cTextauswertung.CREATE5WOERTER.Replace("oerter", "oerterNeu")));
      //Wortlänge.Add(4, (DBT4WOERTER, "w4", cTextauswertung.CREATE4WOERTER.Replace("oerter", "oerterNeu")));
      //Wortlänge.Add(10, (DBT10WOERTER, "w10", cTextauswertung.CREATE10WOERTER.Replace("oerter", "oerterNeu")));
      Assembly assembly = Assembly.GetExecutingAssembly();
      FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
      _ = fvi.CompanyName;
      _ = fvi.ProductName;
      string programmName = fvi.FileDescription;
      string productVersion = fvi.ProductVersion;
      Console.WriteLine($"{programmName} V{productVersion}");
      if (args.Length > 0)
      {
        DBHOST = args[0];
        neustart = (args.Length > 1 && args[1].StartsWith("neu"));
      }
      if (args.Length > 2)
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
      string CREATESTRING = null;
      Stopwatch stw = new Stopwatch();
      #endregion vorbereiten
      try
      {
        using (MySqlConnection con = new MySqlConnection(CONSTRING))
        using (MySqlCommand SQL =
          new MySqlCommand($"SELECT aufgabe FROM {DBTTASKS} WHERE programm= '{programmName}'", con))
        {
          #region tasks
          //try
          //{
          //  con.Open();
          //  Console.WriteLine($"Datenbank {con.Database} auf {con.DataSource}, Version {con.ServerVersion}");
          //  using (MySqlDataReader reader = SQL.ExecuteReader())
          //  {
          //    _ = reader.Read();
          //    if (!neustart&&reader.HasRows)
          //    {
          //      aufgabe = reader.GetUInt32("aufgabe");
          //    }
          //    else
          //    {
          //      aufgabe = 1;
          //      reader.Close();
          //      SQL.CommandText = $"insert into {DBTTASKS} (programm,aufgabe) values ('{programmName}',{aufgabe})";
          //      _ = SQL.ExecuteNonQuery();
          //    }
          //    reader.Close();
          //    SQL.CommandText = $"UPDATE {DBTTASKS} SET aufgabe={aufgabe + 1} WHERE programm='{programmName}'";
          //    _ = SQL.ExecuteNonQuery();
          //  }
          //}//try nur für Tasks
          //catch (MySqlException ex)
          //{
          //  if (MySQLHilfe.IfMySQLTabelleFehltExBeheben(ex, CONSTRING, cTextauswertung.CREATETASKS))
          //  {
          //    return;
          //  }
          //  else
          //  {
          //    throw ex;//2147467259 IPForWatsonBuckets = 0x0263d918 Number = 1054
          //  }
          //}
          #endregion tasks
          stw.Start();
          #region Aufgabe
          ////aufgabe 0...
          //if (aufgabe < 1 || aufgabe > MAXAUFGABE)
          //{
          //  SQL.CommandText = $"UPDATE {DBTTASKS} SET aufgabe=1 WHERE programm='{programmName}'";
          //  _ = SQL.ExecuteNonQuery();
          //  Console.WriteLine("Ablauf beginnt von vorne");
          //}
          //else
          HTMLerstellen(CONSTRING, DBTMELDUNGEN, AusDatei, programmName, productVersion);
          MldHilfe.Laufzeit(stw);

          #endregion

        }//using SQL
      }//try aussen
      catch (MySqlException ex)
      {
        if (!MySQLHilfe.IfMySQLTabelleFehltExBeheben(ex, CONSTRING, CREATESTRING))
        {
          Console.Error.WriteLine(ex.Message);
        }
      }//catch
      catch (Exception ex)
      {
        Console.Beep(880, 300);
        Console.Error.WriteLine($"{ex.Message}\n" +
        $"Connect String: {CONSTRING}");

      }
      Console.Beep(440, 400);
      Console.WriteLine("...fertig");
      _ = Console.ReadKey();
      return;
    }

    //private static double HTMLschreiben(string dBTabelle, MySqlCommand cmd)
    //{
    //  MySqlDataReader reader;
    //  uint types, token;
    //  double rfToken, abd;
    //  //Anzahl Wörter
    //  cmd.CommandText = $"SELECT datum,quelle,category,titel,meldung,link FROM {dBTabelle} WHERE wichtig>0";
    //  reader = cmd.ExecuteReader();
    //  _ = reader.Read();
    //  types = reader.GetUInt32("Types");
    //  Console.WriteLine($"----- Wortstatistik {dBTabelle.ToUpper()} -----\nTypes\t{types}");
    //  if (types < 1)
    //  {
    //    Console.WriteLine($"keine Wörter in Tabelle {dBTabelle}");
    //    return -1.0;
    //  }//entspricht if (reader.HasRows)
    //   //Summe Wörter
    //  token = reader.GetUInt32("Token");
    //  abd = types / (double)token;
    //  Console.WriteLine($"Token\t{token}\nAbdeckung\t{abd:P0}");
    //  rfToken = 1.0 / token;
    //  reader.Close();
    //  return rfToken;
    //}

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