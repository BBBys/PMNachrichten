using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
namespace Borys.Nachrichten
{
  internal class Stop
  {
    private static void Main(string[] args)
    {
      //Stoppliste sl = new Stoppliste();

      const string FEHLER = "Fehler:\nmaximal 1 Argumente";
      string ABFRAGE, DBHOST = "localhost";
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
      using (MySqlConnection conEin = Hilfe.MySQLHilfe.ConnectToDB(DBHOST),
        conAus = Hilfe.MySQLHilfe.ConnectToDB(DBHOST))
      using (MySqlCommand SQLEin = new MySqlCommand(ABFRAGE, conEin),
        SQLAus = new MySqlCommand(ABFRAGE, conAus))
      {
        uint AufgabenId;
        try
        {
          conEin.Open();
          MySqlDataReader reader = SQLEin.ExecuteReader();
          if (!reader.Read())
          {
            reader.Close();
            Console.WriteLine($"Tabelle {Hilfe.MySQLHilfe.DBTBB} keine Einträge für {titel} ");
            return;// endet hier
          }
          AufgabenId = reader.GetUInt32("id");
          reader.Close();
          conAus.Open();
          EntferneStop(
            Hilfe.MySQLHilfe.DBTMELDUNGEN,
            Hilfe.MySQLHilfe.DBTDATEN,
            SQLEin,
            SQLAus);
        }
        catch (MySqlException ex)
        {
          if (Hilfe.MySQLHilfe.IfMySQLTabelleFehltExBeheben(ex, conEin))
          {
            if (Hilfe.MySQLHilfe.WelcheTabelleFehlt(ex).Equals(Hilfe.MySQLHilfe.DBTDATEN))
            {
              SQLEin.CommandText =
          $@"update {Hilfe.MySQLHilfe.DBTMELDUNGEN} set datenID=0;";
              SQLEin.ExecuteNonQuery();
            }
            return;
          }
          throw ex;
        }
        catch (Exception ex)
        {
          throw ex;
        }
        SQLEin.CommandText =
          $@"INSERT INTO {Hilfe.MySQLHilfe.DBTBB} (programm,parameter) VALUES 
          ('P3Wörter','W1'),('P3Wörter','W3'),('P3Wörter','W5'),
          ('P41neuWörter','W1'),('P41neuWörter','W3'),('P41neuWörter','W5');";
        SQLEin.ExecuteNonQuery();
        SQLEin.CommandText = $"DELETE FROM {Hilfe.MySQLHilfe.DBTBB} WHERE id={AufgabenId}";
#if !DEBUG
        SQLEin.ExecuteNonQuery();
#endif
        conEin.Close();
      }
    }


    private static void EntferneStop(
      string dbtMld,
      string dbtDaten,
      MySqlCommand SQLEin, MySqlCommand SQLAus)
    {
      List<string> stopw = Stoppliste.SWL;
      _ = new List<string>();
      SQLEin.CommandText =
        $"SELECT id as id,`rohmeldung` as roh FROM {dbtMld} WHERE datenId IS NULL OR datenId<1";
      using (MySqlDataReader readerEin = SQLEin.ExecuteReader())
      {
        int datenId;
        MySqlDataReader readerAus = null;
        if (!readerEin.HasRows)
        { readerEin.Close(); Console.WriteLine("keine Roh-Meldungen zu bearbeiten"); return; }
        {
          while (readerEin.Read())
          {
            string ohne = string.Empty;
            int meldungId = readerEin.GetInt32("id");
            string roh = readerEin.GetString("roh");
            Debug.WriteLine(roh);
            roh = cTextauswertung.Strukturieren(roh.Trim().ToLower().Normalize());
            // Split the sentence into words
            string[] words = Regex.Split(roh, @"\W+");
            int nWörter = words.Length;
            int nBuchstaben = roh.Length;
            foreach (string wort in words)
            {
              if (!stopw.Contains(wort))
                ohne = $"{ohne} {wort}";
            }
            ohne = ohne.Trim();
            Debug.WriteLine(ohne);
            //SQLAus.CommandText = $"UPDATE `meldungen` SET stopfrei='y' WHERE id = 1; ";
            SQLAus.CommandText =
            $@"INSERT INTO `{dbtDaten}` 
              (`mldid`,stopfrei,buchstaben,woerter) 
              VALUES ('{meldungId}','{ohne}','{nBuchstaben}','{nWörter}');";
            int erfolg = SQLAus.ExecuteNonQuery();
            Debug.Assert(erfolg == 1, "Fehlschlag Eintrag stopfrei in {dbtDaten} ");
            SQLAus.CommandText = $"select id as DatenId from {dbtDaten} where mldid={meldungId}";
            readerAus = SQLAus.ExecuteReader();
            _ = readerAus.Read();
            datenId = readerAus.GetInt32("DatenId");
            readerAus.Close();
            SQLAus.CommandText = $"UPDATE {dbtMld} SET datenID={datenId} WHERE id={meldungId};";
            erfolg = SQLAus.ExecuteNonQuery();
            Debug.Assert(erfolg == 1, $"Fehlschlag Eintram DatenId in {dbtMld}");
          }//while
        }
        readerEin.Close();
      }
    }
  }
}