using Borys.Hilfe;
using MySqlConnector;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Borys.Nachrichten
{
  internal class Eintragen
  {
    /// <summary>
    /// AusStr gelesenen RSS-Feeds Meldungen (titel, description)
    /// extrahieren
    /// </summary>
    /// <param name="args">" 
    ///     1. Parameter:    Eigabedatei
    ///     2. Parameter:    DB-Host
    /// </param>
    private const string FEHLER = "Fehler:\nmaximal 2 Argumente";
    private const string DBTABELLE = "meldungen";
    private static void Main(string[] args)
    {
      string ABFRAGE, DBHOST = "localhost";
      string EinDatei = "rss.rss";
      Assembly assembly = Assembly.GetExecutingAssembly();
      FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
      string companyName = fvi.CompanyName;
      string productName = fvi.ProductName;
      string productVersion = fvi.ProductVersion;
      string titel = fvi.FileDescription; //Assemblyinfo -> Titel
      Console.WriteLine($"{titel} V{productVersion}");
      Console.Title = titel;
      DBHOST = args.Length > 0 ? args[0] : "localhost";
      if (args.Length > 1)
      {
        Console.Error.WriteLine(FEHLER);
        Console.Beep(440, 300);
        Console.Beep(880, 200);
        _ = Console.ReadKey();
        throw new Exception(FEHLER);
      }
      ABFRAGE = $"SELECT id,parameter FROM {Hilfe.MySQLHilfe.DBTBB} WHERE programm='{titel}'";
      using (MySqlConnection con = Hilfe.MySQLHilfe.ConnectToDB(DBHOST))
      using (MySqlCommand SQL = new MySqlCommand(ABFRAGE, con))
      {
        try
        {
          uint id;
          con.Open();
          MySqlDataReader reader = SQL.ExecuteReader();
          if (!reader.Read())
          {
            reader.Close();
            Console.WriteLine($"Tabelle {Hilfe.MySQLHilfe.DBTBB} keine Einträge für {titel}\n...Ende");
            return; // endet hier
          }
          id = reader.GetUInt32("id");
          string RohEinDatei = reader.GetString(1);
          EinDatei = Hilfe.MySQLHilfe.PfadRein(RohEinDatei);
          reader.Close();
          try
          {
            using (XmlTextReader EinStr = new XmlTextReader(EinDatei))
              DBEin(EinStr, con, DBTABELLE);
            File.Delete(EinDatei);
          }
          catch (Exception ex)
          {
            if (ex.HResult == -2147024894)
            {
              SQL.CommandText = $"delete from {Hilfe.MySQLHilfe.DBTBB} where parameter='{RohEinDatei}'";
              SQL.ExecuteNonQuery();
            }
            throw ex;
          }
          SQL.CommandText =
            $"INSERT INTO {Hilfe.MySQLHilfe.DBTBB} (programm) VALUES ('P21Stop');";
          //$@"insert into {Hilfe.MySQLHilfe.DBTBB} (programm,parameter) values 
          //  ('P21Stop','W1'),('P3Wörter','W3'),('P3Wörter','W5'),
          //  ('P41neuWörter','W1'),('P41neuWörter','W3'),('P41neuWörter','W5');";
          SQL.ExecuteNonQuery();
          SQL.CommandText = $"delete from {Hilfe.MySQLHilfe.DBTBB} where id={id}";
#if !DEBUG
          SQL.ExecuteNonQuery();
#endif
          con.Close();
        }
        catch (XmlException ex)
        {
          XmlHilfe.XmlExceptionCatch(ex);
        }
        catch (Exception ex)
        {
          Console.Beep(880, 300);
          Console.Error.WriteLine(
$@"{ex.Message}
DB: {con.Database}
Source: {con.DataSource}
Site: {con.Site}
State: {con.State}");
        }
      }            //using
      Console.Beep(440, 200);
      Console.WriteLine("...fertig");
      _ = Console.ReadKey();
      return;
    }

    private static void DBEin(XmlTextReader EinStr, MySqlConnection con, string tabelle)
    {
      string rsstitel = null;
      int nMld = 0, nMldNeu = 0;
      if (EinStr is null)
      {
        throw new ArgumentNullException(nameof(EinStr));
      }
      using (MySqlCommand SQL = new MySqlCommand(string.Empty, con))
      {
        while (EinStr.Read())
        {
          if (XmlHilfe.ElementAnfang(EinStr, XmlHilfe.CHANNEL))
          {
            #region anfangchannel
            while (!XmlHilfe.ElementEnde(EinStr, XmlHilfe.CHANNEL))
            {//das Ende von channel wird allerdings nie erreicht
              if (rsstitel != null)
                break;// Titel gelesen, Rest egal
              _ = EinStr.Read();
              if (EinStr.NodeType == XmlNodeType.Element)
              {
                switch (EinStr.Name)
                {
                  case XmlHilfe.TITLE:
                    while (!XmlHilfe.ElementEnde(EinStr, XmlHilfe.TITLE))
                    {
                      _ = EinStr.Read();
                      if (EinStr.NodeType == XmlNodeType.Text)
                      {
                        rsstitel = EinStr.Value;
                      }
                    }//while !Ende Titel
                    break; //Ende Titel erreicht
                  default:
                    break;
                }
              }
            }//while !Xml
            #endregion
          }//if Anfang Channel
          if (XmlHilfe.ElementAnfang(EinStr, XmlHilfe.ITEM))
          {
            #region initem
            string titel = "", meldung = "", category = "", datum = "", link = "";
            while (!XmlHilfe.ElementEnde(EinStr, XmlHilfe.ITEM))
            {
              if (EinStr.NodeType == XmlNodeType.Element)
              {
                switch (EinStr.Name)
                {
                  case XmlHilfe.TITLE:
                    titel = TitelFinden(EinStr);
                    break;
                  case XmlHilfe.DESCRIPTION:
                    meldung = MeldungFinden(EinStr);
                    break;
                  case XmlHilfe.CATEGORY:
                    while (!XmlHilfe.ElementEnde(EinStr, XmlHilfe.CATEGORY))
                    {
                      _ = EinStr.Read();
                      if (EinStr.NodeType == XmlNodeType.Text)
                      {
                        category = EinStr.Value;
                      }
                    }
                    break;
                  case XmlHilfe.PUBDATE:
                    while (!XmlHilfe.ElementEnde(EinStr, XmlHilfe.PUBDATE))
                    {
                      _ = EinStr.Read();
                      if (EinStr.NodeType == XmlNodeType.Text)
                      {
                        datum = EinStr.Value;
                      }
                    }
                    break;
                  case XmlHilfe.LINK:
                    while (!XmlHilfe.ElementEnde(EinStr, XmlHilfe.LINK))
                    {
                      _ = EinStr.Read();
                      if (EinStr.NodeType == XmlNodeType.Text)
                      {
                        link = EinStr.Value;
                      }
                    }
                    break;
                  default:
                    break;
                }//switch
              }//if NodeType == Element
              _ = EinStr.Read();
            }//while !Ende Item 
            #endregion
            if (titel.Length > 0 && meldung.Length > 0)
            {
              nMld++;
              try
              {
                nMldNeu = MldInDb(tabelle, rsstitel, nMldNeu, SQL, titel, meldung, category, link, datum);
              }
              catch (MySqlException ex)
              {
                CatchSQLEx(EinStr, SQL, titel, ex);
              }
              catch (Exception ex)
              {
                Console.Error.WriteLine($"Zeile\t{EinStr.LineNumber}\nQuelle\t{ex.Source}\n{ex.Message}\nHelp\t{ex.HelpLink}");
                throw ex;
              }
            }
            else
            {
              Console.Error.WriteLine($"Zeile\t{EinStr.LineNumber} Inhalt fehlt:\n" +
                $">{titel}<\n>{meldung}<");
            }
          }
        }//while Read
      }//using
      Console.WriteLine($"{rsstitel}: {nMld} Meldungen, davon {nMldNeu} neu");
      return;
    }

    private static string MeldungFinden(XmlTextReader EinStr)
    {
      string meldung = null;
      while (!XmlHilfe.ElementEnde(EinStr, XmlHilfe.DESCRIPTION))
      {
        _ = EinStr.Read();
        if (EinStr.NodeType == XmlNodeType.Text)
        {
          meldung = EinStr.Value;
        }
      }
      return meldung;
    }

    private static string TitelFinden(XmlTextReader EinStr)
    {
      string titel = null;
      while (!XmlHilfe.ElementEnde(EinStr, XmlHilfe.TITLE))
      {
        _ = EinStr.Read();
        if (EinStr.NodeType == XmlNodeType.Text)
        {
          titel = EinStr.Value;
        }
      }
      return titel;
    }

    /// <summary>
    /// eine Meldung in DB
    /// </summary>
    /// <param name="pTabelle"></param>
    /// <param name="pRsstitel"></param>
    /// <param name="pNMldNeu"></param>
    /// <param name="pSQL"></param>
    /// <param name="ptitel"></param>
    /// <param name="pmeldung"></param>
    /// <param name="pcategory"></param>
    /// <returns>Anzahl neue Meldungen</returns>
    /// <param name="plink"></param><param name="pdatum"></param>
    private static int MldInDb(
      string pTabelle,
      string pRsstitel,
      int pNMldNeu,
      MySqlCommand pSQL,
      string ptitel,
      string pmeldung,
      string pcategory,
      string plink,
      string pdatum)
    {
      //int hash = ptitel.GetHashCode();
      //titel und meldung dürfe kein >'< enthalten
      ptitel = ptitel.Replace("'", "");
      pmeldung = pmeldung.Replace("'", "");
      try
      {
        DateTime dt;
        dt = DateTime.Parse(pdatum);
        pSQL.CommandText =
    $"INSERT INTO  {pTabelle} (titel,rohmeldung) VALUES ('{ptitel}', '{pmeldung}')";
        _ = pSQL.ExecuteNonQuery();
        pNMldNeu++;
        pSQL.CommandText =
        $"UPDATE  {pTabelle} SET category='{pcategory}' WHERE titel='{ptitel}'";
        _ = pSQL.ExecuteNonQuery();
        if (plink.Length > 255)
        {
          Console.Error.WriteLine(
          $"Link zu lang ({plink.Length} > 255) - kann nicht eingetragen werden\n>{plink}<\nin >{ptitel}<");
        }
        else
        {
          pSQL.CommandText =
        $"UPDATE  {pTabelle} SET link='{plink}' WHERE titel='{ptitel}'";
          _ = pSQL.ExecuteNonQuery();
        }
        pSQL.CommandText =
        $"UPDATE  {pTabelle} SET datum='{dt:yyyy-M-dd HH:mm:ss}' WHERE titel=' {ptitel}'";
        _ = pSQL.ExecuteNonQuery();
        pSQL.CommandText = $"UPDATE {pTabelle} SET quelle= '{pRsstitel}' WHERE titel='{ptitel}'";
        _ = pSQL.ExecuteNonQuery();

      }
      catch (MySqlException ex)
      {
        if (Hilfe.MySQLHilfe.IfMySQLKeyDoppeltEx(ex))
          Console.Error.WriteLine($"dopelter Eintrag\n{ptitel}");
        else
        {
          if (Hilfe.MySQLHilfe.IfMySQLSytaxError(ex))//1064
            Console.Error.WriteLine($"Syntax Error\n{pSQL.CommandText}\n{ex.Message}");
          if (Hilfe.MySQLHilfe.IfMySQLFeldFehlt(ex))//1054
            Console.Error.WriteLine($"DB-Spalte fehlt\n{ex.Message}");
          throw ex;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return pNMldNeu;
    }

    private static void CatchSQLEx(XmlTextReader EinStr, MySqlCommand SQL, string titel, MySqlException ex)
    {
      switch (ex.Number)
      {
        case 1062:
          Console.Error.WriteLine($"doppelt {EinStr.LineNumber}: {titel.Substring(0, 30)}");
          break;
        case 1064:
          Console.Error.WriteLine("ParseError");
          Console.Error.WriteLine(SQL.CommandText);
          throw ex;
        case 1146:
          Console.Error.WriteLine(ex.Message);
          SQL.CommandText = cTextauswertung.CREATEMELDUNGEN;
          _ = SQL.ExecuteNonQuery();
          Console.Error.WriteLine("Tabelle erzeugt");
          throw new Exception("Neustart notwendig");
        default:
          throw ex;
      }//switch                   
    }
    }
}
