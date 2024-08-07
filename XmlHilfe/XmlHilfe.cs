using System;
using System.Xml;
namespace Borys.Hilfe
{
  public static class XmlHilfe
  {
    public const string DESCRIPTION = "description", TITLE = "title", ITEM = "item", CHANNEL = "channel";
    public const string CATEGORY = "category";
    public const string PUBDATE = "pubDate", LINK = "link";

    public static bool ElementAnfang(XmlTextReader EinStr, string v) => EinStr.NodeType == XmlNodeType.Element && EinStr.Name == v;
    public static bool ElementEnde(XmlTextReader EinStr, string v) => EinStr.NodeType == XmlNodeType.EndElement && EinStr.Name == v;
    public static void XmlExceptionCatch(XmlException ex)
    {
      if (ex.HResult == -2146232000)
      {
        Console.Error.WriteLine
        ($"Fehler\nZeile {ex.LineNumber} von {ex.SourceUri}\n{ex.Message}");
      }
      else
      {
        throw ex;
      }
    }
  }
}
