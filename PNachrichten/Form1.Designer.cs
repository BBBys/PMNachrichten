namespace PNachrichten
{
  partial class fNachrichten
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
      this.panel1 = new System.Windows.Forms.Panel();
      this.rbSpiegel = new System.Windows.Forms.RadioButton();
      this.rbTagesschau = new System.Windows.Forms.RadioButton();
      this.bKlass = new System.Windows.Forms.Button();
      this.bAuswerten = new System.Windows.Forms.Button();
      this.bLesen = new System.Windows.Forms.Button();
      this.bSpeichern = new System.Windows.Forms.Button();
      this.bAbfrage = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.lRatio = new System.Windows.Forms.Label();
      this.lToken = new System.Windows.Forms.Label();
      this.lTypes = new System.Windows.Forms.Label();
      this.tbErgebnis = new System.Windows.Forms.TextBox();
      this.panel1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel1.Controls.Add(this.rbSpiegel);
      this.panel1.Controls.Add(this.rbTagesschau);
      this.panel1.Controls.Add(this.bKlass);
      this.panel1.Controls.Add(this.bAuswerten);
      this.panel1.Controls.Add(this.bLesen);
      this.panel1.Controls.Add(this.bSpeichern);
      this.panel1.Controls.Add(this.bAbfrage);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 417);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(800, 33);
      this.panel1.TabIndex = 0;
      // 
      // rbSpiegel
      // 
      this.rbSpiegel.AutoSize = true;
      this.rbSpiegel.Location = new System.Drawing.Point(499, 7);
      this.rbSpiegel.Name = "rbSpiegel";
      this.rbSpiegel.Size = new System.Drawing.Size(70, 17);
      this.rbSpiegel.TabIndex = 6;
      this.rbSpiegel.TabStop = true;
      this.rbSpiegel.Text = "SPIEGEL";
      this.rbSpiegel.UseVisualStyleBackColor = true;
      this.rbSpiegel.CheckedChanged += new System.EventHandler(this.rbSpiegel_CheckedChanged);
      // 
      // rbTagesschau
      // 
      this.rbTagesschau.AutoSize = true;
      this.rbTagesschau.Location = new System.Drawing.Point(408, 7);
      this.rbTagesschau.Name = "rbTagesschau";
      this.rbTagesschau.Size = new System.Drawing.Size(84, 17);
      this.rbTagesschau.TabIndex = 5;
      this.rbTagesschau.TabStop = true;
      this.rbTagesschau.Text = "Tagesschau";
      this.rbTagesschau.UseVisualStyleBackColor = true;
      this.rbTagesschau.CheckedChanged += new System.EventHandler(this.rbSpiegel_CheckedChanged);
      // 
      // bKlass
      // 
      this.bKlass.Location = new System.Drawing.Point(327, 4);
      this.bKlass.Name = "bKlass";
      this.bKlass.Size = new System.Drawing.Size(75, 23);
      this.bKlass.TabIndex = 4;
      this.bKlass.Text = "klassifizieren";
      this.bKlass.UseVisualStyleBackColor = true;
      this.bKlass.Click += new System.EventHandler(this.bKlass_Click);
      // 
      // bAuswerten
      // 
      this.bAuswerten.Location = new System.Drawing.Point(246, 3);
      this.bAuswerten.Name = "bAuswerten";
      this.bAuswerten.Size = new System.Drawing.Size(75, 23);
      this.bAuswerten.TabIndex = 3;
      this.bAuswerten.Text = "auswerten";
      this.bAuswerten.UseVisualStyleBackColor = true;
      this.bAuswerten.Click += new System.EventHandler(this.bAuswerten_Click);
      // 
      // bLesen
      // 
      this.bLesen.Location = new System.Drawing.Point(165, 3);
      this.bLesen.Name = "bLesen";
      this.bLesen.Size = new System.Drawing.Size(75, 23);
      this.bLesen.TabIndex = 2;
      this.bLesen.Text = "lesen";
      this.bLesen.UseVisualStyleBackColor = true;
      this.bLesen.Click += new System.EventHandler(this.bLesen_Click);
      // 
      // bSpeichern
      // 
      this.bSpeichern.Location = new System.Drawing.Point(84, 3);
      this.bSpeichern.Name = "bSpeichern";
      this.bSpeichern.Size = new System.Drawing.Size(75, 23);
      this.bSpeichern.TabIndex = 1;
      this.bSpeichern.Text = "speichern";
      this.bSpeichern.UseVisualStyleBackColor = true;
      this.bSpeichern.Click += new System.EventHandler(this.bSpeichern_Click);
      // 
      // bAbfrage
      // 
      this.bAbfrage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.bAbfrage.Enabled = false;
      this.bAbfrage.Location = new System.Drawing.Point(3, 3);
      this.bAbfrage.Name = "bAbfrage";
      this.bAbfrage.Size = new System.Drawing.Size(75, 23);
      this.bAbfrage.TabIndex = 0;
      this.bAbfrage.Text = "Abfrage";
      this.bAbfrage.UseVisualStyleBackColor = true;
      this.bAbfrage.Click += new System.EventHandler(this.bAbfrage_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.lRatio);
      this.groupBox1.Controls.Add(this.lToken);
      this.groupBox1.Controls.Add(this.lTypes);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
      this.groupBox1.Location = new System.Drawing.Point(0, 0);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(375, 417);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Statistik";
      // 
      // lRatio
      // 
      this.lRatio.AutoSize = true;
      this.lRatio.Location = new System.Drawing.Point(7, 42);
      this.lRatio.Name = "lRatio";
      this.lRatio.Size = new System.Drawing.Size(59, 13);
      this.lRatio.TabIndex = 2;
      this.lRatio.Text = "Verhältnis?";
      // 
      // lToken
      // 
      this.lToken.AutoSize = true;
      this.lToken.Location = new System.Drawing.Point(7, 29);
      this.lToken.Name = "lToken";
      this.lToken.Size = new System.Drawing.Size(44, 13);
      this.lToken.TabIndex = 1;
      this.lToken.Text = "Token?";
      // 
      // lTypes
      // 
      this.lTypes.AutoSize = true;
      this.lTypes.Location = new System.Drawing.Point(7, 16);
      this.lTypes.Name = "lTypes";
      this.lTypes.Size = new System.Drawing.Size(42, 13);
      this.lTypes.TabIndex = 0;
      this.lTypes.Text = "Types?";
      // 
      // tbErgebnis
      // 
      this.tbErgebnis.Dock = System.Windows.Forms.DockStyle.Right;
      this.tbErgebnis.Location = new System.Drawing.Point(381, 0);
      this.tbErgebnis.Multiline = true;
      this.tbErgebnis.Name = "tbErgebnis";
      this.tbErgebnis.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.tbErgebnis.Size = new System.Drawing.Size(419, 417);
      this.tbErgebnis.TabIndex = 2;
      // 
      // fNachrichten
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.tbErgebnis);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.panel1);
      this.Name = "fNachrichten";
      this.Text = "Nachrichten";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button bAbfrage;
    private System.Windows.Forms.Button bSpeichern;
    private System.Windows.Forms.Button bLesen;
    private System.Windows.Forms.Button bAuswerten;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label lTypes;
    private System.Windows.Forms.Label lToken;
    private System.Windows.Forms.Label lRatio;
    private System.Windows.Forms.Button bKlass;
    private System.Windows.Forms.TextBox tbErgebnis;
    private System.Windows.Forms.RadioButton rbSpiegel;
    private System.Windows.Forms.RadioButton rbTagesschau;
  }
}

