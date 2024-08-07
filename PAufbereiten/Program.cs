using System;
using System.Windows.Forms;

namespace PAufbereiten
{
  internal static class Program
  {
    /// <summary>
    /// Der Haupteinstiegspunkt für die Anwendung.
    /// </summary>
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new fAufbereiten());
    }
  }
}
