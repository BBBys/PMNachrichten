using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Borys.Nachrichten
{
  internal class Entropie
  {
    private const string PARAMFEHLER = "Fehler:\nmaximal 1 Argument";
    private const string DBTMELDUNGEN = "meldungen";
    private const string DBT1WOERTER = "woerter1";
    private const string DBT2WOERTER = "woerter2";
    private const string DBT3WOERTER = "woerter3";
    private const string DBT4WOERTER = "woerter4";
    private const string DBT5WOERTER = "woerter5";
    private const string DB = "news", DBPORT = "3306";
    private const string DBUSER = "news", DBPWD = "WImGfKxkx2CQ0B9";
    //-----------------
    /// <summary>
    /// 
    /// </summary>
    private const int LTEXT = 1;
    private const string DBTWOERTER = DBT1WOERTER;
    //-----------------
    private const int LIMIT =
#if DEBUG
      10
#else
      900
#endif
      ;
    private static void Main(string[] args)
    {
      FileVersionInfo fvi;
      Assembly ass;
      string DBHOST, CONSTRING,pn,pv;
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
      using (MySqlConnection conWrt = new MySqlConnection(CONSTRING))
      using (MySqlCommand SQL =
        //        new MySqlCommand($"UPDATE {DBTMELDUNGEN} SET entr=-1 ", conMld))
        new MySqlCommand($"SELECT COUNT(*) FROM {DBTMELDUNGEN} WHERE entr<.0001 ", conMld))
      {
        try
        {
          MySqlDataReader reader;
          conMld.Open();
          conWrt.Open();
          reader = SQL.ExecuteReader();
          reader.Read();
          Console.WriteLine($"noch {reader.GetInt32(0)} Meldungen zu bearbeiten");
          reader.Close();
          SQL.CommandText = $"SELECT hash , titel, meldung FROM {DBTMELDUNGEN} WHERE entr<.0001 ";
          reader = SQL.ExecuteReader();

          if (reader.HasRows)
          {
            int nFehl = 0;
            Stopwatch stw = new Stopwatch();
            int nMld = 0;
            stw.Start();
            while (reader.Read())
            {
              double gesamtEntropie;
              List<string> WordSequences;
              int id, wordCount, letterCount;
              string titel, meldung, str;
              nMld++;
              titel = reader["titel"].ToString();
              meldung = reader["meldung"].ToString();
              str = cTextauswertung.strukturieren($"{titel} {meldung}");
              id = Convert.ToInt32(reader["hash"]);
              WordSequences = MldHilfe.TeileText(str, LTEXT);
              //GetThreeWordSequences(str);
              gesamtEntropie = 0;

              foreach (string sequence in WordSequences)
              {
                double entropie;
                bool gültig;
                (entropie, gültig) = WortfolgeEntropie(sequence, conWrt, DBTWOERTER);
                if (!gültig)
                {
                  Console.Error.WriteLine($"Entropie fehlt für {sequence}");nFehl++;
                  if (nFehl > 10)
                    throw new Exception("zu viele Fehler - Abbruch");
                }
                else
                {
                  gesamtEntropie += entropie;
                }
              }
              wordCount = WordSequences.Count;
              //wordCount = str.Split(new char[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
              letterCount = str.Replace(" ", "").Length;

              UpdateTableMld(DBTMELDUNGEN, id, gesamtEntropie, wordCount, letterCount, conWrt);
              if (nMld % 15 == 0)
              {
                Console.Write(".");
                if (nMld % 500 == 0)
                {
                  if (MldHilfe.Laufzeit(stw, nMld, 0, 0, LIMIT))
                  {
                    break;
                  }
                }
              }
            }//while
          }
          else
          { Console.Error.WriteLine("keine Meldungen!"); }
          reader.Close();
          conMld.Close();
          conWrt.Close();
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

    //static List<string> GetThreeWordSequences(string input)
    //{
    //  List<string> sequences = new List<string>();
    //  string[] words = input.Split(new char[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries);

    //  for (int i = 0; i < words.Length - 2; i++)
    //  {
    //    sequences.Add(words[i] + " " + words[i + 1] + " " + words[i + 2]);
    //  }

    //  return sequences;
    //}
    private static (double, bool) WortfolgeEntropie(string sequence, MySqlConnection con, string DBTabelle)
    {
      using (MySqlCommand SQL = new MySqlCommand($"SELECT entr FROM {DBTabelle} WHERE wort = '{sequence}'", con))
      using (MySqlDataReader reader = SQL.ExecuteReader())
      {
        if (reader.HasRows)
        {
          if (reader.Read())
          {
            object value;
            value = reader.GetValue(0);
            return value.GetType() == typeof(DBNull)
              ? throw new Exception($"entr nicht vorhanden für {sequence} in {DBTabelle}")
              : (Convert.ToDouble(value), true);
          }
        }
        return (0, false);
      }
    }

    private static void UpdateTableMld(string DBTabelle, int hash, double MldEntr, int MldAntW, int MldAntBuchst, MySqlConnection con)
    {
      string str = Convert.ToString(MldEntr, MldHilfe.DEZIMALPUNKT);
      using (MySqlCommand command = new MySqlCommand(
        $"UPDATE {DBTabelle} SET entr = {str}, woerter = {MldAntW}, buchstaben = {MldAntBuchst} WHERE hash = {hash}",
        con))
      {
        _ = command.ExecuteNonQuery();
      }
    }
  }
}