using MySqlConnector;
using System;
using System.Diagnostics;
using System.Reflection;
namespace Borys.Nachrichten
{
  internal partial class WartMain
  {
    /// <summary>
    /// Wartung der DB
    /// </summary>
    /// <param name="args">" 
    ///     1. Parameter:    DB-Host
    ///     2. Parameter:    was
    ///                     -r Tabelle Meldungen, Hash neu berechnen
    ///                     -1 Wortliste 1 zurücksetzen
    ///                     -b Blackboard anlegen
    ///                     -p prüfen
    /// </param>
    private const string PARAMFEHLER = "Fehler:\n1 oder 2 Argumente:\n1.\twas\t[-r]\n\t\t[-w]\n[2.\tDB-Host localhost]";
    private const string DBTMELDUNGEN = "meldungen";
    private const string DBTWOERTER = "woerter1";
    //private const string DB           = "news", DBPORT = "3306";
    //private const string DBUSER       = "news", DBPWD = "WImGfKxkx2CQ0B9";

    /// <summary>
    /// Ausführung Wartung
    /// </summary>
    /// <param name="args"></param>
    private static void Main(string[] args)
    {
      FileVersionInfo fvi;
      Assembly assembly;
      string was, titel, DBHOST;
      assembly = Assembly.GetExecutingAssembly();
      fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
      titel = fvi.FileDescription; //Assemblyinfo -> Titel
      Console.WriteLine($"{titel} V{fvi.ProductVersion}");
      Console.WriteLine(fvi.Comments);
      Console.Title = titel;
      DBHOST = args.Length > 1 ? args[1] : "localhost";
      was = args.Length > 0 ? args[0] : "";
      if (args.Length > 2 || args.Length < 1)
      {
        Console.Error.WriteLine(PARAMFEHLER);
        Console.Beep(880, 300);
      }
      try
      {
        using (MySqlConnection con = Hilfe.MySQLHilfe.ConnectToDB(DBHOST),
           con2 = Hilfe.MySQLHilfe.ConnectToDB(DBHOST))
        {
          con.Open();
          con2.Open();
          switch (was)
          {
            case "-b":
              DBBlackboard(con, Hilfe.MySQLHilfe.DBTCREATEBB);
              break;

            case "-p":
              Prüfen(con, con2, Hilfe.MySQLHilfe.DBTBB);
              break;
            case "-r":
              DBReHash(con, DBTMELDUNGEN);
              break;
            //case eOperation.eoKopieren:
            //  DBKopieren(con, DBTMELDUNGEN, null);
            //  break;
            default:
              throw new NotImplementedException("nicht implementiert: Parameter " + was);
              break;
          }//switch
          con.Close();
          con2.Close();
        }
      }
      catch (MySqlException ex)
      {//access: 1045, state 28000
        //alr exis: 1050, 42S01
        Console.Beep(880, 300);
        Console.Error.WriteLine($"{ex.Message}");
      }
      catch (Exception ex)
      {
        Console.Beep(880, 300);
        Console.Error.WriteLine($"{ex.Message}");
      }
      Console.Beep(440, 200);
      Console.WriteLine("...fertig");
      _ = Console.ReadKey();
      return;
    }

    private static void Prüfen(MySqlConnection conEin, MySqlConnection conAus, string dbTBb)
    {

      MySqlDataReader reader = null;
      Console.WriteLine("DB? OK");
      Console.Write("Blackboard?");
      using (MySqlCommand SQL = new MySqlCommand(string.Empty, conEin),
        SQL2 = new MySqlCommand(string.Empty, conAus))
      {
        bool hatSp, hatTs;
        uint max, min, neu, alt;
        hatSp = false;
        hatTs = false;
        try
        {
          string parameter;
          //erste Prüfung: gibt es Start-Einträge, mindestens je einen für Spiegel und Tagesschau?
          SQL.CommandText = $"SELECT parameter FROM {dbTBb} WHERE programm='P0Abrufen'";
          reader = SQL.ExecuteReader();
          if (reader.HasRows)
            while (reader.Read() && !(hatSp && hatTs))
            {
              parameter = reader.GetString(0);
              hatSp = parameter.ToUpper().StartsWith("SPIEG") || hatSp;
              hatTs = parameter.ToUpper().StartsWith("TAGES") || hatTs;
            }
          reader.Close();
          if (!hatTs)
            AddBBEntry(SQL, "Tagesschau");
          if (!hatSp)
            AddBBEntry(SQL, "Spiegel");
          //zweite Prüfung: wie groß ist ID?
          SQL.CommandText = $"SELECT MAX(id) AS MAX, MIN(id) AS MIN FROM {dbTBb}";
          reader = SQL.ExecuteReader();
          reader.Read();
          max = reader.GetUInt32("MAX");
          min = reader.GetUInt32("MIN");
          reader.Close();
          Console.WriteLine($"ID läuft von {min} bis {max}");
          if (max > 300 && min > 30)
          {
            Console.WriteLine("... wird zusammengeschoben");
            SQL.CommandText = $"SELECT id AS ALT FROM {dbTBb} ORDER BY id ASC";
            reader = SQL.ExecuteReader();
            neu = 0;
            while (reader.Read())
            {
              alt = reader.GetUInt32("ALT");
              SQL2.CommandText = $"UPDATE {dbTBb} SET id={neu} WHERE id={alt}";
              SQL2.ExecuteNonQuery();
              neu++;
            }
          }

        }//try
        catch (MySqlException ex)
        {
          if (Hilfe.MySQLHilfe.IfMySQLTabelleFehltEx(ex))
          {
            Console.Write(" fehlt!");
            reader?.Close();

            DBBlackboard(conEin);
            Console.WriteLine(" erzeugt");
            }
          else
            throw ex;
        }
      }
    }

    private static void DBBlackboard(MySqlConnection con) => DBBlackboard(con, Hilfe.MySQLHilfe.DBTCREATEBB);

    /// <summary>
    /// Tab Blackboard erzeugen
    /// blackboard: (`id`,`programm`,`parameter`,`zeit`)
    /// </summary>
    /// <param name="con"></param>
    /// <param name="create"></param>
    /// 
    private static void DBBlackboard(
      MySqlConnection con, string create)
    {
      using (MySqlCommand cmd = new MySqlCommand(create, con))
      {
        try
        {
          _ = cmd.ExecuteNonQuery();
        }
        catch (MySqlException ex)
        {
          throw ex;
        }
        AddBBEntry(cmd, "Spiegel", 1);
        AddBBEntry(cmd, "Tagesschau", 2);
      }
    }

    /// <summary>
    /// Parameter für Spiegel- oder Tagesschau-Abruf in Blackboad eintragen
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="param">Spiegel oder Tagesschau</param>
    /// <param name="id">wenn nicht angegeben: Autoinkrement</param>
    /// <param name="dBTab">Blackboard</param>
    private static void AddBBEntry(
      MySqlCommand cmd,
      string param,
      uint id = 0,
      string dBTab = Hilfe.MySQLHilfe.DBTBB)
    {
      cmd.CommandText = (id > 0)
              ?
              $"insert into {dBTab} (id,`programm`,`parameter`,zeit) values ({id},'P0Abrufen','{param}',0)"
              :
              $"insert into {dBTab} (`programm`,`parameter`,zeit) values ('P0Abrufen','{param}',0)";
      _ = cmd.ExecuteNonQuery();
      Console.WriteLine($"Eintrag P0Abrufen {param} in {dBTab} erzeugt");
    }
  }
}