using System;
using System.Collections.Generic;
using System.IO;
using static cTextauswertung;

namespace PAbrufen
{
    internal class Abrufen
    {
        /// <summary>
        /// 
        /// </summary>
        private const string RssUrlSp = "https://www.spiegel.de/schlagzeilen/index.rss";
        private const string RssUrlTs = "https://www.tagesschau.de/newsticker.rdf";
        private const bool Append = true;

        /// <summary>
        /// RSS-Feeds von Spiegel und Tagesschau öffnen; 
        /// lesen und Ergebnisse in
        /// Param1 und Param2 speichern. 
        /// Ausgabe-Dateien ohne Parameter-Angabe: 
        /// TS.RSS und
        /// SP.RSS
        /// </summary>
        /// <param name="args">" 2 Parameter: Ausgabedateien</param>
        private static void Main(string[] args)
        {
            string Param1 = "TS.RSS";
            string Param2 = "SP.RSS";
            //Console.Title = "Abfrageergebnis";
            if (args.Length > 0)
            {
                if (args.Length == 2)
                {
                    Param1 = args[0];
                    Param2 = args[1];
                }
                else
                {
                    Console.Error.WriteLine("Fehler:\nentweder 2 oder kein Argument");
                    Console.Beep(440, 200);
                    Console.Beep(880, 200);
                    _ = Console.ReadKey();
                    throw new Exception("Fehler:\nentweder 2 oder kein Argument");
                }
            }
            using (StreamWriter Ausgabe1 = new StreamWriter(
          Param1, Append),
        Ausgabe2 = new StreamWriter(Param2, Append))
            {
                Abfrage(RssUrlTs, Ausgabe1);
                Abfrage(RssUrlSp, Ausgabe2);
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
            List<string> lNachrichten;
            eWoInRSS WoInRSS = eWoInRSS.Aussen;
            lNachrichten = cTextauswertung.NachrichtenLesen(RssUrl);
            foreach (string s0 in lNachrichten)
            {
                if (s0 != null)
                {
                    string s, s1;
                    int p, p1;
#if DEBUG
                    Console.WriteLine("{0}", s0);
#endif
                    /* erst später:
                    s = s0.ToLower();
                    s = Entfernen("&quot;", s);
                    s = Entfernen("&#39;", s);
                    s = Entfernen("&amp;", s);
                    */
                    s = s0;

                    switch (WoInRSS)
                    {
                        case eWoInRSS.Aussen:
                            if (s.Contains("<item"))
                            {
                                WoInRSS = eWoInRSS.Item;
                            }
                            break;
                        case eWoInRSS.Item:
                            if (s.Contains("</item>"))
                            {
                                WoInRSS = eWoInRSS.Aussen;
                                break;
                            }
                            p = s.IndexOf("<title>");
                            if (p >= 0)
                            {
                                p1 = s.IndexOf("</title>");
                                if (p1 > p + 7)
                                {
                                    s1 = s.Substring(p, p1 - p + 8);
                                    TextSpeichern(s1, aus);
                                }
                                else
                                {
                                    throw new Exception("<title> nicht in einer Zeile");
                                }
                                break;
                            }
                            p = s.IndexOf("<description>");
                            if (p >= 0)
                            {
                                p1 = s.IndexOf("</description>");
                                if (p1 > p + 13)
                                {
                                    s1 = s.Substring(p, p1 - p + 14);
                                    TextSpeichern(s1, aus);
                                }
                                else
                                {
                                    throw new Exception("<description> nicht in einer Zeile");
                                }
                                break;
                            }
                            break;
                        case eWoInRSS.Text:
                            break;
                        case eWoInRSS.Description:
                            break;
                        default:
                            break;
                    }
                }
            }
            lNachrichten.Clear();
        }
    }
}

