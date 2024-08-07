using PSätze.Properties;

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using static cTextauswertung;

namespace PSätze
{


  public partial class fSätze : Form
  {
    private const int MAXL = 6;
    private readonly Settings Props;
    private List<string> lMeldungen = null;
    private List<WortType>[] lWörter = new List<WortType>[MAXL];
    /*   null;
     private List<WortType> l2Woerter = null;
     private List<WortType> l3Woerter = null;
     private List<WortType> l4Woerter = null;
     private List<WortType> lWörter[4] = null;
     private List<WortType> lWörter[5] = null;*/
    private int TotalToken;

    public fSätze()
    {
      InitializeComponent();
      Props = Settings.Default;
      Text = $"{Resources.Fenstertitel} V{Application.ProductVersion}. ";
#if DEBUG
      Text += "DEBUGVERSION";
#else
      Text += Application.CompanyName + ": " + Application.ProductName;
#endif
      tbAusgabe.Text = Props.Ausgabedatei;
      tbEingabe.Text = Props.Eingabedatei;

    }

    private void tbEingabe_Click(object sender, EventArgs e)
    {
      ofdEingabe.FileName = tbEingabe.Text;
      if (ofdEingabe.ShowDialog() == DialogResult.OK)
      {
        Props.Eingabedatei = ofdEingabe.FileName;
        Props.Save();
        tbEingabe.Text = Props.Eingabedatei;
      }
    }

