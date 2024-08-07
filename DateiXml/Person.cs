using System.Xml;

namespace DateiXml
{
    class Person
    {
        private readonly string name;
        private readonly string vorname;
        private readonly int pnummer;
        private readonly double gehalt;
        private readonly DateTime gtag;

        public Person(string na, string vo, int pe, double gh, DateTime gt)
        {
            name = na;
            vorname = vo;
            pnummer = pe;
            gehalt = gh;
            gtag = gt;
        }

        public void AlsXmlElementSchreiben(XmlTextWriter xw)
        {
            xw.WriteStartElement("person");
            xw.WriteAttributeString("name", name);
            xw.WriteAttributeString("vorname", vorname);
            xw.WriteAttributeString("pnummer", pnummer.ToString());
            xw.WriteAttributeString("gehalt", gehalt.ToString());
            xw.WriteAttributeString("gtag", gtag.ToShortDateString());
            xw.WriteEndElement();
        }

        public override string ToString() => $"{name}, {vorname}, " +
            $"{pnummer}, {gehalt}, {gtag.ToShortDateString()}";
    }
}
