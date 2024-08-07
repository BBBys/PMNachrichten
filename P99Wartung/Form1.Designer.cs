namespace P99Wartung
{
  partial class Form1
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
            this.BEnde = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.BTab = new System.Windows.Forms.Button();
            this.GbTab = new System.Windows.Forms.GroupBox();
            this.flpT = new System.Windows.Forms.FlowLayoutPanel();
            this.rbW = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.FLPW = new System.Windows.Forms.FlowLayoutPanel();
            this.CBW1 = new System.Windows.Forms.CheckBox();
            this.CBW3 = new System.Windows.Forms.CheckBox();
            this.CBW5 = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.GbTab.SuspendLayout();
            this.flpT.SuspendLayout();
            this.FLPW.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BEnde);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 425);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 25);
            this.panel1.TabIndex = 0;
            // 
            // BEnde
            // 
            this.BEnde.Dock = System.Windows.Forms.DockStyle.Right;
            this.BEnde.Location = new System.Drawing.Point(725, 0);
            this.BEnde.Name = "BEnde";
            this.BEnde.Size = new System.Drawing.Size(75, 25);
            this.BEnde.TabIndex = 0;
            this.BEnde.Text = "Ende";
            this.BEnde.UseVisualStyleBackColor = true;
            this.BEnde.Click += new System.EventHandler(this.BEnde_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel1.Controls.Add(this.BTab);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(800, 34);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // BTab
            // 
            this.BTab.Location = new System.Drawing.Point(3, 3);
            this.BTab.Name = "BTab";
            this.BTab.Size = new System.Drawing.Size(75, 25);
            this.BTab.TabIndex = 0;
            this.BTab.Text = "Tabellen";
            this.BTab.UseVisualStyleBackColor = true;
            this.BTab.Click += new System.EventHandler(this.BTab_Click);
            // 
            // GbTab
            // 
            this.GbTab.Controls.Add(this.flpT);
            this.GbTab.Controls.Add(this.FLPW);
            this.GbTab.Dock = System.Windows.Forms.DockStyle.Top;
            this.GbTab.Location = new System.Drawing.Point(0, 34);
            this.GbTab.Name = "GbTab";
            this.GbTab.Size = new System.Drawing.Size(800, 115);
            this.GbTab.TabIndex = 2;
            this.GbTab.TabStop = false;
            this.GbTab.Text = "Tabellen";
            this.GbTab.Visible = false;
            // 
            // flpT
            // 
            this.flpT.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.flpT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flpT.Controls.Add(this.rbW);
            this.flpT.Controls.Add(this.radioButton2);
            this.flpT.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpT.Location = new System.Drawing.Point(12, 18);
            this.flpT.Name = "flpT";
            this.flpT.Size = new System.Drawing.Size(79, 90);
            this.flpT.TabIndex = 1;
            // 
            // rbW
            // 
            this.rbW.AutoSize = true;
            this.rbW.Location = new System.Drawing.Point(3, 3);
            this.rbW.Name = "rbW";
            this.rbW.Size = new System.Drawing.Size(72, 21);
            this.rbW.TabIndex = 0;
            this.rbW.TabStop = true;
            this.rbW.Text = "Wörter";
            this.rbW.UseVisualStyleBackColor = true;
            this.rbW.CheckedChanged += new System.EventHandler(this.rbW_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(3, 30);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(37, 21);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "?";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // FLPW
            // 
            this.FLPW.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.FLPW.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.FLPW.Controls.Add(this.CBW1);
            this.FLPW.Controls.Add(this.CBW3);
            this.FLPW.Controls.Add(this.CBW5);
            this.FLPW.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.FLPW.Location = new System.Drawing.Point(95, 18);
            this.FLPW.Name = "FLPW";
            this.FLPW.Size = new System.Drawing.Size(100, 90);
            this.FLPW.TabIndex = 0;
            this.FLPW.Visible = false;
            // 
            // CBW1
            // 
            this.CBW1.AutoSize = true;
            this.CBW1.Location = new System.Drawing.Point(3, 3);
            this.CBW1.Name = "CBW1";
            this.CBW1.Size = new System.Drawing.Size(86, 21);
            this.CBW1.TabIndex = 0;
            this.CBW1.Text = "1-Wörter";
            this.CBW1.UseVisualStyleBackColor = true;
            // 
            // CBW3
            // 
            this.CBW3.AutoSize = true;
            this.CBW3.Location = new System.Drawing.Point(3, 30);
            this.CBW3.Name = "CBW3";
            this.CBW3.Size = new System.Drawing.Size(86, 21);
            this.CBW3.TabIndex = 1;
            this.CBW3.Text = "3-Wörter";
            this.CBW3.UseVisualStyleBackColor = true;
            // 
            // CBW5
            // 
            this.CBW5.AutoSize = true;
            this.CBW5.Location = new System.Drawing.Point(3, 57);
            this.CBW5.Name = "CBW5";
            this.CBW5.Size = new System.Drawing.Size(86, 21);
            this.CBW5.TabIndex = 2;
            this.CBW5.Text = "5-Wörter";
            this.CBW5.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.GbTab);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.GbTab.ResumeLayout(false);
            this.flpT.ResumeLayout(false);
            this.flpT.PerformLayout();
            this.FLPW.ResumeLayout(false);
            this.FLPW.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button BEnde;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.Button BTab;
    private System.Windows.Forms.GroupBox GbTab;
    private System.Windows.Forms.FlowLayoutPanel FLPW;
    private System.Windows.Forms.CheckBox CBW1;
    private System.Windows.Forms.CheckBox CBW3;
    private System.Windows.Forms.CheckBox CBW5;
    private System.Windows.Forms.FlowLayoutPanel flpT;
    private System.Windows.Forms.RadioButton rbW;
    private System.Windows.Forms.RadioButton radioButton2;
  }
}