    private void tbAusgabe_Click(object sender, EventArgs e)
    {
      ofdAusgabe.FileName = tbAusgabe.Text;
      if (ofdAusgabe.ShowDialog() == DialogResult.OK)
      {
        Props.Ausgabedatei = ofdAusgabe.FileName;
        Props.Save();
        tbAusgabe.Text = Props.Ausgabedatei;
      }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Literale nicht als lokalisierte Parameter übergeben", MessageId = "System.Windows.Forms.Control.set_Text(System.String)")]
    private void bStart_Click(object sender, EventArgs e)
    {
      Button b = (Button)sender;
      b.Enabled = false;
      if (lMeldungen == null)
      {
        lMeldungen = DateiLesen(Props.Eingabedatei);
      }
      else
      {
        lMeldungen.AddRange(DateiLesen(Props.Eingabedatei));
      }
      lAnzahl.Text = $"Anzahl: {lMeldungen.Count}";
      b.Enabled = true;
      b.Text = "mehr?";
      bWörter.Enabled = true;
      b2Wörter.Enabled = true;
      b3Wörter.Enabled = true;
      b4Wörter.Enabled = true;
      b6Wörter.Enabled = true;
      b5Wörter.Enabled = true;
      bAusgeben.Enabled = true;

    }

    private void bWörter_Click(object sender, EventArgs e)
    {
      Button b = (Button)sender;
      positionieren(b);
      lWörter[0] = WörterExtrahieren(lWörter[0], 1);
      if (lWörter[0].Count > 1)
      {
        lWörter[0] = WörterZusammenfassen(lWörter[0], 1);
      }
    }

    private void positionieren(Button b)
    {
      b.Enabled = false;
      // gbStatistik.Top=b.Top;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="wortTypes">die Liste</param>
    /// <param name="länge">n-Gramm-Länge</param>
    /// <returns></returns>
    private List<WortType> WörterZusammenfassen(List<WortType> wortTypes, int länge)
    {
      int i, types, token;
      WortType w1, w2;
      if (länge < 1 || länge > MAXL)
        throw new ArgumentOutOfRangeException(nameof(länge));
      if (wortTypes == null)
        throw new ArgumentNullException(nameof(wortTypes));
      #region zusammenfassen
      wortTypes.Sort();
      w1 = wortTypes[0];
      i = 1;
      pBar.Visible = true;
      while (i < wortTypes.Count)
      {
        pBar.Maximum = wortTypes.Count;
        pBar.Value = i;
        w2 = wortTypes[i];
        if (w1 == w2)
        {
          if (w1.Unklassifiziert)
          {
            w1.Klasse = w2.Klasse;
          }
          w1.Anzahl += w2.Anzahl;
          wortTypes.RemoveAt(i);
        }
        else
        {
          // w1 ist fertig
          w1 = w2;
          i++;
        }
        pBar.Update();
      }
      types = wortTypes.Count;
      #endregion
      token = 0;
      foreach (WortType w in wortTypes)
      {
        token += w.Anzahl;
      }
      TotalToken = token;
      double entrop = 0.0;
      foreach (WortType wort in wortTypes)
      {
        wort.nTokenInText = TotalToken;
        entrop += wort.Entropie;
      }
      lTypes.Text = $"Types:         {types}";
      lToken.Text = $"Token:         {TotalToken}";
      lRatio.Text = $"Verhältnis:    {(double)TotalToken / (double)types:F1}";
      lTokEnt.Text = $"Tokenentropie: {entrop:F1}";
      lWortEnt.Text = $"Wortentropie:  {entrop / (double)länge:F1}";
      WörterAnzeigen(wortTypes, TotalToken, länge);
      pBar.Visible = false;
      int chartSerien;
      #region zeichnen
      chartSerien = chart1.Series.Count;
      int chartAreas = chart1.ChartAreas.Count;
      if (chartAreas < 1)
      {
        chart1.ChartAreas.Add("Zipfs");
      }

      chart1.ChartAreas[0].AxisX.IsLogarithmic = true;
      chart1.ChartAreas[0].AxisY.IsLogarithmic = true;
      chart1.ChartAreas[0].AxisY.Title = "Anzahl";
      chart1.ChartAreas[0].AxisX.Title = "Position";
      try
      {
        chart1.Series.Add($"Zipf{länge}");
        chart1.Series[chartSerien].IsVisibleInLegend = true;
        chart1.Series[chartSerien].LegendText = $"{länge} Wörter ";
        chart1.Series[chartSerien].XValueType = ChartValueType.Double;
        chart1.Series[chartSerien].YValueType = ChartValueType.Double;
        chart1.Series[chartSerien].ChartType = SeriesChartType.Line;
        //chart1.Series[chartSerien].AxisLabel = "log Position";

        i = 1;
        foreach (WortType wort in wortTypes)
        {
          int nw = wort.Anzahl;
          if (nw > 0)
          {
            chart1.Series[chartSerien].Points.AddXY(i, nw);
            i++;
          }
        }

      }
      catch (Exception eee)
      {
        MessageBox.Show(eee.Message);
        throw eee;
      }
      #endregion

      return wortTypes;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="wortTypes">Liste</param>
    /// <param name="nToken">Gesamt-Anzahl im Text</param>
    /// <param name="länge">n-Gramm-Länge</param>
    private void WörterAnzeigen(List<WortType> wortTypes, int nToken, int länge)
    {
      /*
       * 4er: 16492 Token, anzeigen: 4
       * 3er: 17408 Token, anzeigen: 5
       * 2er: 18324 Token anzeigen: 10
       * 1er: 17408 Token, anzeigen 15
       */
      /*
       * Stoppgrenze
       * n  rH    Entro
       * 1  0,003 0,024
       * 2  0,002 0,019
       * 3
       * 4
       * 5
       * 6
       */
      double grenze = Math.Log10(nToken) / 0.26 / länge;
#if DEBUG
      grenze /= 2;
#endif
      if (grenze < 1.5)
        grenze = 1.5;
      if (länge < 1 || länge > MAXL)
        throw new ArgumentOutOfRangeException(nameof(länge));
      if (wortTypes == null)
        throw new ArgumentNullException(nameof(wortTypes));
      tbErgebnis.Clear();
      //Comparer<WortType> nh=new Comparer<WortType>();
      //nachHäufigkeit nh = new nachHäufigkeit();
      //wortTypes.Sort(nh);
      wortTypes.Sort(new nachHäufigkeit());
      StringBuilder sb = new StringBuilder();
      foreach (WortType wort in wortTypes)
      {
        if (wort.Anzahl > grenze)
        {
          // tbErgebnis.Text += wort.ToString() + Environment.NewLine;
          sb.Append(wort.ToString() + Environment.NewLine);
        }
      }
      tbErgebnis.Text = sb.ToString();
      sb.Clear();
    }

    private List<WortType> WörterExtrahieren(List<WortType> wortTypes, int länge)
    {
      if (länge < 1 || länge > MAXL)
        throw new ArgumentOutOfRangeException(nameof(länge));
      if (wortTypes == null)
      {
        wortTypes = new List<WortType>();
      }
      foreach (string meldung0 in lMeldungen)
      {
        int p1, p;
        string meldung;
        p = meldung0.IndexOf("<meldung>");
        if (p >= 0)
        {
          p1 = meldung0.IndexOf("</meldung>");
          if (p1 > p + 9)
          {
            string w1, w2, w3, w4, w5;
            meldung = meldung0.Substring(p + 9, p1 - p - 9);
            meldung = strukturieren(meldung);
            string[] woerter = meldung.Split(separator: WortTrennzeichen);
            const string leererString = "";
            switch (länge)
            {
              case 1:
                foreach (string wort in woerter)
                {
                  if (Gültig(wort))
                  {
                    if (wort.Length > 0)
                    {
                      wortTypes.Add(new WortType(wort, 1));
                    }
                  }
                }
                break;
              case 2:
                w1 = leererString;
                foreach (string wneu in woerter)
                {
                  if (!Gültig(w1))
                  {
                    w1 = wneu;
                  }
                  else
                  {
                    if (Gültig(wneu))
                    {
                      wortTypes.Add(new WortType(w1 + " " + wneu, 2));
                      w1 = wneu;
                    }
                    else
                    {
                      w1 = leererString;
                    }
                  }
                }
                //wortTypes.Add(new WortType(w1 + " <stop>", 2));
                break;
              case 3:
                w1 = leererString;
                w2 = leererString;
                foreach (string wneu in woerter)
                {
                  if (!Gültig(w1, w2))
                  {
                    w1 = w2;
                    w2 = wneu;
                  }
                  else
                  {
                    if (Gültig(wneu))
                    {
                      wortTypes.Add(new WortType(w1 + " " + w2 + " " + wneu, 3));
                      w1 = w2;
                      w2 = wneu;
                    }
                    else
                    {
                      w1 = w2 = leererString;
                    }
                  }
                }
                //wortTypes.Add(new WortType(w1 + " " + w2 + " <stop>", 3));
                break;
              case 4:
                w1 = leererString;
                w2 = leererString;
                w3 = leererString;
                foreach (string wneu in woerter)
                {
                  if (!Gültig(w1, w2, w3))
                  {
                    w1 = w2;
                    w2 = w3;
                    w3 = wneu;
                  }
                  else
                  {
                    if (Gültig(wneu))
                    {
                      wortTypes.Add(new WortType(w1 + " " + w2 + " " + w3 + " " + wneu, 4));
                      w1 = w2;
                      w2 = w3;
                      w3 = wneu;
                    }
                    else
                    {
                      w1 = w2 = w3 = leererString;
                    }
                  }
                }
                //wortTypes.Add(new WortType(w1 + " " + w2 + " " + w3 + " <stop>", 4));
                break;
              case 5:
                w1 = leererString;
                w2 = leererString;
                w3 = leererString;
                w4 = leererString;
                foreach (string wneu in woerter)
                {
                  if (!Gültig(w1, w2, w3, w4))
                  {
                    w1 = w2;
                    w2 = w3;
                    w3 = w4;
                    w4 = wneu;
                  }
                  else
                  {
                    if (Gültig(wneu))
                    {
                      wortTypes.Add(new WortType(w1 + " " + w2 + " " + w3 + " " + w4 + " " + wneu, 5));
                      w1 = w2;
                      w2 = w3;
                      w3 = w4;
                      w4 = wneu;
                    }
                    else
                    {
                      w1 = w2 = w3 = w4 = leererString;
                    }
                  }
                }
                //wortTypes.Add(new WortType(w1 + " " + w2 + " " + w3 + " " + w4 + " <stop>", 5));
                break;
              case 6:
                w1 = leererString;
                w2 = leererString;
                w3 = leererString;
                w4 = leererString;
                w5 = leererString;
                foreach (string wneu in woerter)
                {
                  if (!Gültig(w1, w2, w3, w4, w5))
                  {
                    w1 = w2;
                    w2 = w3;
                    w3 = w4;
                    w4 = w5;
                    w5 = wneu;
                  }
                  else
                  {
                    if (Gültig(wneu))
                    {
                      wortTypes.Add(new WortType(w1 + " " + w2 + " " + w3 + " " + w4 + " " + w5 + " " + wneu, 6));
                      w1 = w2;
                      w2 = w3;
                      w3 = w4;
                      w4 = w5;
                      w5 = wneu;
                    }
                    else
                    {
                      w1 = w2 = w3 = w4 = w5 = leererString;
                    }
                  }
                }
                //wortTypes.Add(new WortType(w1 + " " + w2 + " " + w3 + " " + w4 + " " + w5 + " <stop>", 6));
                break;
              default:
                throw new NotImplementedException(message: "case in switch fehlt");
            }
          }
          else
          {
            throw new Exception(message: "<meldung> nicht in einer Zeile");
          }
        }
      }
      return wortTypes;
    }

    private bool Gültig(string w1, string w2, string w3, string w4, string w5)
    {
      return Gültig(w1, w2, w3) && Gültig(w4, w5);
    }

    private bool Gültig(string w1, string w2, string w3, string w4)
    {
      return Gültig(w1, w2) && Gültig(w3, w4);
    }

    private bool Gültig(string w1, string w2, string w3)
    {
      return Gültig(w1) && Gültig(w2, w3);
    }

    private bool Gültig(string w1, string w2)
    {
      return Gültig(w1) && Gültig(w2);
    }

    /// <summary>
    /// gültig, wenn: nicht leer und kein Tag (also mit < und >). Überladungen für 2 bis 5 Wörter
    /// </summary>
    /// <param name="wort">zu überprüfen</param>
    /// <returns></returns>
    private static bool Gültig(string wort)
    {
      if (wort.Length < 1)
        return false;
      if (wort.Contains("<"))
        return false;
      if (wort.StartsWith("<"))
        return false;
      if (wort.EndsWith(">"))
        return false;
      return true;
    }

    private void b2Wörter_Click(object sender, EventArgs e)
    {
      Button b = (Button)sender;
      positionieren(b);
      lWörter[1] = WörterExtrahieren(lWörter[1], 2);
      if (lWörter[1].Count > 1)
      {
        lWörter[1] = WörterZusammenfassen(lWörter[1], 2);
      }
    }

    private void b3Wörter_Click(object sender, EventArgs e)
    {
      Button b = (Button)sender;
      positionieren(b);
      lWörter[2] = WörterExtrahieren(lWörter[2], 3);
      if (lWörter[2].Count > 1)
      {
        lWörter[2] = WörterZusammenfassen(lWörter[2], 3);
      }
    }

    private void b4Wörter_Click(object sender, EventArgs e)
    {
      Button b = (Button)sender;
      positionieren(b);
      lWörter[3] = WörterExtrahieren(lWörter[3], 4);
      if (lWörter[3].Count > 1)
      {
        lWörter[3] = WörterZusammenfassen(lWörter[3], 4);
      }
    }

    private void b5Wörter_Click(object sender, EventArgs e)
    {
      Button b = (Button)sender;
      positionieren(b);
      lWörter[4] = WörterExtrahieren(lWörter[4], 5);
      if (lWörter[4].Count > 1)
      {
        lWörter[4] = WörterZusammenfassen(lWörter[4], 5);
      }
    }

    private void b6Wörter_Click(object sender, EventArgs e)
    {
      Button b = (Button)sender;
      positionieren(b);
      lWörter[5] = WörterExtrahieren(lWörter[5], 6);
      if (lWörter[5].Count > 1)
      {
        lWörter[5] = WörterZusammenfassen(lWörter[5], 6);
      }
    }

    private void bAusgeben_Click(object sender, EventArgs e)
    {
      bool append = cbAnhängen.Checked;
      Button b = (Button)sender;
      b.Enabled = false;
      if (lWörter[0] != null)
      {
        WTDateiSchreiben(lWörter[0], Props.Ausgabedatei, append);
        append = true;
      }

      if (lWörter[1] != null)
      {
        WTDateiSchreiben(lWörter[1], Props.Ausgabedatei, append);
        append = true;
      }
      if (lWörter[2] != null)
      {
        WTDateiSchreiben(lWörter[2], Props.Ausgabedatei, append);
        append = true;
      }

      if (lWörter[3] != null)
      {
        WTDateiSchreiben(lWörter[3], Props.Ausgabedatei, append);
        append = true;
      }

      if (lWörter[4] != null)
      {
        WTDateiSchreiben(lWörter[4], Props.Ausgabedatei, append);
        append = true;
      }

      if (lWörter[5] != null)
      {
        WTDateiSchreiben(lWörter[5], Props.Ausgabedatei, append);
      }
      b.Enabled = true;
    }

    private void tbErgebnis_TextChanged(Object sender, EventArgs e)
    {

    }
  }
}