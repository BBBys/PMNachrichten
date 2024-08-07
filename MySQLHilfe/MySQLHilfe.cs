using MySqlConnector;
using System;
using System.Text.RegularExpressions;

namespace Borys.Hilfe
{
  public class MySQLHilfe
  {
    public static string DBTCREATEWÖRTER =
  @" CREATE TABLE `TABNAME` (
 `id` int(11)                   NOT NULL AUTO_INCREMENT,
 `wort` tinytext              DEFAULT NULL  COMMENT 'Wort/Wortfolge',
 `anzahl` int (11) unsigned   DEFAULT 0     COMMENT 'absolute Anzahl',
 `gesamt` int (10) unsigned   DEFAULT NULL  COMMENT 'Gesamtzahl, auf die sich rel. H. bezieht',
 `rang` int (11) unsigned     DEFAULT NULL  COMMENT 'Rang nach Anzahl',
 `relh` double unsigned       DEFAULT NULL  COMMENT 'relative Häufigkeit',
 `entr` float unsigned        DEFAULT NULL  COMMENT 'Wort-Entropie',
 `beitrag` float unsigned     DEFAULT NULL  COMMENT 'Beitrag zum mittl. Wortentropie (pro Wortkette)',
 `zeichen` tinyint(3) unsigned DEFAULT NULL COMMENT 'Anz. Zeichen in Kette',
 PRIMARY KEY(`id`),
 UNIQUE KEY `wort` (`wort`) USING HASH
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE = utf8mb4_general_ci 
COMMENT='abs. Häufigkeit Wörter'";
    public static string DBTCREATEMELDUNGEN =
 @" CREATE TABLE `meldungen` (
 `id` int (11) NOT NULL AUTO_INCREMENT,
 `datum` timestamp NOT NULL DEFAULT current_timestamp(),
 `quelle` tinytext DEFAULT NULL COMMENT 'Quelle der Meldung',
 `category` tinytext DEFAULT NULL,
 `titel` tinytext NOT NULL,
 `rohmeldung` text DEFAULT NULL,
 `link` tinytext DEFAULT NULL,
 `datenID` int (11) DEFAULT NULL,
 PRIMARY KEY(`id`)
) ENGINE=InnoDB AUTO_INCREMENT = 2134341416 
DEFAULT CHARSET = utf8mb4 COLLATE=utf8mb4_general_ci";
    public static string DBTCREATEDATEN =
@"CREATE TABLE `daten` (
 `id` int (11) NOT NULL AUTO_INCREMENT,
 `mldid` int (11) DEFAULT NULL COMMENT 'id der Meldung',
 `stopfrei` text DEFAULT NULL COMMENT 'von Stoppwörtern befreit',
 `w1` tinyint(1) NOT NULL DEFAULT 0 COMMENT 'Einzelwörter erfasst',
 `w3` tinyint(1) NOT NULL DEFAULT 0 COMMENT '3-Wörter erfasst',
 `w5` tinyint(1) NOT NULL DEFAULT 0 COMMENT '5-Wörter erfasst',
 `entr` float DEFAULT NULL COMMENT 'Meldungsentropie',
 `buchstaben` int (11) DEFAULT 0,
 `woerter` tinyint(3) DEFAULT 0,
 PRIMARY KEY(`id`)
) ENGINE=InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8mb4 
COLLATE=utf8mb4_general_ci";
    public static string DBTCREATEBB = " CREATE TABLE `blackboard` (" +
" `id`        int (10) unsigned NOT NULL AUTO_INCREMENT," +
" `programm`  tinytext DEFAULT NULL," +
" `parameter` tinytext DEFAULT NULL," +
" `zeit`      datetime NOT NULL DEFAULT current_timestamp()," +
" PRIMARY KEY(`id`))" +
" ENGINE=InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8mb4 COLLATE=utf8mb4_general_ci;";

    public static string PfadRaus(string mitPfad)
      => Regex.Replace(
      mitPfad,
      @"\\",
      @"%BSL%");
    public static string PfadRein(string ohnePfad)
         => Regex.Replace(
         ohnePfad,
         "%BSL%",
         "\\");

    /// <summary>
    /// Verbindungsparameter
    /// </summary>                                                               
    private const string DB = "news", DBPORT = "3306", DBUSER = "news", DBPWD = "WImGfKxkx2CQ0B9";
    /// <summary>
    /// Tabellen
    /// </summary>
    public const string DBTMELDUNGEN = "meldungen";
    public const string DBTDATEN = "daten";
    public const string DBT1WÖRTER = "woerter1";
    public const string DBT3WÖRTER = "woerter3";
    public const string DBT5WÖRTER = "woerter5";
    public const string DBTBB = "blackboard";
    public const string DBTWICHTIGE = "wichtige";
    /// <summary>
    /// Verbindung zu DBHOST herstellen
    /// alle anderen Param optional
    /// </summary>
    /// <param name="host">DBHOST</param>
    /// <param name="db"></param>
    /// <param name="dbuser"></param>
    /// <param name="dbport"></param>
    /// <param name="dbpwd"></param>
    /// <returns></returns>
    public static MySqlConnection ConnectToDB(
      string host,
      string db = DB,
      string dbuser = DBUSER,
      uint dbport = 3306,
      string dbpwd = DBPWD) => new MySqlConnection(
          $@"Server=   {host};
          database= {db};
          user=     {dbuser};
          port=     {dbport}; 
          password= '{dbpwd}' ");


    public static bool IfMySQLFeldFehlt(MySqlException ex) => ex.Number == 1054;
    public static bool IfMySQLSytaxError(MySqlException ex) => ex.Number == 1064;
    public static bool IfMySQLKeyDoppeltEx(MySqlException ex) => ex.Number == 1062;
    public static bool IfMySQLTabelleFehltEx(MySqlException ex) => ex.Number == 1146;
    public static bool IfMySQLTabelleFehltExBeheben(MySqlException ex, MySqlConnection con)
    {
      if (ex.Message.Contains("news.daten"))
      { MSTF(ex, con, DBTCREATEDATEN); return true; }
      if (ex.Message.Contains("woerter"))
      {
        //      string[] spl = ex.Message.Split(Convert.ToChar("'"));
        string name = WelcheTabelleFehlt(ex);
        string cre = DBTCREATEWÖRTER.Replace("TABNAME", name);
        MSTF(ex, con, cre);
        return true;
      }
      return false;
    }

    public static string WelcheTabelleFehlt(MySqlException ex)
    {
      string[] spl = ex.Message.Split('.');
      string[] spl2 = spl[1].Split(Convert.ToChar("'"));
      return spl2[0];
    }

    /// <summary>
    /// behebt "Tabelle fehlt"
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="strConnect"></param>
    /// <param name="strCreate"></param>
    /// <returns></returns>
    public static bool IfMySQLTabelleFehltExBeheben(
      MySqlException ex,
      string strConnect,
      string strCreate)
    {
      if (IfMySQLTabelleFehltEx(ex))
      {
        using (MySqlConnection con = new MySqlConnection(strConnect))
        {
          con.Open();
          MSTF(ex, con, strCreate);
        }
        return true;
      }
      else
        return false;
    }

    private static void MSTF(MySqlException ex, MySqlConnection con, string strCreate)
    {
      using (MySqlCommand SQL = new MySqlCommand(strCreate, con))
      {
        Console.Error.WriteLine(ex.Message);
        _ = SQL.ExecuteNonQuery();
        Console.Error.WriteLine("Tabelle erzeugt\nNeustart notwendig");
      }
    }
  }
}