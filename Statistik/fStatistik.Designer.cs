namespace Statistik
{
#pragma warning disable CA1303 // Literale nicht als lokalisierte Parameter übergeben
  partial class fStatistik
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
      System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
      System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
      System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.tbAusgabe = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.tbEingabe = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.ofdEingabe = new System.Windows.Forms.OpenFileDialog();
      this.ofdAusgabe = new System.Windows.Forms.SaveFileDialog();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.bAuswerten = new System.Windows.Forms.Button();
      this.bZeichnen = new System.Windows.Forms.Button();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      this.rb1 = new System.Windows.Forms.RadioButton();
      this.rb2 = new System.Windows.Forms.RadioButton();
      this.rb3 = new System.Windows.Forms.RadioButton();
      this.rb4 = new System.Windows.Forms.RadioButton();
      this.rb5 = new System.Windows.Forms.RadioButton();
      this.rb6 = new System.Windows.Forms.RadioButton();
      this.pBar = new System.Windows.Forms.ProgressBar();
      this.cbAnhängen = new System.Windows.Forms.CheckBox();
      this.bAusgeben = new System.Windows.Forms.Button();
      this.lAnzahl = new System.Windows.Forms.Label();
      this.bLesen = new System.Windows.Forms.Button();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.tbErgebnisse = new System.Windows.Forms.TextBox();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
      this.groupBox2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.flowLayoutPanel1.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
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
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(1163, 67);
      this.groupBox2.TabIndex = 11;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Dateien";
      // 
      // tbAusgabe
      // 
      this.tbAusgabe.Location = new System.Drawing.Point(64, 38);
      this.tbAusgabe.Name = "tbAusgabe";
      this.tbAusgabe.Size = new System.Drawing.Size(724, 20);
      this.tbAusgabe.TabIndex = 7;
      this.tbAusgabe.Click += new System.EventHandler(this.tbAusgabe_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 41);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(52, 13);
      this.label2.TabIndex = 6;
      this.label2.Text = "Ausgabe:";
      this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // tbEingabe
      // 
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
      // ofdEingabe
      // 
      this.ofdEingabe.DefaultExt = "*.txt";
      this.ofdEingabe.FileName = "openFileDialog1";
      this.ofdEingabe.Filter = "Wörter|*.wrt|Text|*.txt|alle|*.*";
      this.ofdEingabe.Title = "Eingabedatei aufbereitete Meldungen";
      // 
      // ofdAusgabe
      // 
      this.ofdAusgabe.DefaultExt = "*.txt";
      this.ofdAusgabe.Title = "Ausgabedatei ";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.bAuswerten);
      this.groupBox1.Controls.Add(this.bZeichnen);
      this.groupBox1.Controls.Add(this.groupBox3);
      this.groupBox1.Controls.Add(this.pBar);
      this.groupBox1.Controls.Add(this.cbAnhängen);
      this.groupBox1.Controls.Add(this.bAusgeben);
      this.groupBox1.Controls.Add(this.lAnzahl);
      this.groupBox1.Controls.Add(this.bLesen);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
      this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.groupBox1.Location = new System.Drawing.Point(0, 67);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(241, 660);
      this.groupBox1.TabIndex = 12;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "bearbeiten";
      // 
      // bAuswerten
      // 
      this.bAuswerten.Location = new System.Drawing.Point(6, 94);
      this.bAuswerten.Name = "bAuswerten";
      this.bAuswerten.Size = new System.Drawing.Size(75, 23);
      this.bAuswerten.TabIndex = 22;
      this.bAuswerten.Text = "auswerten";
      this.bAuswerten.UseVisualStyleBackColor = true;
      this.bAuswerten.Click += new System.EventHandler(this.bAuswerten_Click);
      // 
      // bZeichnen
      // 
      this.bZeichnen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.bZeichnen.Location = new System.Drawing.Point(6, 290);
      this.bZeichnen.Name = "bZeichnen";
      this.bZeichnen.Size = new System.Drawing.Size(75, 23);
      this.bZeichnen.TabIndex = 21;
      this.bZeichnen.Text = "zeichnen";
      this.bZeichnen.UseVisualStyleBackColor = true;
      this.bZeichnen.Click += new System.EventHandler(this.bZeichnen_Click);
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.flowLayoutPanel1);
      this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox3.Location = new System.Drawing.Point(3, 16);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(235, 46);
      this.groupBox3.TabIndex = 20;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "n-Wort";
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.flowLayoutPanel1.Controls.Add(this.rb1);
      this.flowLayoutPanel1.Controls.Add(this.rb2);
      this.flowLayoutPanel1.Controls.Add(this.rb3);
      this.flowLayoutPanel1.Controls.Add(this.rb4);
      this.flowLayoutPanel1.Controls.Add(this.rb5);
      this.flowLayoutPanel1.Controls.Add(this.rb6);
      this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Size = new System.Drawing.Size(229, 27);
      this.flowLayoutPanel1.TabIndex = 0;
      // 
      // rb1
      // 
      this.rb1.AutoSize = true;
      this.rb1.Location = new System.Drawing.Point(3, 3);
      this.rb1.Name = "rb1";
      this.rb1.Size = new System.Drawing.Size(31, 17);
      this.rb1.TabIndex = 0;
      this.rb1.TabStop = true;
      this.rb1.Text = "1";
      this.rb1.UseVisualStyleBackColor = true;
      this.rb1.Click += new System.EventHandler(this.rb1_Click);
      // 
      // rb2
      // 
      this.rb2.AutoSize = true;
      this.rb2.Location = new System.Drawing.Point(40, 3);
      this.rb2.Name = "rb2";
      this.rb2.Size = new System.Drawing.Size(31, 17);
      this.rb2.TabIndex = 1;
      this.rb2.TabStop = true;
      this.rb2.Text = "2";
      this.rb2.UseVisualStyleBackColor = true;
      this.rb2.Click += new System.EventHandler(this.rb1_Click);
      // 
      // rb3
      // 
      this.rb3.AutoSize = true;
      this.rb3.Location = new System.Drawing.Point(77, 3);
      this.rb3.Name = "rb3";
      this.rb3.Size = new System.Drawing.Size(31, 17);
      this.rb3.TabIndex = 2;
      this.rb3.TabStop = true;
      this.rb3.Text = "3";
      this.rb3.UseVisualStyleBackColor = true;
      this.rb3.Click += new System.EventHandler(this.rb1_Click);
      // 
      // rb4
      // 
      this.rb4.AutoSize = true;
      this.rb4.Location = new System.Drawing.Point(114, 3);
      this.rb4.Name = "rb4";
      this.rb4.Size = new System.Drawing.Size(31, 17);
      this.rb4.TabIndex = 3;
      this.rb4.TabStop = true;
      this.rb4.Text = "4";
      this.rb4.UseVisualStyleBackColor = true;
      this.rb4.Click += new System.EventHandler(this.rb1_Click);
      // 
      // rb5
      // 
      this.rb5.AutoSize = true;
      this.rb5.Location = new System.Drawing.Point(151, 3);
      this.rb5.Name = "rb5";
      this.rb5.Size = new System.Drawing.Size(31, 17);
      this.rb5.TabIndex = 4;
      this.rb5.TabStop = true;
      this.rb5.Text = "5";
      this.rb5.UseVisualStyleBackColor = true;
      this.rb5.Click += new System.EventHandler(this.rb1_Click);
      // 
      // rb6
      // 
      this.rb6.AutoSize = true;
      this.rb6.Location = new System.Drawing.Point(188, 3);
      this.rb6.Name = "rb6";
      this.rb6.Size = new System.Drawing.Size(31, 17);
      this.rb6.TabIndex = 5;
      this.rb6.TabStop = true;
      this.rb6.Text = "6";
      this.rb6.UseVisualStyleBackColor = true;
      this.rb6.Click += new System.EventHandler(this.rb1_Click);
      // 
      // pBar
      // 
      this.pBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pBar.Location = new System.Drawing.Point(6, 319);
      this.pBar.Name = "pBar";
      this.pBar.Size = new System.Drawing.Size(229, 23);
      this.pBar.TabIndex = 19;
      // 
      // cbAnhängen
      // 
      this.cbAnhängen.AutoSize = true;
      this.cbAnhängen.Enabled = false;
      this.cbAnhängen.Location = new System.Drawing.Point(91, 352);
      this.cbAnhängen.Name = "cbAnhängen";
      this.cbAnhängen.Size = new System.Drawing.Size(74, 17);
      this.cbAnhängen.TabIndex = 15;
      this.cbAnhängen.Text = "anhängen";
      this.cbAnhängen.UseVisualStyleBackColor = true;
      this.cbAnhängen.Visible = false;
      // 
      // bAusgeben
      // 
      this.bAusgeben.Enabled = false;
      this.bAusgeben.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.bAusgeben.Location = new System.Drawing.Point(6, 348);
      this.bAusgeben.Name = "bAusgeben";
      this.bAusgeben.Size = new System.Drawing.Size(75, 23);
      this.bAusgeben.TabIndex = 14;
      this.bAusgeben.Text = "ausgeben";
      this.bAusgeben.UseVisualStyleBackColor = true;
      this.bAusgeben.Click += new System.EventHandler(this.bAusgeben_Click);
      // 
      // lAnzahl
      // 
      this.lAnzahl.AutoSize = true;
      this.lAnzahl.Location = new System.Drawing.Point(87, 70);
      this.lAnzahl.Name = "lAnzahl";
      this.lAnzahl.Size = new System.Drawing.Size(92, 13);
      this.lAnzahl.TabIndex = 9;
      this.lAnzahl.Text = "noch keine Daten";
      // 
      // bLesen
      // 
      this.bLesen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.bLesen.Location = new System.Drawing.Point(6, 65);
      this.bLesen.Name = "bLesen";
      this.bLesen.Size = new System.Drawing.Size(75, 23);
      this.bLesen.TabIndex = 8;
      this.bLesen.Text = "lesen";
      this.bLesen.UseVisualStyleBackColor = true;
      this.bLesen.Click += new System.EventHandler(this.bLesen_Click);
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(241, 67);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(922, 660);
      this.tabControl1.TabIndex = 14;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.tbErgebnisse);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(914, 634);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Ergebnisse";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // tbErgebnisse
      // 
      this.tbErgebnisse.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tbErgebnisse.Location = new System.Drawing.Point(3, 3);
      this.tbErgebnisse.Multiline = true;
      this.tbErgebnisse.Name = "tbErgebnisse";
      this.tbErgebnisse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.tbErgebnisse.Size = new System.Drawing.Size(908, 628);
      this.tbErgebnisse.TabIndex = 15;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.chart1);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(914, 634);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Grafik";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // chart1
      // 
      chartArea3.Name = "ChartArea1";
      this.chart1.ChartAreas.Add(chartArea3);
      this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
      legend3.Name = "Legend1";
      this.chart1.Legends.Add(legend3);
      this.chart1.Location = new System.Drawing.Point(3, 3);
      this.chart1.Name = "chart1";
      series3.ChartArea = "ChartArea1";
      series3.Legend = "Legend1";
      series3.Name = "Series1";
      this.chart1.Series.Add(series3);
      this.chart1.Size = new System.Drawing.Size(908, 628);
      this.chart1.TabIndex = 0;
      this.chart1.Text = "chart1";
      // 
      // fStatistik
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1163, 727);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.groupBox2);
      this.Name = "fStatistik";
      this.Text = "Statistik";
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.flowLayoutPanel1.ResumeLayout(false);
      this.flowLayoutPanel1.PerformLayout();
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
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
    private System.Windows.Forms.ProgressBar pBar;
    private System.Windows.Forms.CheckBox cbAnhängen;
    private System.Windows.Forms.Button bAusgeben;
    private System.Windows.Forms.Label lAnzahl;
    private System.Windows.Forms.Button bLesen;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.RadioButton rb1;
    private System.Windows.Forms.RadioButton rb2;
    private System.Windows.Forms.RadioButton rb3;
    private System.Windows.Forms.RadioButton rb4;
    private System.Windows.Forms.RadioButton rb5;
    private System.Windows.Forms.RadioButton rb6;
    private System.Windows.Forms.Button bZeichnen;
    private System.Windows.Forms.Button bAuswerten;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    private System.Windows.Forms.TextBox tbErgebnisse;
  }
}

