using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PNachrichten
{
  public partial class fKlassifikation : Form
  {
    internal List<WortType> lWoerter;
    internal int TotalToken=0;
    private int KlassiPos = 0;

    public fKlassifikation()
    {
      InitializeComponent();
    }

    private void fKlassifikation_Shown(object sender, EventArgs e)
    {
      KlassiPos = 0;
      Anzeigen();
    }

    private void Anzeigen()
    {
      if (cbUnklassifiziert.Checked)  //wir wollen nur unklassifizierte
      {
        while (KlassiPos < lWoerter.Count)  //solange wir nicht am Ende sind
        {
          if (lWoerter[KlassiPos].Unklassifiziert())  //sollte dieser unklassifiziert sein
          {
            break;  //dann ist gut
          }
          else //ist er aber schon klassifiziert
          {
            KlassiPos++;  //dann den nächsten
          }
        }
      }
      if (KlassiPos < lWoerter.Count)
      {

        tbType.Text = lWoerter[KlassiPos].Wort;
        lPosition.Text = $"Position:   {KlassiPos + 1} von {lWoerter.Count}";
        lKlasse.Text = lWoerter[KlassiPos].Klasse.ToString();
        if (TotalToken > 0)
        {
          lVorkommen.Text = string.Format("Vorkommen: {0} mal = {1} %", lWoerter[KlassiPos].Anzahl,
            (float) lWoerter[KlassiPos].Anzahl * 100.0 / (float) TotalToken);
        }
        else
        {
          lVorkommen.Text = $"Vorkommen: {lWoerter[KlassiPos].Anzahl} mal";
        }
      }
      else
      {
        Close();
      }
    }

    private void rbPos_Click(object sender, EventArgs e)
    {
      lWoerter[KlassiPos].Klasse = WortType.eKlassen.positiv;
      Naechste();
    }

    private void Naechste()
    {
      KlassiPos++;
      Anzeigen();
    }

    private void rbNeg_Click(object sender, EventArgs e)
    {
      lWoerter[KlassiPos].Klasse = WortType.eKlassen.negativ;
      Naechste();
    }

    private void rbStop_Click(object sender, EventArgs e)
    {
      lWoerter[KlassiPos].Klasse = WortType.eKlassen.stopp;
      Naechste();
    }

    private void rbNeutral_Click(object sender, EventArgs e)
    {
      lWoerter[KlassiPos].Klasse = WortType.eKlassen.neutral;
      Naechste();
    }
  }
}
