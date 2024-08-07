using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace test
{
  internal class Program
  {
    static void Main(string[] args)
    {
      string result;
      while (true)
      {
        Dictionary<string, string> dict = new Dictionary<string, string>
    {
      { "pxp", "yyy" }, { "x", "yyyohne" }
    };
        string tpl = "123 $pxp$ 456 $x$";
        //string result = Regex.Replace(tpl, @"$(\w+)$", match => dict[match.Groups[1].Value]);
      result = Regex.Replace(tpl, @"\$(\w+)\$", match => dict[match.Groups[1].Value]);
        result = Regex.Replace(tpl, @"\$(\w+)\$", "...");
        result = Regex.Replace(tpl, @"\$(\w+)\$", fMatch );

      }
    }

    private static string fMatch(Match match)
    {
      Debug.WriteLine(match);
      Debug.WriteLine(match.Groups);
      Debug.WriteLine(match.Groups[1]);
      Debug.WriteLine(match.Groups[1].Value);
      return match.Groups[1].Value;
    }

    // static string match(Match input) { Debug.WriteLine(input); return input; }
    string match() { Debug.WriteLine("match"); return "m"; }
  }
}
