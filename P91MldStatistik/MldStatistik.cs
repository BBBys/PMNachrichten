using MySqlConnector;
using System;
using System.Diagnostics;
using System.Reflection;

namespace Borys.Nachrichten
{
  internal class MldStatistik
  {
    private const string PARAMFEHLER = "Fehler:\nmaximal 1 Argument";
    private const string DBTMELDUNGEN = "meldungen";
    //private const string DBT1WOERTER = "woerter1";
    //private const string DBT2WOERTER = "woerter2";
    //private const string DBT3WOERTER = "woerter3";
    //private const string DBT4WOERTER = "woerter4";
    //private const string DBT5WOERTER = "woerter5";
    private const string DB = "news", DBPORT = "3306";
    private const string DBUSER = "news", DBPWD = "WImGfKxkx2CQ0B9";

    private static void Main(string[] args)
    {
      FileVersionInfo fvi;
      Assembly ass;
      string DBHOST, CONSTRING, pn, pv;
      ass = Assembly.GetExecutingAssembly();
      fvi = FileVersionInfo.GetVersionInfo(ass.Location);
      pn = fvi.FileDescription; //Assemblyinfo -> Titel
      pv = fvi.ProductVersion; //Version  1.2.3.4 
      Console.WriteLine($"{pn} V{pv}");
      DBHOST = "localhost";
      if (args.Length > 0)
      {
        DBHOST = args[0];
        if (args.Length > 1)
        {
          Console.Error.WriteLine(PARAMFEHLER);
          Console.Beep(440, 300);
          Console.Beep(880, 200);
          _ = Console.ReadKey();
          throw new Exception(PARAMFEHLER);
        }
      }
      CONSTRING =
           $"Server=   {DBHOST}; " +
           $"database= {DB};" +
           $"user=     {DBUSER};" +
           $"port=      {DBPORT}; " +
           $"password=  '{DBPWD}' ";
      //string connectionString = "Server=IhrServer;Database=IhreDatenbank;Uid=IhrBenutzername;Pwd=IhrPasswort;";
      using (MySqlConnection conMld = new MySqlConnection(CONSTRING))
      using (MySqlCommand SQL =
        //        new MySqlCommand($"UPDATE {DBTMELDUNGEN} SET entr=-1 ", conMld))
        new MySqlCommand($"SELECT COUNT(meldung) AS anz, MIN(datum) AS erste, MAX(datum) AS letzte FROM {DBTMELDUNGEN} ", conMld))
      {
        try
        {
          double mine=0, maxe=0, mitte = 0;
          uint anz = 0;
          DateTime von, bis;
          MySqlDataReader reader;
          conMld.Open();
          //conWrt.Open();
          reader = SQL.ExecuteReader();
          _ = reader.Read();
          anz = reader.GetUInt32("anz");
          von = reader.GetDateTime("erste");
          bis = reader.GetDateTime("letzte");
          reader.Close();
          Console.WriteLine($"\t{anz,5}\tMeldungen\nvon\t{von}\nbis\t{bis}");

          SQL.CommandText = $"SELECT COUNT(meldung) AS anz,MIN(entr) AS min, MAX(entr) AS max, AVG(entr) AS mittel FROM {DBTMELDUNGEN} WHERE entr>0 ";
          reader = SQL.ExecuteReader();
          _ = reader.Read();
        anz = reader.GetUInt32("anz");if (anz > 0)
          {
            mine = reader.GetFloat("min");
            maxe = reader.GetFloat("max");
            mitte = reader.GetFloat("mittel");
            Console.WriteLine($"Entropie\n\t{anz,5} x berechnet\nvon\t{mine,7:N1}\nMittel\t{mitte,7:N1}\nbis\t{maxe,7:N1}");
          }else
            Console.WriteLine("keine Entropie berechnet");
          reader.Close();
        }
        catch (Exception ex)
        {
          Console.Beep(880, 300);
          Console.Error.WriteLine($"{ex.Message}");
        }
      }

      Console.Beep(440, 400);
      Console.WriteLine("...fertig");
      _ = Console.ReadKey();
    }
  }
}