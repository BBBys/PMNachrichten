using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Borys.Nachrichten
{
  internal partial class WichtigeMld
  {
    private static Dictionary<string, string> GetVariablenVonReader(
      MySqlDataReader reader,
      Dictionary<string, string> dict)
    {
      for (int i = 0; i < reader.FieldCount; i++)
        dict.Add(reader.GetName(i), reader.GetValue(i).ToString());
      return dict;
    }
    private static void HTMLerstellen(
      string programmName,
      string produktVersion,
      string ConnectionString,
      string dBTMeldungen,
      string AusDatei,
      string WebseitenVerzeichnis)
    {
      string StylesheetVerzeichnis, StyleFarben, Stylesheet;
      FileStream fs;
      Dictionary<string, string> MldVariablen = new Dictionary<string, string>(),
        HeadFootVariablen = new Dictionary<string, string>();
      if (!File.Exists(WebseitenVerzeichnis))
        Directory.CreateDirectory(WebseitenVerzeichnis);
      StylesheetVerzeichnis = Path.Combine(WebseitenVerzeichnis, Properties.Resources.cssDir);
      StyleFarben = Path.Combine(StylesheetVerzeichnis, Properties.Resources.farbenDatei);
      Stylesheet = Path.Combine(StylesheetVerzeichnis, Properties.Resources.stilDatei);
      if (!File.Exists(StylesheetVerzeichnis))
        Directory.CreateDirectory(StylesheetVerzeichnis);
      if (!File.Exists(StyleFarben))
        using (StreamWriter Str = new StreamWriter(StyleFarben))
          Str.Write(Properties.Resources.farben);
      if (!File.Exists(Stylesheet))
        using (StreamWriter Str = new StreamWriter(Stylesheet))
          Str.Write(Properties.Resources.stil);
      using (MySqlConnection con = new MySqlConnection(ConnectionString))
      using (MySqlCommand SQL = new MySqlCommand("", con))
      {
        con.Open();
        MySqlDataReader rMeld;
        SQL.CommandText = $"SELECT * FROM {dBTMeldungen} WHERE wichtig>0";
        rMeld = SQL.ExecuteReader();
        if (rMeld.HasRows)
        {
          fs = new FileStream(Path.Combine(WebseitenVerzeichnis, AusDatei), FileMode.Create);
          using (StreamWriter EinStr = new StreamWriter(fs))
          {
            int n = 0;
            string result;
            HeadFootVariablen.Add("name", programmName);
            HeadFootVariablen.Add("version", produktVersion);
            result = Regex.Replace(
              Properties.Resources.Header,
              @"\$(\w+)\$",
              match => HeadFootVariablen[match.Groups[1].Value]);
            EinStr.Write(result);
            while (rMeld.Read())
            {
              n++;
              MldVariablen = GetVariablenVonReader(rMeld, MldVariablen);
              MldVariablen.Add("nummer", n.ToString());
              result = Regex.Replace(
                Properties.Resources.Body,
                @"\$(\w+)\$",
                match => MldVariablen[match.Groups[1].Value]);
              EinStr.Write(result);
              MldVariablen.Clear();
#if DEBUG
              if (n > 3)
                break;
#endif
            }
            HeadFootVariablen.Add("footer", $"...Ende {DateTime.Now}");
            HeadFootVariablen.Add("nummer", n.ToString());
            result = Regex.Replace(Properties.Resources.Footer,
                                   @"\$(\w+)\$",
                                   match => HeadFootVariablen[match.Groups[1].Value]);
            EinStr.Write(result);
          }
        }
        else
        {
          Console.WriteLine($"keine Einträge im Zeitraum in {SQL.CommandText}");
          return;
        }
        con.Close();
      }
    }
    private const string PARAMFEHLER = "Fehler:\nmaximal 1 Argument";
    private const string DBTTASKS = "tasks";
    private const string DBTMELDUNGEN = "meldungen", AusDatei = "news.html";
    private const string DBT3WOERTER = "woerter3";
    private const string DBT5WOERTER = "woerter5";
    private const string DB = "news", DBPORT = "3306";
    private const string DBUSER = "news", DBPWD = "WImGfKxkx2CQ0B9";
    /// <summary>
    /// aus Tabelle markierte Meldungen (titel, description) extrahieren 
    /// und in HTML-Seite aufbauen
    /// </summary>
    /// <param name="args">" 
    ///     1. Parameter:    DB-Host
    ///     2. Parameter:    HTML-Verzeichnis
    /// </param>
    /// <exception cref="Exception"></exception>
    private static void Main(string[] args)
    {
      #region vorbereiten
      string DBHOST = "localhost", WWW = ".";
      Assembly assembly = Assembly.GetExecutingAssembly();
      FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
      string programmName = fvi.FileDescription;
      string productVersion = fvi.ProductVersion;
      Console.WriteLine($"{programmName} V{productVersion}");
      if (args.Length > 0)
        DBHOST = args[0];
      if (args.Length > 1)
        WWW = args[1];
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
      Stopwatch stw = new Stopwatch();
      #endregion vorbereiten
      try
      {
        using (MySqlConnection con = new MySqlConnection(CONSTRING))
        using (MySqlCommand SQL =
          new MySqlCommand($"SELECT aufgabe FROM {DBTTASKS} WHERE programm= '{programmName}'", con))
        {
          HTMLerstellen(programmName, productVersion, CONSTRING, DBTMELDUNGEN, AusDatei, WWW);
          MldHilfe.Laufzeit(stw);
        }//using SQL
      }//try aussen
      catch (MySqlException ex)
      {
        Console.Error.WriteLine(ex.Message);
      }
      catch (Exception ex)
      {
        Console.Beep(880, 300);
        Console.Error.WriteLine($"{ex.Message}\n" +
        $"Connect String: {CONSTRING}");
      }
      Console.Beep(440, 400);
      Console.WriteLine("...fertig");
      return;
    }
  }
}