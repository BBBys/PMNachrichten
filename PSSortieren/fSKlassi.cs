using PSKlassifizieren.Properties;
using System;
using System.IO;
using System.Windows.Forms;

namespace PSKlassifizieren
{
  public partial class fSKlassi : Form
  {
    private readonly Settings Props;
    private readonly Random Zufall;
    private TextReader EinReader = null;
    private StreamWriter GutWriter = null, SchlechtWriter = null;
    private string Meldung;

    public fSKlassi()
    {
      InitializeComponent();
      Props = Settings.Default;
      Text = $"{Resources.Fenstertitel} V{Application.ProductVersion}. ";
#if DEBUG
      Text += "DEBUGVERSION";
#else
            Text += Application.CompanyName + ": " + Application.ProductName;
#endif
      tbGutAusgabe.Text = Props.GutAusgabedatei;
      tbSchlechtAusgabe.Text = Props.SchlechtAusgabedatei;
      tbEingabe.Text = Props.Eingabedatei;
      bGut.Enabled = false;
      bSchlecht.Enabled = false;
      bWeiter.Enabled = false;
      Zufall = new Random();
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


    private void tbSchlechtAusgabe_Click(object sender, EventArgs e)
    {
      ofdAusgabe.FileName = tbSchlechtAusgabe.Text;
      if (ofdAusgabe.ShowDialog() == DialogResult.OK)
      {
        Props.SchlechtAusgabedatei = ofdAusgabe.FileName;
        Props.Save();
        tbSchlechtAusgabe.Text = Props.SchlechtAusgabedatei;
      }
    }

    private void tbGutAusgabe_Click(object sender, EventArgs e)
    {
      ofdAusgabe.FileName = tbGutAusgabe.Text;
      if (ofdAusgabe.ShowDialog() == DialogResult.OK)
      {
        Props.GutAusgabedatei = ofdAusgabe.FileName;
        Props.Save();
        tbGutAusgabe.Text = Props.GutAusgabedatei;
      }
    }

    private void bEnde_Click(object sender, EventArgs e)
    {


      Close();
    }

    private void bStart_Click(object sender, EventArgs e)
    {
      Button b = (Button) sender;
      b.Enabled = false;
      EinReader = new StreamReader(Props.Eingabedatei);
      GutWriter = new StreamWriter(Props.GutAusgabedatei,true);
      SchlechtWriter = new StreamWriter(Props.SchlechtAusgabedatei,true);
      nächsten();
    }

    private void bWeiter_Click(object sender, EventArgs e)
    {
      nächsten();
    }

    private void bGut_Click(object sender, EventArgs e)
    {
      GutWriter.WriteLine(Meldung);
      nächsten();
    }

    private void bSchlecht_Click(object sender, EventArgs e)
    {
      SchlechtWriter.WriteLine(Meldung);
      nächsten();
    }

    private void fSKlassi_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (EinReader != null)
      {
        EinReader.Dispose();
      }

      if (GutWriter != null)
      {
        GutWriter.Dispose();
      }

      if (SchlechtWriter != null)
      {
        SchlechtWriter.Dispose();
      }
    }

    private void nächsten()
    {
      int i,n;
#if DEBUG
      n = Zufall.Next(30, 50);
      for (i = 0; i < n; i++)
      {
        EinReader.ReadLine();
      }
#endif
      if (!rbAlle.Checked)
      {
          n = Zufall.Next(700, 1300);
        if (rb1.Checked)
          n = Zufall.Next(70, 130);
        if (rb10.Checked)
          n = Zufall.Next(7, 13);
        for (i = 0; i < n; i++)
        {
          EinReader.ReadLine();
        }
      }
      Meldung = EinReader.ReadLine();
      if (Meldung==null)
      {
      tbMeldung.Text = "*** ENDE ***";
      bGut.Enabled = false;
      bSchlecht.Enabled = false;
      }
      else
      {
      tbMeldung.Text = Meldung;
      bGut.Enabled = true;
      bSchlecht.Enabled = true;
      bWeiter.Enabled = true;
      }

    }
  }
}
