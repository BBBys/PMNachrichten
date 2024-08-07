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
  internal partial class MldWählen
  {
    private const string PARAMFEHLER = "Fehler:\nmaximal 1 Argument";
    private const string DBTMELDUNGEN = "meldungen";
    private const string DBT3WOERTER = "woerter3";
    private const string DBT5WOERTER = "woerter5";
    private const string DBTWICHTIGE = "wichtige";
    private const string DB = "news", DBPORT = "3306";
    private const string DBUSER = "news", DBPWD = "WImGfKxkx2CQ0B9";
    /// <summary>
    /// Meldungen nach Bestimmung der wichtigsten Wörter auswählen
    /// </summary>
    /// <param name="args">" 
    ///     1. Parameter:    DB-Host
    /// </param>
    /// <exception cref="Exception"></exception>
    private static void Main(string[] args)
    {
      Dictionary<uint, (uint, string, string, string)> Aufgaben = new Dictionary<uint, (uint, string, string, string)>();
      string DBHOST = "localhost";
      Aufgaben.Add(1, (0, DBTWICHTIGE, "", cTextauswertung.CREATEWICHTIGE));
      Aufgaben.Add(3, (3, DBT3WOERTER, "w3", cTextauswertung.CREATE3WOERTER));
      Aufgaben.Add(2, (5, DBT5WOERTER, "w5", cTextauswertung.CREATE5WOERTER));
      Assembly assembly = Assembly.GetExecutingAssembly();
      FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
      string programmName = fvi.FileDescription;
      string productVersion = fvi.ProductVersion;
      if (args.Length > 0)
        DBHOST = args[0];
      Console.WriteLine($"{programmName} V{productVersion}");
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
      try
      {
        using (MySqlConnection con1 = new MySqlConnection(CONSTRING),
          con2 = new MySqlConnection(CONSTRING),
          con3 = new MySqlConnection(CONSTRING))
        using (MySqlCommand SQL1 = new MySqlCommand("", con1),
          SQL2 = new MySqlCommand("", con2),
          SQL3 = new MySqlCommand("", con3))
        {
          stw.Start();
          #region Aufgabe
          con1.Open();
          con2.Open();
          con3.Open();
          if (!Wählen(SQL1, SQL2, DBTWICHTIGE, DBTMELDUNGEN, CONSTRING))
            Console.WriteLine($"keine Wörter in {con1.DataSource}/{con1.Database}.{DBTWICHTIGE.ToUpper()}");
#pragma warning fehlt truncate table wichtige
          con1.Close();
          con2.Close();
          con3.Close();
          MldHilfe.Laufzeit(stw);
          #endregion
        }
      }//try 
      catch (MySqlException ex)
      {
        if (!Hilfe.MySQLHilfe.IfMySQLTabelleFehltExBeheben(ex, CONSTRING, CREATESTRING))
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
    //private static (uint, double) WörterÜbersicht(string dBTabelle, MySqlCommand cmd, string nulltest = "")
    //{
    //  string testen;

    //  uint types, token;
    //  double rfToken, abd;
    //  testen = (nulltest.Length > 1) ? $"AND '{nulltest}' IS NOT null" : "";
    //  //Anzahl Wörter
    //  cmd.CommandText = //"SELECT COUNT(`anzahl`) as Types FROM `woerterNeu3` WHERE anzahl> 0 and `relh` IS NOT null;";
    //  $"SELECT COUNT(anzahl) AS Types, " +
    //  $"       SUM(anzahl)   AS Token " +
    //  $"FROM {dBTabelle} WHERE (anzahl>0 {testen})";
    //  using (MySqlDataReader reader = cmd.ExecuteReader())
    //  {
    //    _ = reader.Read();
    //    types = reader.GetUInt32("Types");
    //    Console.WriteLine($"----- Wortstatistik {dBTabelle.ToUpper()} -----\n{types,5}\tTypes");
    //    if (types < 1)
    //    {
    //      Console.WriteLine($"keine Wörter in Tabelle {dBTabelle}");
    //      return (0, 0);
    //    }//entspricht if (reader.HasRows)
    //     //Summe Wörter
    //    token = reader.GetUInt32("Token");
    //    abd = types / (double)token;
    //    Console.WriteLine($"{token,5}\tToken\n{abd,7:P0}\tWiederholungen");
    //    rfToken = 1.0 / token;
    //    reader.Close();
    //    return (types, rfToken);
    //  }
    //}
  }
}