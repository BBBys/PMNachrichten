using Statistik.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using static cTextauswertung;

namespace Statistik
{
#pragma warning disable IDE1006 // Benennungsstile
#pragma warning disable CA1303 // Literale nicht als lokalisierte Parameter übergeben
  public partial class fStatistik : Form
  {
    private readonly Settings Props;
    private List<string> lEingabe = null;
    private List<WortType> lWörter = new List<WortType>();
    private int SollLänge = 0;
    public fStatistik()
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

#pragma warning disable IDE1006 // Benennungsstile
    private void tbAusgabe_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Benennungsstile
    {
      ofdAusgabe.FileName = tbAusgabe.Text;
      if (ofdAusgabe.ShowDialog() == DialogResult.OK)
      {
        Props.Ausgabedatei = ofdAusgabe.FileName;
        Props.Save();
        tbAusgabe.Text = Props.Ausgabedatei;
      }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Literale nicht als lokalisierte Parameter übergeben")]
    private void bLesen_Click(object sender, EventArgs e)
    {
      Button b = (Button) sender;
      b.Enabled = false;
      bAuswerten.Enabled = false;
      lEingabe = DateiLesen(Props.Eingabedatei);

      foreach (string zeile in lEingabe)
      {
        WortType w = new WortType(zeile);
        if (w.Länge == SollLänge)
        {
          lWörter.Add(w);
        }
        
      }

      bAuswerten.Enabled = true;
      lAnzahl.Text = $"Anzahl: {lWörter.Count} Types";
      lEingabe.Clear();
      lEingabe = null;
      bAusgeben.Enabled = true;
    }


    private void bZeichnen_Click(object sender, EventArgs e)
    {
      Button b = (Button) sender;
      b.Enabled = false;

      b.Enabled = true;

    }

    private void rb1_Click(object sender, EventArgs e)
    {
      RadioButton rb = (RadioButton) sender;
      SollLänge = Convert.ToInt32(rb.Text);
      lWörter.Clear();
      bLesen.Enabled = true;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Literale nicht als lokalisierte Parameter übergeben")]
    private void bAuswerten_Click(object sender, EventArgs e)
    {
      int gesamt;
      double entropie,entropieGl;
      gesamt = 0;
      entropie = 0;
      foreach (WortType wort in lWörter)
      {
        gesamt += wort.Anzahl;
      }
      tbErgebnisse.Text += $"{SollLänge}-Wort-Gruppen:" + Environment.NewLine;
      tbErgebnisse.Text += $"gesamt {gesamt} Token der Länge {SollLänge} ergeben {lWörter.Count} Types." + Environment.NewLine;
      tbErgebnisse.Text += $"Token/Type-Verhältnis {(double) gesamt / (double) lWörter.Count:F3}." + Environment.NewLine;
      foreach (WortType wort in lWörter)
      {
        wort.Gesamt = gesamt;
        entropie += wort.Entropie;
      }
      entropieGl = Math.Log(gesamt) *WortType.RLN2;
      tbErgebnisse.Text += $"Entropie {entropie:F2} Bit/Token, {entropie / SollLänge:F2} Bit/Wort" + Environment.NewLine;
      double entropiereduktion = entropieGl / entropie;
      tbErgebnisse.Text += $"bei Gleichverteilung {entropieGl:F2} (Reduktion um {entropiereduktion:F3})" + Environment.NewLine + Environment.NewLine;
    }

    /// <summary>
    /// ausgabe aller 5-buchstabigen Wörter
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void bAusgeben_Click(object sender, EventArgs e)
    {
      int gesamt = 0;
      string datei = Props.Ausgabedatei;
      using (StreamWriter objWriter = new StreamWriter        (datei, false))
      {
        foreach (WortType wort in lWörter)
        {
          if (wort.NBuchstaben == 5)
          {
            objWriter.WriteLine(wort.Wort);
          }
        }

      }
    }
  }
}