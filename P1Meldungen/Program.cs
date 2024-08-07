using System;
using System.Diagnostics;
using System.Reflection;
using System.Xml;

#warning Deprecated code ersetzt durch P2Eintragen
namespace PAbrufen
{
    internal class Abrufen
    {

    private const bool Append = true;
        private const string FEHLER = "Fehler:\nmaximal 2 Argumente";

        /// <summary>
        /// AusStr gelesenen RSS-Feeds Meldungen (titel, description)
        /// extrahieren
        /// </summary>
        /// <param name="args">" 
        ///     2 Parameter: 
        ///     Eigabedatei,
        ///     Ausgabedatei
        /// </param>
        private static void Main(string[] args)
        {
            int dateinr = 0;
            string EinDatei = "rss.rss";
            string AusDatei = "mld.mld";
            //Console.Title = "Meldungen extrahieren";

            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string companyName = fvi.CompanyName;
            string productName = fvi.ProductName;
            string productVersion = fvi.ProductVersion;


            if (args.Length > 0)
            {
                EinDatei = args[0];
            }

            if (args.Length > 1)
            {
                AusDatei = args[1];
            }

            if (args.Length > 2)
            {
                Console.Error.WriteLine(FEHLER);
                Console.Beep(440, 300);
                Console.Beep(880, 200);
                _ = Console.ReadKey();
                throw new Exception(FEHLER);
            }
            bool nochmal = true;
            using (XmlTextReader EinStr = new XmlTextReader(EinDatei))
            {
                while (nochmal)
                {
                    using (XmlTextWriter AusStr = new XmlTextWriter((++dateinr).ToString() + AusDatei, System.Text.Encoding.Unicode))
                    {
                        try
                        {
                            AusStr.WriteStartDocument();
                            AusStr.WriteComment($"{companyName}: {productName} V{productVersion}");
                            AusStr.WriteStartElement("nachrichten");
                            Abfrage(EinStr, AusStr);
                            nochmal = false;



#if DEBUG
                            AusStr.WriteEndElement();//nachrichten
                            AusStr.Close();
                            break;
#endif
                        }
                        catch (XmlException ex)
                        {
                            if (ex.HResult == -2146232000)
                            {
                                Console.Error.WriteLine
                                ($"behebbarer Fehler\nZeile {ex.LineNumber} von {ex.SourceUri}\n{ex.Message}");
                                nochmal = true;
                            }
                            else
                            {
                                throw ex;
                            }
                        }
                    }
                }
            }

            Console.Beep(440, 200);
            Console.WriteLine("...fertig");
            _ = Console.ReadKey();
            return;
        }

        private static void Abfrage(XmlTextReader EinStr, XmlTextWriter AusStr)
        {
            if (EinStr is null)
            {
                throw new ArgumentNullException(nameof(EinStr));
            }

            if (AusStr is null)
            {
                throw new ArgumentNullException(nameof(AusStr));
            }


            while (EinStr.Read())
            {
                if (XmlHelpers.ElementAnfang(EinStr, XmlHelpers.ITEM))
                {
                    AusStr.WriteStartElement("nachricht");
                    while (!XmlHelpers.ElementEnde(EinStr, XmlHelpers.ITEM))
                    {
                        if (EinStr.NodeType == XmlNodeType.Element)
                        {
                            switch (EinStr.Name)
                            {
                                case XmlHelpers.TITLE:
                                    while (!XmlHelpers.ElementEnde(EinStr, XmlHelpers.TITLE))


                                    {
                                        _ = EinStr.Read();
                                        if (EinStr.NodeType == XmlNodeType.Text)
                                        {
                                            AusStr.WriteElementString(XmlHelpers.TITLE, EinStr.Value);
                                        }
                                    }
                                    break;
                                case XmlHelpers.DESCRIPTION:
                                    while (!XmlHelpers.ElementEnde(EinStr, XmlHelpers.DESCRIPTION))
                                    {
                                        _ = EinStr.Read();
                                        if (EinStr.NodeType == XmlNodeType.Text)
                                        {
                                            AusStr.WriteElementString(XmlHelpers.DESCRIPTION, EinStr.Value);
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }

                        _ = EinStr.Read();
                    }
                    AusStr.WriteEndElement();//nachricht
                    AusStr.WriteWhitespace(" \n");
                }
            }
        }
    }
}