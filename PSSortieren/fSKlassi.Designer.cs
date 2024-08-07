namespace PSKlassifizieren
{
  partial class fSKlassi
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
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.tbSchlechtAusgabe = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.tbGutAusgabe = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.tbEingabe = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.tbMeldung = new System.Windows.Forms.TextBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.bEnde = new System.Windows.Forms.Button();
      this.bSchlecht = new System.Windows.Forms.Button();
      this.bWeiter = new System.Windows.Forms.Button();
      this.bGut = new System.Windows.Forms.Button();
      this.bStart = new System.Windows.Forms.Button();
      this.ofdEingabe = new System.Windows.Forms.OpenFileDialog();
      this.ofdAusgabe = new System.Windows.Forms.SaveFileDialog();
      this.rb01 = new System.Windows.Forms.RadioButton();
      this.rb1 = new System.Windows.Forms.RadioButton();
      this.rb10 = new System.Windows.Forms.RadioButton();
      this.rbAlle = new System.Windows.Forms.RadioButton();
      this.groupBox2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.tbSchlechtAusgabe);
      this.groupBox2.Controls.Add(this.label3);
      this.groupBox2.Controls.Add(this.tbGutAusgabe);
      this.groupBox2.Controls.Add(this.label2);
      this.groupBox2.Controls.Add(this.tbEingabe);
      this.groupBox2.Controls.Add(this.label1);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox2.Location = new System.Drawing.Point(0, 0);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(795, 93);
      this.groupBox2.TabIndex = 11;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Dateien";
      // 
      // tbSchlechtAusgabe
      // 
      this.tbSchlechtAusgabe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbSchlechtAusgabe.Location = new System.Drawing.Point(64, 63);
      this.tbSchlechtAusgabe.Name = "tbSchlechtAusgabe";
      this.tbSchlechtAusgabe.Size = new System.Drawing.Size(724, 20);
      this.tbSchlechtAusgabe.TabIndex = 9;
      this.tbSchlechtAusgabe.Click += new System.EventHandler(this.tbSchlechtAusgabe_Click);
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(6, 65);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(52, 13);
      this.label3.TabIndex = 8;
      this.label3.Text = "schlecht:";
      this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // tbGutAusgabe
      // 
      this.tbGutAusgabe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbGutAusgabe.Location = new System.Drawing.Point(64, 38);
      this.tbGutAusgabe.Name = "tbGutAusgabe";
      this.tbGutAusgabe.Size = new System.Drawing.Size(724, 20);
      this.tbGutAusgabe.TabIndex = 7;
      this.tbGutAusgabe.Click += new System.EventHandler(this.tbGutAusgabe_Click);
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(6, 40);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(52, 13);
      this.label2.TabIndex = 6;
      this.label2.Text = "gut:";
      this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // tbEingabe
      // 
      this.tbEingabe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbEingabe.Location = new System.Drawing.Point(64, 12);
      this.tbEingabe.Name = "tbEingabe";
      this.tbEingabe.Size = new System.Drawing.Size(724, 20);
      this.tbEingabe.TabIndex = 5;
      this.tbEingabe.Click += new System.EventHandler(this.tbEingabe_Click);
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(6, 15);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(52, 13);
      this.label1.TabIndex = 4;
      this.label1.Text = "Eingabe:";
      this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.tbMeldung);
      this.groupBox1.Controls.Add(this.panel1);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox1.Location = new System.Drawing.Point(0, 93);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(795, 357);
      this.groupBox1.TabIndex = 12;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "bearbeiten";
      // 
      // tbMeldung
      // 
      this.tbMeldung.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tbMeldung.Location = new System.Drawing.Point(3, 16);
      this.tbMeldung.Multiline = true;
      this.tbMeldung.Name = "tbMeldung";
      this.tbMeldung.Size = new System.Drawing.Size(789, 238);
      this.tbMeldung.TabIndex = 1;
      // 
      // panel1
      // 
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel1.Controls.Add(this.rbAlle);
      this.panel1.Controls.Add(this.rb10);
      this.panel1.Controls.Add(this.rb1);
      this.panel1.Controls.Add(this.rb01);
      this.panel1.Controls.Add(this.bEnde);
      this.panel1.Controls.Add(this.bSchlecht);
      this.panel1.Controls.Add(this.bWeiter);
      this.panel1.Controls.Add(this.bGut);
      this.panel1.Controls.Add(this.bStart);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(3, 254);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(789, 100);
      this.panel1.TabIndex = 0;
      // 
      // bEnde
      // 
      this.bEnde.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.bEnde.Location = new System.Drawing.Point(703, 64);
      this.bEnde.Name = "bEnde";
      this.bEnde.Size = new System.Drawing.Size(75, 23);
      this.bEnde.TabIndex = 4;
      this.bEnde.Text = "Ende";
      this.bEnde.UseVisualStyleBackColor = true;
      this.bEnde.Click += new System.EventHandler(this.bEnde_Click);
      // 
      // bSchlecht
      // 
      this.bSchlecht.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bSchlecht.Location = new System.Drawing.Point(543, 3);
      this.bSchlecht.Name = "bSchlecht";
      this.bSchlecht.Size = new System.Drawing.Size(143, 84);
      this.bSchlecht.TabIndex = 3;
      this.bSchlecht.Text = "schlecht";
      this.bSchlecht.UseVisualStyleBackColor = true;
      this.bSchlecht.Click += new System.EventHandler(this.bSchlecht_Click);
      // 
      // bWeiter
      // 
      this.bWeiter.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bWeiter.Location = new System.Drawing.Point(394, 3);
      this.bWeiter.Name = "bWeiter";
      this.bWeiter.Size = new System.Drawing.Size(143, 84);
      this.bWeiter.TabIndex = 2;
      this.bWeiter.Text = "weiter";
      this.bWeiter.UseVisualStyleBackColor = true;
      this.bWeiter.Click += new System.EventHandler(this.bWeiter_Click);
      // 
      // bGut
      // 
      this.bGut.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bGut.Location = new System.Drawing.Point(245, 3);
      this.bGut.Name = "bGut";
      this.bGut.Size = new System.Drawing.Size(143, 84);
      this.bGut.TabIndex = 1;
      this.bGut.Text = "gut";
      this.bGut.UseVisualStyleBackColor = true;
      this.bGut.Click += new System.EventHandler(this.bGut_Click);
      // 
      // bStart
      // 
      this.bStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.bStart.Location = new System.Drawing.Point(164, 64);
      this.bStart.Name = "bStart";
      this.bStart.Size = new System.Drawing.Size(75, 23);
      this.bStart.TabIndex = 0;
      this.bStart.Text = "Start";
      this.bStart.UseVisualStyleBackColor = true;
      this.bStart.Click += new System.EventHandler(this.bStart_Click);
      // 
      // ofdEingabe
      // 
      this.ofdEingabe.DefaultExt = "*.mld";
      this.ofdEingabe.FileName = "openFileDialog1";
      this.ofdEingabe.Filter = "Meldungen|*.mld|Text|*.txt|alle|*.*";
      this.ofdEingabe.Title = "Eingabedatei aufbereitete Meldungen";
      // 
      // ofdAusgabe
      // 
      this.ofdAusgabe.DefaultExt = "*.mld";
      this.ofdAusgabe.Filter = "Meldungen|*.mld|Text|*.txt|alle|*.*";
      this.ofdAusgabe.Title = "Ausgabedatei ";
      // 
      // rb01
      // 
      this.rb01.AutoSize = true;
      this.rb01.Location = new System.Drawing.Point(7, 4);
      this.rb01.Name = "rb01";
      this.rb01.Size = new System.Drawing.Size(51, 17);
      this.rb01.TabIndex = 5;
      this.rb01.Text = "0,1 %";
      this.rb01.UseVisualStyleBackColor = true;
      // 
      // rb1
      // 
      this.rb1.AutoSize = true;
      this.rb1.Location = new System.Drawing.Point(7, 25);
      this.rb1.Name = "rb1";
      this.rb1.Size = new System.Drawing.Size(42, 17);
      this.rb1.TabIndex = 6;
      this.rb1.Text = "1 %";
      this.rb1.UseVisualStyleBackColor = true;
      // 
      // rb10
      // 
      this.rb10.AutoSize = true;
      this.rb10.Location = new System.Drawing.Point(6, 46);
      this.rb10.Name = "rb10";
      this.rb10.Size = new System.Drawing.Size(48, 17);
      this.rb10.TabIndex = 7;
      this.rb10.Text = "10 %";
      this.rb10.UseVisualStyleBackColor = true;
      // 
      // rbAlle
      // 
      this.rbAlle.AutoSize = true;
      this.rbAlle.Checked = true;
      this.rbAlle.Location = new System.Drawing.Point(6, 67);
      this.rbAlle.Name = "rbAlle";
      this.rbAlle.Size = new System.Drawing.Size(41, 17);
      this.rbAlle.TabIndex = 8;
      this.rbAlle.TabStop = true;
      this.rbAlle.Text = "alle";
      this.rbAlle.UseVisualStyleBackColor = true;
      // 
      // fSKlassi
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(795, 450);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.groupBox2);
      this.Name = "fSKlassi";
      this.Text = "Form1";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fSKlassi_FormClosing);
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.TextBox tbSchlechtAusgabe;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox tbGutAusgabe;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox tbEingabe;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox tbMeldung;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button bEnde;
    private System.Windows.Forms.Button bSchlecht;
    private System.Windows.Forms.Button bWeiter;
    private System.Windows.Forms.Button bGut;
    private System.Windows.Forms.Button bStart;
    private System.Windows.Forms.OpenFileDialog ofdEingabe;
    private System.Windows.Forms.SaveFileDialog ofdAusgabe;
    private System.Windows.Forms.RadioButton rb10;
    private System.Windows.Forms.RadioButton rb1;
    private System.Windows.Forms.RadioButton rb01;
    private System.Windows.Forms.RadioButton rbAlle;
  }
}

