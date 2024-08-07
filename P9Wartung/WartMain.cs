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
        using (MySqlConnection con = Hilfe.MySQLHilfe.ConnectToDB(DBHOST))
        {
          //con.Open();
          switch (was)
          {
            case "-b":
              DBBlackboard(con, Hilfe.MySQLHilfe.DBTBB, Hilfe.MySQLHilfe.DBTCREATEBB);
              break;

            case "-p":
              Prüfen(con, Hilfe.MySQLHilfe.DBTBB);
              break;
            case "-r":
              DBReHash(con, DBTMELDUNGEN);
              break;
            //case eOperation.eoKopieren:
            //  DBKopieren(con, DBTMELDUNGEN, null);
            //  break;
            default:
              throw new NotImplementedException("nicht implementiert: " + was);
              break;
          }//switch
          con.Close();
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

    private static void Prüfen(MySqlConnection con, string dbTBb)
    {
      bool bbtest, hatSp, hatTs;
      MySqlDataReader reader = null;
      Console.WriteLine("DB? OK");
      Console.Write("Blackboard?");
      using (MySqlCommand cmd = new MySqlCommand($"select parameter from {dbTBb} where programm='P0Abrufen'", con))
      {
        bbtest = true;
        while (bbtest)
        {
          bbtest = false;
          hatSp = false;
          hatTs = false;
          try
          {
            string parameter;
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
              for (int i = 0; i < 2; i++)
              {
                reader.Read();
                parameter = reader.GetString(0);
                if (parameter.ToUpper().StartsWith("SPIEG"))
                  hatSp = true;
                if (parameter.ToUpper().StartsWith("TAGES"))
                  hatTs = true;
              }
            }
            reader.Close();
            if (!hatTs)
            {
              AddBBEntry(cmd, "Tagesschau");
              bbtest = true;
            }
            if (!(hatTs && hatSp))
            {
              AddBBEntry(cmd, "Spiegel");
              bbtest = true;
            }
          }
          catch (MySqlException ex)
          {
            if (Hilfe.MySQLHilfe.IfMySQLTabelleFehltEx(ex))
            {
              Console.Write(" fehlt!");
              reader?.Close();

              DBBlackboard(con);
              Console.WriteLine(" erzeugt");
              bbtest = true;
            }
            else
              throw ex;
          }
        }
      }
    }

    private static void DBBlackboard(MySqlConnection con) => DBBlackboard(con, Hilfe.MySQLHilfe.DBTBB, Hilfe.MySQLHilfe.DBTCREATEBB);

    /// <summary>
    /// Tab Blackboard erzeugen
    /// blackboard: (`id`,`programm`,`parameter`,`zeit`)
    /// </summary>
    /// <param name="con"></param>
    /// <param name="dBTab"></param>
    /// <param name="create"></param>
    private static void DBBlackboard(
      MySqlConnection con, string dBTab, string create)
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

    private static void AddBBEntry(MySqlCommand cmd, string param, uint id = 0, string dBTab = Hilfe.MySQLHilfe.DBTBB)
    {
      if (id > 0)
        cmd.CommandText =
                $"insert into {dBTab} (id,`programm`,`parameter`,zeit) values ({id},'P0Abrufen','{param}',0)";
      else
        cmd.CommandText =
               $"insert into {dBTab} (`programm`,`parameter`,zeit) values ('P0Abrufen','{param}',0)";
      _ = cmd.ExecuteNonQuery();
    }
  }
}