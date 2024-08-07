using System.Collections.Generic;
using System.IO;
namespace Borys.Nachrichten
{
  public class Stoppliste
  {
    private static List<string> _SWL = null;
    //public Stoppliste()
    //{
    //  //   string line=";";_SWL = new List<string>();
    //  //   //foreach (char s in Properties.Resources.Stoppwörter) {Console.WriteLine(s); }
    //  //   StringReader sr = new StringReader(Properties.Resources.Stoppwörter);
    //  //while (line!=null)    
    //  //   {  if (!line.StartsWith(";")) _SWL.Add(line);line = sr.ReadLine(); };

    //}

    public static List<string> SWL
    {
      get
      {
        if (_SWL == null)
        {
          string line = ";";
          _SWL = new List<string>();
          StringReader sr = new StringReader(Properties.Resources.Stoppwörter);
          while (line != null)
          {
            if (!line.StartsWith(";"))
              _SWL.Add(line);
            line = sr.ReadLine();
          };
        }
        return _SWL;
      }
    }
  }
}
