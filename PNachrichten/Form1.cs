using PNachrichten.Properties;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;
using static cTextauswertung;

namespace PNachrichten
{
  public partial class fNachrichten : Form
  {
    private fKlassifikation FKlassifikation = new fKlassifikation();
    //private const string 
    private string RssUrl;
    private List<WortType> lWoerter = new List<WortType>();
    private List<string> lPositiv = new List<string>();
    private List<string> lNegativ = new List<string>();
    private List<string> lStopp = new List<string>();
    private readonly Settings Props;
    private enum eWoInRSS
    {
      Aussen, Item, Text, Description
    };
    private eWoInRSS WoInRSS;
    private int KlassiPos = 0, TotalToken = 0;

    public fNachrichten()
    {
      InitializeComponent();
      Text = Properties.Resources.Fenstertitel + " V" + Application.ProductVersion + ". ";
#if DEBUG
      Text += "DEBUGVERSION";
#else
            Text += Application.CompanyName + ": " + Application.ProductName;
#endif
      Props = Properties.Settings.Default;
    }

    /// <summary>
    /// global
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void bAbfrage_Click(object sender, EventArgs e)
    {
      List<string> lNachrichten;
    lNachrichten =   NachrichtenLesen(RssUrl);
      WoInRSS = eWoInRSS.Aussen;
      foreach (string s0 in lNachrichten)
      {
        if (s0 != null)
        {
          string s, s1;
          int p, p1;
          Console.WriteLine("{0}", s0);
          s = s0.ToLower();
          s = Entfernen("&quot;", s);
          s = Entfernen("&#39;", s);
          s = Entfernen("&amp;", s);

          switch (WoInRSS)
          {
            case eWoInRSS.Aussen:
              if (s.Contains("<item>"))
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
                  s1 = s.Substring(p + 7, p1 - p - 7);
                  SatzAuswerten(s1);
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
                  s1 = s.Substring(p + 13, p1 - p - 13);
                  SatzAuswerten(s1);
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
    }


    /// <summary>
    /// globales Modul
    /// </summary>
    /// <param name="s1"></param>
    private void SatzAuswerten(string s1)
    {
      int pos = 0, sum = 0, neg = 0;
      float ratio = 0;
      //s1 = "ein 70-jähriger mann hat in einem café einen herzstillstand erlitten. während die rettungskräfte um sein leben kämpften, filmten umstehende das geschehen – bis einige frauen einschritten";
      string[] woerter = s1.Split(WortTrennzeichen);
      foreach (string wort in woerter)
      {
        if (lPositiv.Contains(wort))
        {
          pos++;
        }
        else if (lNegativ.Contains(wort))
        {
          neg++;
        }
        else if (wort.Length > 2)
        {
          lWoerter.Add(new WortType(wort));
        }
        else if (wort.Length > 0)
        {
          lWoerter.Add(new WortType(wort, 1, WortType.eKlassen.stopp.ToString()));
        }
      }
      //tbErgebnis.Lines[tbErgebnis.Lines.Length] = s1;
      sum = pos + neg;
      if (sum > 0)
      {
        ratio = (pos - neg) / (float) sum;
        tbErgebnis.Lines = new string[] { tbErgebnis.Text, s1, string.Format("pos: {0}, neg: {1}, rat: {2}", pos, neg, ratio) };
      }
      return;
    }

    /// <summary>
    /// globalesModul
    /// </summary>
    /// <param name="was"></param>
    /// <param name="s"></param>
    /// <returns></returns>
    private static string Entfernen(string was, string s)
    {
      int p = s.IndexOf(was);
      int l = was.Length;
      while (p > -1)
      {
        s = s.Remove(p, l);
        p = s.IndexOf(was);
      }

      return s;
    }

    private void bSpeichern_Click(object sender, EventArgs e)
    {
      lWoerter.Sort();
      using (StreamWriter sw = new StreamWriter("Wörter.txt"))
      {
        foreach (WortType w in lWoerter)
        {
          sw.WriteLine(w.ToString());
        }
      }
    }

    private void bLesen_Click(object sender, EventArgs e)
    {
      using (StreamReader sr = new StreamReader("Wörter.txt"))
      {
        string line;
        lPositiv.Clear();
        lNegativ.Clear();
        lStopp.Clear();
        while ((line = sr.ReadLine()) != null)
        {
          string[] teile;
          teile = line.Split(';');
          WortType w = new WortType(teile[0], Convert.ToInt32(teile[1]), teile[2]);
          lWoerter.Add(w);
          if (w.Positiv())
          {
            lPositiv.Add(teile[0]);
          }
          if (w.Negativ())
          {
            lNegativ.Add(teile[0]);
          }
          if (w.Stopp())
          {
            lStopp.Add(teile[0]);
          }
        }
      }
    }

    private void bAuswerten_Click(object sender, EventArgs e)
    {
      int length;
      length = lWoerter.Count;
      if (length > 1)
      {
        int i, types, token;
        lWoerter.Sort();
        WortType w1, w2;
        w1 = lWoerter[0];
        i = 1;
        while (i < lWoerter.Count)
        {
          w2 = lWoerter[i];
          if (w1 == w2)
          {
            if (w1.Unklassifiziert())
            {
              w1.Klasse = w2.Klasse;
            }
            w1.Anzahl += w2.Anzahl;
            lWoerter.RemoveAt(i);
          }
          else
          {
            w1 = w2;
            i++;
          }
        }
        types = lWoerter.Count;
        token = 0;
        foreach (WortType w in lWoerter)
        {
          token += w.Anzahl;
        }
        TotalToken = token;
        lTypes.Text = string.Format("Types:      {0}", types);
        lToken.Text = string.Format("Token:      {0}", token);
        lRatio.Text = string.Format("Verhältnis: {0}", token / (float) types);
      }
    }

    private void rbSpiegel_CheckedChanged(object sender, EventArgs e)
    {
      if (rbSpiegel.Checked)
      {
RssUrl = "https://www.spiegel.de/schlagzeilen/index.rss";
      }
      if (rbTagesschau.Checked)
      {
        RssUrl = "https://www.tagesschau.de/newsticker.rdf";
      }
      bAbfrage.Enabled = true;
    }

    private void bKlass_Click(object sender, EventArgs e)
    {
      FKlassifikation.lWoerter = lWoerter;
      FKlassifikation.TotalToken = TotalToken;
      FKlassifikation.ShowDialog();
    }
  }
}