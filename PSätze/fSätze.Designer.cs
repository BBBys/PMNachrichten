namespace PSätze
{
  partial class fSätze
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
      System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
      System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.tbAusgabe = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.tbEingabe = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.ofdEingabe = new System.Windows.Forms.OpenFileDialog();
      this.ofdAusgabe = new System.Windows.Forms.SaveFileDialog();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.lWortEnt = new System.Windows.Forms.Label();
      this.lTokEnt = new System.Windows.Forms.Label();
      this.lRatio = new System.Windows.Forms.Label();
      this.lToken = new System.Windows.Forms.Label();
      this.lTypes = new System.Windows.Forms.Label();
      this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      this.b6Wörter = new System.Windows.Forms.Button();
      this.b5Wörter = new System.Windows.Forms.Button();
      this.b4Wörter = new System.Windows.Forms.Button();
      this.b3Wörter = new System.Windows.Forms.Button();
      this.b2Wörter = new System.Windows.Forms.Button();
      this.pBar = new System.Windows.Forms.ProgressBar();
      this.cbAnhängen = new System.Windows.Forms.CheckBox();
      this.bAusgeben = new System.Windows.Forms.Button();
      this.cbInhalt = new System.Windows.Forms.CheckBox();
      this.cbTitel = new System.Windows.Forms.CheckBox();
      this.bWörter = new System.Windows.Forms.Button();
      this.lAnzahl = new System.Windows.Forms.Label();
      this.bStart = new System.Windows.Forms.Button();
      this.panel1 = new System.Windows.Forms.Panel();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.tbErgebnis = new System.Windows.Forms.TextBox();
      this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
      this.groupBox2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
      this.SuspendLayout();
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.tbAusgabe);
      this.groupBox2.Controls.Add(this.label2);
      this.groupBox2.Controls.Add(this.tbEingabe);
      this.groupBox2.Controls.Add(this.label1);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox2.Location = new System.Drawing.Point(0, 0);
      this.groupBox2.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Padding = new System.Windows.Forms.Padding(8, 10, 8, 10);
      this.groupBox2.Size = new System.Drawing.Size(2156, 100);
      this.groupBox2.TabIndex = 10;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Dateien";
      // 
      // tbAusgabe
      // 
      this.tbAusgabe.Location = new System.Drawing.Point(873, 33);
      this.tbAusgabe.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
      this.tbAusgabe.Name = "tbAusgabe";
      this.tbAusgabe.Size = new System.Drawing.Size(500, 38);
      this.tbAusgabe.TabIndex = 7;
      this.tbAusgabe.Click += new System.EventHandler(this.tbAusgabe_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(718, 36);
      this.label2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(129, 31);
      this.label2.TabIndex = 6;
      this.label2.Text = "Ausgabe:";
      this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // tbEingabe
      // 
      this.tbEingabe.Location = new System.Drawing.Point(202, 33);
      this.tbEingabe.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
      this.tbEingabe.Name = "tbEingabe";
      this.tbEingabe.Size = new System.Drawing.Size(500, 38);
      this.tbEingabe.TabIndex = 5;
      this.tbEingabe.Click += new System.EventHandler(this.tbEingabe_Click);
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(19, 43);
      this.label1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(164, 37);
      this.label1.TabIndex = 4;
      this.label1.Text = "Eingabe:";
      this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
      this.ofdAusgabe.DefaultExt = "*.wrt";
      this.ofdAusgabe.Filter = "Wörter|*.wrt|Text|*.txt|alle|*.*";
      this.ofdAusgabe.Title = "Ausgabedatei ";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.lWortEnt);
      this.groupBox1.Controls.Add(this.lTokEnt);
      this.groupBox1.Controls.Add(this.lRatio);
      this.groupBox1.Controls.Add(this.lToken);
      this.groupBox1.Controls.Add(this.lTypes);
      this.groupBox1.Controls.Add(this.flowLayoutPanel2);
      this.groupBox1.Controls.Add(this.flowLayoutPanel1);
      this.groupBox1.Controls.Add(this.b6Wörter);
      this.groupBox1.Controls.Add(this.b5Wörter);
      this.groupBox1.Controls.Add(this.b4Wörter);
      this.groupBox1.Controls.Add(this.b3Wörter);
      this.groupBox1.Controls.Add(this.b2Wörter);
      this.groupBox1.Controls.Add(this.pBar);
      this.groupBox1.Controls.Add(this.cbAnhängen);
      this.groupBox1.Controls.Add(this.bAusgeben);
      this.groupBox1.Controls.Add(this.cbInhalt);
      this.groupBox1.Controls.Add(this.cbTitel);
      this.groupBox1.Controls.Add(this.bWörter);
      this.groupBox1.Controls.Add(this.lAnzahl);
      this.groupBox1.Controls.Add(this.bStart);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
      this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.groupBox1.Location = new System.Drawing.Point(0, 100);
      this.groupBox1.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Padding = new System.Windows.Forms.Padding(8, 10, 8, 10);
      this.groupBox1.Size = new System.Drawing.Size(756, 724);
      this.groupBox1.TabIndex = 11;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "bearbeiten";
      // 
      // lWortEnt
      // 
      this.lWortEnt.AutoSize = true;
      this.lWortEnt.Location = new System.Drawing.Point(31, 429);
      this.lWortEnt.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
      this.lWortEnt.Name = "lWortEnt";
      this.lWortEnt.Size = new System.Drawing.Size(29, 31);
      this.lWortEnt.TabIndex = 33;
      this.lWortEnt.Text = "?";
      // 
      // lTokEnt
      // 
      this.lTokEnt.AutoSize = true;
      this.lTokEnt.Location = new System.Drawing.Point(31, 466);
      this.lTokEnt.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
      this.lTokEnt.Name = "lTokEnt";
      this.lTokEnt.Size = new System.Drawing.Size(29, 31);
      this.lTokEnt.TabIndex = 32;
      this.lTokEnt.Text = "?";
      // 
      // lRatio
      // 
      this.lRatio.AutoSize = true;
      this.lRatio.Location = new System.Drawing.Point(31, 485);
      this.lRatio.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
      this.lRatio.Name = "lRatio";
      this.lRatio.Size = new System.Drawing.Size(29, 31);
      this.lRatio.TabIndex = 31;
      this.lRatio.Text = "?";
      // 
      // lToken
      // 
      this.lToken.AutoSize = true;
      this.lToken.Location = new System.Drawing.Point(31, 508);
      this.lToken.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
      this.lToken.Name = "lToken";
      this.lToken.Size = new System.Drawing.Size(29, 31);
      this.lToken.TabIndex = 30;
      this.lToken.Text = "?";
      // 
      // lTypes
      // 
      this.lTypes.AutoSize = true;
      this.lTypes.Location = new System.Drawing.Point(31, 530);
      this.lTypes.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
      this.lTypes.Name = "lTypes";
      this.lTypes.Size = new System.Drawing.Size(29, 31);
      this.lTypes.TabIndex = 29;
      this.lTypes.Text = "?";
      // 
      // flowLayoutPanel2
      // 
      this.flowLayoutPanel2.Location = new System.Drawing.Point(503, 208);
      this.flowLayoutPanel2.Name = "flowLayoutPanel2";
      this.flowLayoutPanel2.Size = new System.Drawing.Size(200, 100);
      this.flowLayoutPanel2.TabIndex = 27;
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.AutoScroll = true;
      this.flowLayoutPanel1.AutoSize = true;
      this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.flowLayoutPanel1.Location = new System.Drawing.Point(11, 432);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Size = new System.Drawing.Size(0, 0);
      this.flowLayoutPanel1.TabIndex = 26;
      // 
      // b6Wörter
      // 
      this.b6Wörter.Enabled = false;
      this.b6Wörter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.b6Wörter.Location = new System.Drawing.Point(21, 379);
      this.b6Wörter.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
      this.b6Wörter.Name = "b6Wörter";
      this.b6Wörter.Size = new System.Drawing.Size(150, 40);
      this.b6Wörter.TabIndex = 24;
      this.b6Wörter.Text = "6 Wörter";
      this.b6Wörter.UseVisualStyleBackColor = true;
      this.b6Wörter.Click += new System.EventHandler(this.b6Wörter_Click);
      // 
      // b5Wörter
      // 
      this.b5Wörter.Enabled = false;
      this.b5Wörter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.b5Wörter.Location = new System.Drawing.Point(21, 335);
      this.b5Wörter.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
      this.b5Wörter.Name = "b5Wörter";
      this.b5Wörter.Size = new System.Drawing.Size(150, 40);
      this.b5Wörter.TabIndex = 23;
      this.b5Wörter.Text = "5 Wörter";
      this.b5Wörter.UseVisualStyleBackColor = true;
      this.b5Wörter.Click += new System.EventHandler(this.b5Wörter_Click);
      // 
      // b4Wörter
      // 
      this.b4Wörter.Enabled = false;
      this.b4Wörter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.b4Wörter.Location = new System.Drawing.Point(21, 294);
      this.b4Wörter.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
      this.b4Wörter.Name = "b4Wörter";
      this.b4Wörter.Size = new System.Drawing.Size(150, 40);
      this.b4Wörter.TabIndex = 22;
      this.b4Wörter.Text = "4 Wörter";
      this.b4Wörter.UseVisualStyleBackColor = true;
      this.b4Wörter.Click += new System.EventHandler(this.b4Wörter_Click);
      // 
      // b3Wörter
      // 
      this.b3Wörter.Enabled = false;
      this.b3Wörter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.b3Wörter.Location = new System.Drawing.Point(21, 251);
      this.b3Wörter.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
      this.b3Wörter.Name = "b3Wörter";
      this.b3Wörter.Size = new System.Drawing.Size(150, 40);
      this.b3Wörter.TabIndex = 21;
      this.b3Wörter.Text = "3 Wörter";
      this.b3Wörter.UseVisualStyleBackColor = true;
      this.b3Wörter.Click += new System.EventHandler(this.b3Wörter_Click);
      // 
      // b2Wörter
      // 
      this.b2Wörter.Enabled = false;
      this.b2Wörter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.b2Wörter.Location = new System.Drawing.Point(21, 208);
      this.b2Wörter.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
      this.b2Wörter.Name = "b2Wörter";
      this.b2Wörter.Size = new System.Drawing.Size(150, 40);
      this.b2Wörter.TabIndex = 20;
      this.b2Wörter.Text = "2 Wörter";
      this.b2Wörter.UseVisualStyleBackColor = true;
      this.b2Wörter.Click += new System.EventHandler(this.b2Wörter_Click);
      // 
      // pBar
      // 
      this.pBar.Location = new System.Drawing.Point(19, 908);
      this.pBar.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
      this.pBar.Name = "pBar";
      this.pBar.Size = new System.Drawing.Size(600, 64);
      this.pBar.TabIndex = 19;
      // 
      // cbAnhängen
      // 
      this.cbAnhängen.AutoSize = true;
      this.cbAnhängen.Enabled = false;
      this.cbAnhängen.Location = new System.Drawing.Point(287, 1003);
      this.cbAnhängen.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
      this.cbAnhängen.Name = "cbAnhängen";
      this.cbAnhängen.Size = new System.Drawing.Size(156, 35);
      this.cbAnhängen.TabIndex = 15;
      this.cbAnhängen.Text = "anhängen";
      this.cbAnhängen.UseVisualStyleBackColor = true;
      this.cbAnhängen.Visible = false;
      // 
      // bAusgeben
      // 
      this.bAusgeben.Enabled = false;
      this.bAusgeben.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.bAusgeben.Location = new System.Drawing.Point(19, 989);
      this.bAusgeben.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
      this.bAusgeben.Name = "bAusgeben";
      this.bAusgeben.Size = new System.Drawing.Size(236, 64);
      this.bAusgeben.TabIndex = 14;
      this.bAusgeben.Text = "ausgeben";
      this.bAusgeben.UseVisualStyleBackColor = true;
      this.bAusgeben.Click += new System.EventHandler(this.bAusgeben_Click);
      // 
      // cbInhalt
      // 
      this.cbInhalt.AutoSize = true;
      this.cbInhalt.Enabled = false;
      this.cbInhalt.Location = new System.Drawing.Point(160, 54);
      this.cbInhalt.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
      this.cbInhalt.Name = "cbInhalt";
      this.cbInhalt.Size = new System.Drawing.Size(103, 35);
      this.cbInhalt.TabIndex = 13;
      this.cbInhalt.Text = "Inhalt";
      this.cbInhalt.UseVisualStyleBackColor = true;
      this.cbInhalt.Visible = false;
      // 
      // cbTitel
      // 
      this.cbTitel.AutoSize = true;
      this.cbTitel.Checked = true;
      this.cbTitel.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbTitel.Enabled = false;
      this.cbTitel.Location = new System.Drawing.Point(21, 54);
      this.cbTitel.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
      this.cbTitel.Name = "cbTitel";
      this.cbTitel.Size = new System.Drawing.Size(88, 35);
      this.cbTitel.TabIndex = 12;
      this.cbTitel.Text = "Titel";
      this.cbTitel.UseVisualStyleBackColor = true;
      this.cbTitel.Visible = false;
      // 
      // bWörter
      // 
      this.bWörter.Enabled = false;
      this.bWörter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.bWörter.Location = new System.Drawing.Point(21, 163);
      this.bWörter.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
      this.bWörter.Name = "bWörter";
      this.bWörter.Size = new System.Drawing.Size(150, 40);
      this.bWörter.TabIndex = 10;
      this.bWörter.Text = "Wörter";
      this.bWörter.UseVisualStyleBackColor = true;
      this.bWörter.Click += new System.EventHandler(this.bWörter_Click);
      // 
      // lAnzahl
      // 
      this.lAnzahl.AutoSize = true;
      this.lAnzahl.Location = new System.Drawing.Point(187, 95);
      this.lAnzahl.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
      this.lAnzahl.Name = "lAnzahl";
      this.lAnzahl.Size = new System.Drawing.Size(225, 31);
      this.lAnzahl.TabIndex = 9;
      this.lAnzahl.Text = "noch keine Daten";
      // 
      // bStart
      // 
      this.bStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.bStart.Location = new System.Drawing.Point(21, 95);
      this.bStart.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
      this.bStart.Name = "bStart";
      this.bStart.Size = new System.Drawing.Size(150, 50);
      this.bStart.TabIndex = 8;
      this.bStart.Text = "lesen";
      this.bStart.UseVisualStyleBackColor = true;
      this.bStart.Click += new System.EventHandler(this.bStart_Click);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.splitContainer1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
      this.panel1.Location = new System.Drawing.Point(756, 100);
      this.panel1.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(1400, 724);
      this.panel1.TabIndex = 12;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.tbErgebnis);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.chart1);
      this.splitContainer1.Size = new System.Drawing.Size(1400, 724);
      this.splitContainer1.SplitterDistance = 465;
      this.splitContainer1.SplitterWidth = 12;
      this.splitContainer1.TabIndex = 1;
      // 
      // tbErgebnis
      // 
      this.tbErgebnis.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tbErgebnis.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tbErgebnis.Location = new System.Drawing.Point(0, 0);
      this.tbErgebnis.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
      this.tbErgebnis.Multiline = true;
      this.tbErgebnis.Name = "tbErgebnis";
      this.tbErgebnis.Size = new System.Drawing.Size(465, 724);
      this.tbErgebnis.TabIndex = 1;
      this.tbErgebnis.TextChanged += new System.EventHandler(this.tbErgebnis_TextChanged);
      // 
      // chart1
      // 
      chartArea1.BackColor = System.Drawing.SystemColors.Control;
      chartArea1.Name = "ChartArea1";
      this.chart1.ChartAreas.Add(chartArea1);
      this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
      legend1.Name = "Legend1";
      legend1.Title = "Verteilung von:";
      this.chart1.Legends.Add(legend1);
      this.chart1.Location = new System.Drawing.Point(0, 0);
      this.chart1.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
      this.chart1.Name = "chart1";
      this.chart1.Size = new System.Drawing.Size(923, 724);
      this.chart1.TabIndex = 0;
      this.chart1.Text = "chart1";
      // 
      // fSätze
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoScroll = true;
      this.AutoSize = true;
      this.ClientSize = new System.Drawing.Size(1722, 845);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.groupBox2);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
      this.Name = "fSätze";
      this.Text = "Sätze";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.TextBox tbAusgabe;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox tbEingabe;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.OpenFileDialog ofdEingabe;
    private System.Windows.Forms.SaveFileDialog ofdAusgabe;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox cbAnhängen;
    private System.Windows.Forms.Button bAusgeben;
    private System.Windows.Forms.CheckBox cbInhalt;
    private System.Windows.Forms.CheckBox cbTitel;
    private System.Windows.Forms.Label lAnzahl;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.ProgressBar pBar;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.TextBox tbErgebnis;
    private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    private System.Windows.Forms.Button b6Wörter;
    private System.Windows.Forms.Button b5Wörter;
    private System.Windows.Forms.Button b4Wörter;
    private System.Windows.Forms.Button b3Wörter;
    private System.Windows.Forms.Button b2Wörter;
    private System.Windows.Forms.Button bWörter;
    private System.Windows.Forms.Button bStart;
    private System.Windows.Forms.Label lWortEnt;
    private System.Windows.Forms.Label lTokEnt;
    private System.Windows.Forms.Label lRatio;
    private System.Windows.Forms.Label lToken;
    private System.Windows.Forms.Label lTypes;
  }
}

