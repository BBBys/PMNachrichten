using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P99Wartung
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
      Assembly assembly = Assembly.GetExecutingAssembly();
      FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
      string productName = fvi.ProductName;//Assemblyinfo -> Produkt
      string programmName = fvi.FileDescription; //Assemblyinfo -> Titel
      string productVersion = fvi.ProductVersion; //Version  1.2.3.4 
    Text = $"{programmName} V{productVersion}";
#if DEBUG
      Text += " DEBUGVERSION";
#endif
      GbTab.Visible = false;
      FLPW.Visible = false;
    }

    private void BEnde_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void BTab_Click(object sender, EventArgs e)
    {
      GbTab.Visible = true;
    }

    private void rbW_CheckedChanged(object sender, EventArgs e)
    {RadioButton rb=(RadioButton)sender;
      FLPW.Visible=rb.Checked;
    }
  }
  }
