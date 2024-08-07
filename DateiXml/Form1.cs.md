using System.Text;
using System.Xml;

namespace DateiXml
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void CmdSchreiben_Click(object sender, EventArgs e)
        {
            XmlTextWriter xw = new("C:/Temp/firma.xml", new UnicodeEncoding());
            xw.WriteStartDocument();
            xw.WriteStartElement("firma");

            xw.WriteStartElement("person");
            xw.WriteAttributeString("name", "Maier");
            xw.WriteAttributeString("vorname", "Hans");
            xw.WriteAttributeString("pnummer", "6714");
            xw.WriteAttributeString("gehalt", "3500,0");
            xw.WriteAttributeString("gtag", "15.03.1962");
            xw.WriteEndElement();

            xw.WriteStartElement("person");
            xw.WriteAttributeString("name", "Schmitz");
            xw.WriteAttributeString("vorname", "Peter");
            xw.WriteAttributeString("pnummer", "81343");
            xw.WriteAttributeString("gehalt", "3750,0");
            xw.WriteAttributeString("gtag", "12.04.1958");
            xw.WriteEndElement();

            xw.WriteStartElement("person");
            xw.WriteAttributeString("name", "Mertens");
            xw.WriteAttributeString("vorname", "Julia");
            xw.WriteAttributeString("pnummer", "2297");
            xw.WriteAttributeString("gehalt", "3621,5");
            xw.WriteAttributeString("gtag", "30.12.1959");
            xw.WriteEndElement();

            xw.WriteEndElement();
            xw.Close();
            LblAnzeige.Text = "Elemente in XML-Datei geschrieben";
        }

        private void CmdLesen_Click(object sender, EventArgs e)
        {
            if (!File.Exists("C:/Temp/firma.xml"))
            {
                LblAnzeige.Text = "Datei firma.xml existiert nicht";
                return;
            }

            XmlTextReader xr = new("C:/Temp/firma.xml");

            LblAnzeige.Text = "";
            while (xr.Read())
            {
                if (xr.NodeType == XmlNodeType.Element)
                {
                    if (xr.AttributeCount > 0)
                    {
                        while (xr.MoveToNextAttribute())
                            LblAnzeige.Text += $"{xr.Name} -> {xr.Value}\n";
                        LblAnzeige.Text += "\n";
                    }
                }
            }

            xr.Close();
        }

        private void CmdObjekteSchreiben_Click(object sender, EventArgs e)
        {
            Person[] personFeld = {
                new Person("Maier", "Hans", 6714, 3500.0,
                    new DateTime(1962, 3, 5)),
                new Person("Schmitz", "Peter", 81343, 3750.0,
                    new DateTime(1958, 4, 12)),
                new Person("Mertens", "Julia", 2297, 3621.5,
                    new DateTime(1959, 12, 30)) };

            XmlTextWriter xw = new("C:/Temp/firma.xml", new UnicodeEncoding());
            xw.WriteStartDocument();
            xw.WriteStartElement("firma");

            foreach (Person p in personFeld)
                p.AlsXmlElementSchreiben(xw);

            xw.WriteEndElement();
            xw.Close();
            LblAnzeige.Text = "Objekte in XML-Datei geschrieben";
        }

        private void CmdObjekteLesen_Click(object sender, EventArgs e)
        {
            if (!File.Exists("C:/Temp/firma.xml"))
            {
                LblAnzeige.Text = "Datei firma.xml existiert nicht";
                return;
            }

            string na = "";
            string vo = "";
            int pe = 0;
            double gh = 0.0;
            DateTime gb = new();

            LblAnzeige.Text = "";
            XmlTextReader xr = new("C:/Temp/firma.xml");
            while (xr.Read())
                if (xr.NodeType == XmlNodeType.Element)
                    if (xr.AttributeCount > 0)
                    {
                        while (xr.MoveToNextAttribute())
                            switch (xr.Name)
                            {
                                case "name":
                                    na = xr.Value;
                                    break;
                                case "vorname":
                                    vo = xr.Value;
                                    break;
                                case "pnummer":
                                    pe = Convert.ToInt32(xr.Value);
                                    break;
                                case "gehalt":
                                    gh = Convert.ToSingle(xr.Value);
                                    break;
                                case "gtag":
                                    int jahr = Convert.ToInt32(xr.Value[6..10]);
                                    int monat = Convert.ToInt32(xr.Value[3..5]);
                                    int tag = Convert.ToInt32(xr.Value[..2]);
                                    gb = new DateTime(jahr, monat, tag);
                                    break;
                            }
                        Person p = new(na, vo, pe, gh, gb);
                        LblAnzeige.Text += $"{p}\n";
                    }
            xr.Close();
        }
    }
}