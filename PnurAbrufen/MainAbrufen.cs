using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
//using static cTextauswertung;
namespace Borys.Nachrichten
{
  internal class MainAbrufen
  {
    private const string RssUrlSp = "https://www.spiegel.de/schlagzeilen/index.rss";
    private const string RssUrlTs = "https://www.tagesschau.de/newsticker.rdf";
    private const string ParamAus = "rss.rss";
    private const string FEHLER = "Fehler:\nentweder 1 oder 2 Parameter";
    /// <summary>
    /// RSS-Feeds öffnen; 
    /// NUR LESEN und Ergebnis in AusgabeDATEI speichern. 
    /// </summary>
    /// <param name="args">" 
    /// 2 Parameter: 
    ///     1.  RSS-URL
    ///         oder "SP"
    ///         oder "TS"
    ///     2. Ausgabedatei
    /// </param>
    private static void Main(string[] args)
    {
      FileVersionInfo fvi;
      Assembly assembly;
      string ausDatei, titel, PFAD, DBHOST;
      assembly = Assembly.GetExecutingAssembly();
      fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
      titel = fvi.FileDescription; //Assemblyinfo -> Titel
      Console.WriteLine($"{titel} V{fvi.ProductVersion}");
      Console.WriteLine(fvi.Comments);
      Console.Title = titel;
      string RSSURL;
      if (args.Length > 2 || args.Length < 1)
      {
        Console.Error.WriteLine(FEHLER);
        Console.Beep(440, 200);
        Console.Beep(880, 300);
        _ = Console.ReadKey();
        throw new Exception(FEHLER);
        //endet hier
      }
      DBHOST = args.Length > 0 ? args[0] : "localhost";
      PFAD = args.Length > 1 ? args[1] : ParamAus;
      using (MySqlConnection con = Hilfe.MySQLHilfe.ConnectToDB(DBHOST + ";ConvertZeroDateTime=True;"))//  AllowZeroDateTime=True; "))
      {
        string ABFRAGE;
        ABFRAGE = $"SELECT id,parameter,zeit FROM {Hilfe.MySQLHilfe.DBTBB} WHERE programm='{titel}'";
        using (MySqlCommand SQL = new MySqlCommand(ABFRAGE, con))
          try
          {
            uint id;
            string ohneBacksl, welchesMedium;
            DateTime jetzt, letzterLauf;
            TimeSpan abstand;
            con.Open();
            MySqlDataReader reader = SQL.ExecuteReader();
            if (!(reader.Read() && reader.Read()))//es muss 2 Records geben
            {
              reader.Close();
              SQL.CommandText = $"insert into {Hilfe.MySQLHilfe.DBTBB} (programm,parameter) values ('{titel}','Spiegel')";
              _ = SQL.ExecuteNonQuery();
              SQL.CommandText = $"insert into {Hilfe.MySQLHilfe.DBTBB} (programm,parameter) values ('{titel}','Tagesschau')";
              _ = SQL.ExecuteNonQuery();
              throw new Exception($"Tabelle {Hilfe.MySQLHilfe.DBTBB} Einträge {titel} erzeugt - Neustart notwendig\n");
              // endet hier
            }
            reader.Close();
            reader = SQL.ExecuteReader();
            _ = reader.Read();
#if DEBUG
            abstand = TimeSpan.FromHours(0);
#else            
abstand = TimeSpan.FromHours(6);
#endif
            jetzt = DateTime.Now;
            letzterLauf = reader.GetDateTime("zeit");
            if ((jetzt - letzterLauf) < abstand)//es ist noch nicht so weit
            {
              _ = reader.Read();
              letzterLauf = reader.GetDateTime("zeit");
              if ((jetzt - letzterLauf) < abstand)//auch hierfür es ist noch nicht so weit
              { Console.WriteLine("Zeitpunkt noch nicht erreicht"); return; }
            }
            welchesMedium = reader.GetString("parameter");
            id = reader.GetUInt32("id");
            reader.Close();
            RSSURL = welchesMedium.ToUpper().StartsWith("SPIEGEL") ? RssUrlSp : RssUrlTs;
            ausDatei = Path.Combine(PFAD, $"{welchesMedium}{id}");
            ausDatei = Path.ChangeExtension(ausDatei, "rss");
            Console.Write($"Ausgabe auf {ausDatei}... ");
            using (StreamWriter StrAus = new StreamWriter(ausDatei, false))
              Abfrage(RSSURL, StrAus);
            Console.WriteLine("...OK");
            SQL.CommandText = $"delete FROM {Hilfe.MySQLHilfe.DBTBB} WHERE id={id}";
            _ = SQL.ExecuteNonQuery();
            SQL.CommandText = $"insert into {Hilfe.MySQLHilfe.DBTBB} (programm,parameter) values ('{titel}','{welchesMedium}')";
            _ = SQL.ExecuteNonQuery();
            ohneBacksl = Hilfe.MySQLHilfe.PfadRaus(ausDatei);
            SQL.CommandText =
              $"insert into {Hilfe.MySQLHilfe.DBTBB} (programm,parameter) values ('P2Eintragen','{ohneBacksl}')";
            _ = SQL.ExecuteNonQuery();
          }
          catch (Exception ex) { Console.Error.Write(ex.Message); throw ex; }
      }
      Console.Beep(440, 200);
      Console.WriteLine("...fertig");
      _ = Console.ReadKey();
      return;
    }
    /// <summary>
    /// diese Version speichert den Text
    /// </summary>
    /// <param name="RssUrl"></param>
    /// <param name="aus"></param>
    private static void Abfrage(string RssUrl, StreamWriter aus)
    {
      int i = 0;
      Console.Write(" - lesen");
      List<string> lNachrichten = cTextauswertung.NachrichtenLesen(RssUrl);
      Console.Write($"-{lNachrichten.Count} gelesen\nschreiben");
      foreach (string s0 in from string s0 in lNachrichten
                            where s0 != null
                            select s0)
      {
        aus.WriteLine(s0);
        if (++i % 50 == 0)
          Console.Write(".");
      }
      lNachrichten.Clear();
      Console.WriteLine(" - geschrieben");
    }
  }
}
