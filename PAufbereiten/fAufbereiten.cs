using PAufbereiten.Properties;

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PAufbereiten
{
#warning Deprecated code ersetzt durch P2Eintragen
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "f")]
  public partial class fAufbereiten : Form
  {
    private readonly Settings Props;
    private readonly List<string> lTexte = new List<string>();
    public fAufbereiten()
    {
      InitializeComponent();
      Props = Settings.Default;
      Text = Resources.Fenstertitel + " V" + Application.ProductVersion + ". ";
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

    private void bStart_Click(object sender, EventArgs e)
    {
      Button b = (Button)sender;
      b.Enabled = false;
      List<string> lNachrichten;
      lNachrichten = DateiLesen(Props.Eingabedatei);
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
          if (cbTitel.Checked)
          {
            p = s.IndexOf("<title>");
            if (p >= 0)
            {
              p1 = s.IndexOf("</title>");
              if (p1 > p + 7)
              {
                s1 = s.Substring(p + 7, p1 - p - 7);
                lTexte.Add(s1);
              }
              else
              {
                throw new Exception("<title> nicht in einer Zeile");
              }
              continue;
            }
          }
          if (cbInhalt.Checked)
          {
            p = s.IndexOf("<description>");
            if (p >= 0)
            {
              p1 = s.IndexOf("</description>");
              if (p1 > p + 13)
              {
                s1 = s.Substring(p + 13, p1 - p - 13);
                lTexte.Add(s1);
              }
              else
              {
                throw new Exception("<description> nicht in einer Zeile");
              }
              continue;
            }
          }
        }
      }
      lAnzal.Text = $"Anzahl: {lTexte.Count}";
      b.Enabled = true;
      b.Text = "mehr?";
      bVerdichten.Enabled = true;
      bAusgeben.Enabled = true;
    }

    private void bVerdichten_Click(object sender, EventArgs e)
    {
      int anz;
      Button b = (Button)sender;
      b.Enabled = false;
      anz = lTexte.Count;
      if (anz > 1)
      {
        pb1.Maximum = anz;
        string text1, text2;
        lTexte.Sort();
        text1 = lTexte[0];
        //tbErgebnis.Text += text1;
        pb1.Visible = true;
        for (int i = 1; i < anz; i++)
        {
          pb1.Value = i;
          text2 = lTexte[i];
          if (text1.Equals(text2))
          {
            lTexte.RemoveAt(i);
            anz--;
            i--;
            pb1.Maximum = anz;
            if (i >= anz)
            {
              break;
            }
          }
          else
          {
            //tbErgebnis.Text += text1;
            text1 = text2;
          }
          pb1.Update();
        }
      }//if anz>1
      lTexte.TrimExcess();
      lEinzig.Text = $"unterschiedlich: {lTexte.Count}";
      pb1.Visible = false;
    }

    private void bAusgeben_Click(object sender, EventArgs e)
    {
      Button b = (Button)sender;
      b.Enabled = false;
      using (StreamWriter aus = new StreamWriter(Props.Ausgabedatei, cbAnhängen.Checked))
      {
        MeldungSpeichern(lTexte, aus);
      }
      b.Enabled = true;
    }

    private void bAnzeigen_Click(object sender, EventArgs e)
    {
      int i = 0, anz = lTexte.Count;
      pb1.Maximum = anz;
      pb1.Visible = true;
      foreach (string text in lTexte)
      {
        tbErgebnis.Text += text;
        pb1.Value = ++i;
      }
      pb1.Visible = false;
    }

    private void groupBox2_Enter(object sender, EventArgs e)
    {

    }
  }
}