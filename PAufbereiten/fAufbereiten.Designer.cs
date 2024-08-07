namespace PAufbereiten
{
  partial class fAufbereiten
  {
    /// <summary>
    /// Erforderliche Designervariable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Vom Windows Form-Designer generierter Code

    /// <summary>
    /// Erforderliche Methode für die Designerunterstützung.
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent()
    {
      this.ofdEingabe = new System.Windows.Forms.OpenFileDialog();
      this.ofdAusgabe = new System.Windows.Forms.SaveFileDialog();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.pAus = new System.Windows.Forms.Panel();
      this.tbAusgabe = new System.Windows.Forms.TextBox();
      this.lAus = new System.Windows.Forms.Label();
      this.pEin = new System.Windows.Forms.Panel();
      this.tbEingabe = new System.Windows.Forms.TextBox();
      this.lEin = new System.Windows.Forms.Label();
      this.panel1 = new System.Windows.Forms.Panel();
      this.tbErgebnis = new System.Windows.Forms.TextBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.bAnzeigen = new System.Windows.Forms.Button();
      this.pb1 = new System.Windows.Forms.ProgressBar();
      this.cbAnhängen = new System.Windows.Forms.CheckBox();
      this.bAusgeben = new System.Windows.Forms.Button();
      this.cbInhalt = new System.Windows.Forms.CheckBox();
      this.cbTitel = new System.Windows.Forms.CheckBox();
      this.lEinzig = new System.Windows.Forms.Label();
      this.bVerdichten = new System.Windows.Forms.Button();
      this.lAnzal = new System.Windows.Forms.Label();
      this.bStart = new System.Windows.Forms.Button();
      this.groupBox2.SuspendLayout();
      this.pAus.SuspendLayout();
      this.pEin.SuspendLayout();
      this.panel1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // ofdEingabe
      // 
      this.ofdEingabe.DefaultExt = "*.RSS";
      this.ofdEingabe.FileName = "openFileDialog1";
      this.ofdEingabe.Filter = "RSS|*.RSS|Text|*.txt|alle|*.*";
      this.ofdEingabe.Title = "Eingabedatei RSS-Meldungen";
      // 
      // ofdAusgabe
      // 
      this.ofdAusgabe.DefaultExt = "*.mld";
      this.ofdAusgabe.Filter = "Meldungen|*.mld|Text|*.txt|alle|*.*";
      this.ofdAusgabe.Title = "Ausgabedatei aufbereitete Texte";
      // 
      // groupBox2
      // 
      this.groupBox2.AutoSize = true;
      this.groupBox2.Controls.Add(this.pAus);
      this.groupBox2.Controls.Add(this.pEin);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.groupBox2.Location = new System.Drawing.Point(0, 0);
      this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.groupBox2.Size = new System.Drawing.Size(1067, 158);
      this.groupBox2.TabIndex = 9;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Dateien";
      this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
      // 
      // pAus
      // 
      this.pAus.AutoSize = true;
      this.pAus.Controls.Add(this.tbAusgabe);
      this.pAus.Controls.Add(this.lAus);
      this.pAus.Dock = System.Windows.Forms.DockStyle.Top;
      this.pAus.Location = new System.Drawing.Point(4, 95);
      this.pAus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.pAus.Name = "pAus";
      this.pAus.Size = new System.Drawing.Size(1059, 59);
      this.pAus.TabIndex = 9;
      // 
      // tbAusgabe
      // 
      this.tbAusgabe.Location = new System.Drawing.Point(190, 10);
      this.tbAusgabe.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.tbAusgabe.Name = "tbAusgabe";
      this.tbAusgabe.Size = new System.Drawing.Size(840, 45);
      this.tbAusgabe.TabIndex = 9;
      this.tbAusgabe.Click += new System.EventHandler(this.tbAusgabe_Click);
      // 
      // lAus
      // 
      this.lAus.AutoSize = true;
      this.lAus.Dock = System.Windows.Forms.DockStyle.Left;
      this.lAus.Location = new System.Drawing.Point(0, 0);
      this.lAus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lAus.Name = "lAus";
      this.lAus.Size = new System.Drawing.Size(161, 39);
      this.lAus.TabIndex = 8;
      this.lAus.Text = "Ausgabe:";
      this.lAus.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // pEin
      // 
      this.pEin.AutoSize = true;
      this.pEin.Controls.Add(this.tbEingabe);
      this.pEin.Controls.Add(this.lEin);
      this.pEin.Dock = System.Windows.Forms.DockStyle.Top;
      this.pEin.Location = new System.Drawing.Point(4, 42);
      this.pEin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.pEin.Name = "pEin";
      this.pEin.Size = new System.Drawing.Size(1059, 53);
      this.pEin.TabIndex = 8;
      // 
      // tbEingabe
      // 
      this.tbEingabe.Location = new System.Drawing.Point(207, 4);
      this.tbEingabe.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.tbEingabe.Name = "tbEingabe";
      this.tbEingabe.Size = new System.Drawing.Size(823, 45);
      this.tbEingabe.TabIndex = 7;
      this.tbEingabe.Click += new System.EventHandler(this.tbEingabe_Click);
      // 
      // lEin
      // 
      this.lEin.AutoSize = true;
      this.lEin.Dock = System.Windows.Forms.DockStyle.Left;
      this.lEin.Location = new System.Drawing.Point(0, 0);
      this.lEin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lEin.Name = "lEin";
      this.lEin.Size = new System.Drawing.Size(152, 39);
      this.lEin.TabIndex = 6;
      this.lEin.Text = "Eingabe:";
      this.lEin.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // panel1
      // 
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel1.Controls.Add(this.tbErgebnis);
      this.panel1.Controls.Add(this.groupBox1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 158);
      this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(1067, 396);
      this.panel1.TabIndex = 10;
      // 
      // tbErgebnis
      // 
      this.tbErgebnis.BackColor = System.Drawing.Color.Black;
      this.tbErgebnis.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tbErgebnis.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tbErgebnis.ForeColor = System.Drawing.Color.White;
      this.tbErgebnis.Location = new System.Drawing.Point(538, 0);
      this.tbErgebnis.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.tbErgebnis.Multiline = true;
      this.tbErgebnis.Name = "tbErgebnis";
      this.tbErgebnis.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.tbErgebnis.Size = new System.Drawing.Size(525, 392);
      this.tbErgebnis.TabIndex = 10;
      // 
      // groupBox1
      // 
      this.groupBox1.AutoSize = true;
      this.groupBox1.Controls.Add(this.bAnzeigen);
      this.groupBox1.Controls.Add(this.pb1);
      this.groupBox1.Controls.Add(this.cbAnhängen);
      this.groupBox1.Controls.Add(this.bAusgeben);
      this.groupBox1.Controls.Add(this.cbInhalt);
      this.groupBox1.Controls.Add(this.cbTitel);
      this.groupBox1.Controls.Add(this.lEinzig);
      this.groupBox1.Controls.Add(this.bVerdichten);
      this.groupBox1.Controls.Add(this.lAnzal);
      this.groupBox1.Controls.Add(this.bStart);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
      this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.groupBox1.Location = new System.Drawing.Point(0, 0);
      this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.groupBox1.Size = new System.Drawing.Size(538, 392);
      this.groupBox1.TabIndex = 9;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "bearbeiten";
      // 
      // bAnzeigen
      // 
      this.bAnzeigen.AutoSize = true;
      this.bAnzeigen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.bAnzeigen.Location = new System.Drawing.Point(8, 160);
      this.bAnzeigen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.bAnzeigen.Name = "bAnzeigen";
      this.bAnzeigen.Size = new System.Drawing.Size(170, 49);
      this.bAnzeigen.TabIndex = 17;
      this.bAnzeigen.Text = "anzeigen";
      this.bAnzeigen.UseVisualStyleBackColor = true;
      this.bAnzeigen.Click += new System.EventHandler(this.bAnzeigen_Click);
      // 
      // pb1
      // 
      this.pb1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pb1.Location = new System.Drawing.Point(13, 352);
      this.pb1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.pb1.Name = "pb1";
      this.pb1.Size = new System.Drawing.Size(517, 28);
      this.pb1.TabIndex = 16;
      // 
      // cbAnhängen
      // 
      this.cbAnhängen.AutoSize = true;
      this.cbAnhängen.Location = new System.Drawing.Point(209, 203);
      this.cbAnhängen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.cbAnhängen.Name = "cbAnhängen";
      this.cbAnhängen.Size = new System.Drawing.Size(191, 43);
      this.cbAnhängen.TabIndex = 15;
      this.cbAnhängen.Text = "anhängen";
      this.cbAnhängen.UseVisualStyleBackColor = true;
      // 
      // bAusgeben
      // 
      this.bAusgeben.AutoSize = true;
      this.bAusgeben.Enabled = false;
      this.bAusgeben.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.bAusgeben.Location = new System.Drawing.Point(8, 200);
      this.bAusgeben.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.bAusgeben.Name = "bAusgeben";
      this.bAusgeben.Size = new System.Drawing.Size(181, 49);
      this.bAusgeben.TabIndex = 14;
      this.bAusgeben.Text = "ausgeben";
      this.bAusgeben.UseVisualStyleBackColor = true;
      this.bAusgeben.Click += new System.EventHandler(this.bAusgeben_Click);
      // 
      // cbInhalt
      // 
      this.cbInhalt.AutoSize = true;
      this.cbInhalt.Location = new System.Drawing.Point(121, 40);
      this.cbInhalt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.cbInhalt.Name = "cbInhalt";
      this.cbInhalt.Size = new System.Drawing.Size(123, 43);
      this.cbInhalt.TabIndex = 13;
      this.cbInhalt.Text = "Inhalt";
      this.cbInhalt.UseVisualStyleBackColor = true;
      // 
      // cbTitel
      // 
      this.cbTitel.AutoSize = true;
      this.cbTitel.Checked = true;
      this.cbTitel.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbTitel.Location = new System.Drawing.Point(9, 40);
      this.cbTitel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.cbTitel.Name = "cbTitel";
      this.cbTitel.Size = new System.Drawing.Size(104, 43);
      this.cbTitel.TabIndex = 12;
      this.cbTitel.Text = "Titel";
      this.cbTitel.UseVisualStyleBackColor = true;
      // 
      // lEinzig
      // 
      this.lEinzig.AutoSize = true;
      this.lEinzig.Location = new System.Drawing.Point(202, 124);
      this.lEinzig.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lEinzig.Name = "lEinzig";
      this.lEinzig.Size = new System.Drawing.Size(328, 39);
      this.lEinzig.TabIndex = 11;
      this.lEinzig.Text = "noch nicht verdichtet";
      // 
      // bVerdichten
      // 
      this.bVerdichten.AutoSize = true;
      this.bVerdichten.Enabled = false;
      this.bVerdichten.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.bVerdichten.Location = new System.Drawing.Point(8, 120);
      this.bVerdichten.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.bVerdichten.Name = "bVerdichten";
      this.bVerdichten.Size = new System.Drawing.Size(189, 49);
      this.bVerdichten.TabIndex = 10;
      this.bVerdichten.Text = "verdichten";
      this.bVerdichten.UseVisualStyleBackColor = true;
      this.bVerdichten.Click += new System.EventHandler(this.bVerdichten_Click);
      // 
      // lAnzal
      // 
      this.lAnzal.AutoSize = true;
      this.lAnzal.Location = new System.Drawing.Point(204, 84);
      this.lAnzal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lAnzal.Name = "lAnzal";
      this.lAnzal.Size = new System.Drawing.Size(282, 39);
      this.lAnzal.TabIndex = 9;
      this.lAnzal.Text = "noch keine Daten";
      // 
      // bStart
      // 
      this.bStart.AutoSize = true;
      this.bStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.bStart.Location = new System.Drawing.Point(8, 80);
      this.bStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.bStart.Name = "bStart";
      this.bStart.Size = new System.Drawing.Size(111, 49);
      this.bStart.TabIndex = 8;
      this.bStart.Text = "lesen";
      this.bStart.UseVisualStyleBackColor = true;
      this.bStart.Click += new System.EventHandler(this.bStart_Click);
      // 
      // fAufbereiten
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.BackColor = System.Drawing.Color.Black;
      this.ClientSize = new System.Drawing.Size(1067, 554);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.groupBox2);
      this.ForeColor = System.Drawing.Color.White;
      this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.Name = "fAufbereiten";
      this.Text = "Aufbereiten";
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.pAus.ResumeLayout(false);
      this.pAus.PerformLayout();
      this.pEin.ResumeLayout(false);
      this.pEin.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.OpenFileDialog ofdEingabe;
    private System.Windows.Forms.SaveFileDialog ofdAusgabe;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.TextBox tbErgebnis;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label lEinzig;
    private System.Windows.Forms.Button bVerdichten;
    private System.Windows.Forms.Label lAnzal;
    private System.Windows.Forms.Button bStart;
    private System.Windows.Forms.CheckBox cbInhalt;
    private System.Windows.Forms.CheckBox cbTitel;
    private System.Windows.Forms.Button bAusgeben;
    private System.Windows.Forms.CheckBox cbAnhängen;
    private System.Windows.Forms.ProgressBar pb1;
    private System.Windows.Forms.Button bAnzeigen;
    private System.Windows.Forms.Panel pEin;
    private System.Windows.Forms.Panel pAus;
    private System.Windows.Forms.TextBox tbAusgabe;
    private System.Windows.Forms.Label lAus;
    private System.Windows.Forms.TextBox tbEingabe;
    private System.Windows.Forms.Label lEin;
  }
}

