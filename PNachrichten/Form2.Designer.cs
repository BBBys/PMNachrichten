namespace PNachrichten
{
  partial class fKlassifikation
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.gbKlassi = new System.Windows.Forms.GroupBox();
      this.cbUnklassifiziert = new System.Windows.Forms.CheckBox();
      this.lKlasse = new System.Windows.Forms.Label();
      this.lPosition = new System.Windows.Forms.Label();
      this.lVorkommen = new System.Windows.Forms.Label();
      this.rbStop = new System.Windows.Forms.RadioButton();
      this.rbNeg = new System.Windows.Forms.RadioButton();
      this.rbPos = new System.Windows.Forms.RadioButton();
      this.tbType = new System.Windows.Forms.TextBox();
      this.rbNeutral = new System.Windows.Forms.RadioButton();
      this.gbKlassi.SuspendLayout();
      this.SuspendLayout();
      // 
      // gbKlassi
      // 
      this.gbKlassi.Controls.Add(this.rbNeutral);
      this.gbKlassi.Controls.Add(this.cbUnklassifiziert);
      this.gbKlassi.Controls.Add(this.lKlasse);
      this.gbKlassi.Controls.Add(this.lPosition);
      this.gbKlassi.Controls.Add(this.lVorkommen);
      this.gbKlassi.Controls.Add(this.rbStop);
      this.gbKlassi.Controls.Add(this.rbNeg);
      this.gbKlassi.Controls.Add(this.rbPos);
      this.gbKlassi.Controls.Add(this.tbType);
      this.gbKlassi.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gbKlassi.Location = new System.Drawing.Point(0, 0);
      this.gbKlassi.Name = "gbKlassi";
      this.gbKlassi.Size = new System.Drawing.Size(800, 450);
      this.gbKlassi.TabIndex = 3;
      this.gbKlassi.TabStop = false;
      this.gbKlassi.Text = "Klassifizierung";
      // 
      // cbUnklassifiziert
      // 
      this.cbUnklassifiziert.AutoSize = true;
      this.cbUnklassifiziert.Location = new System.Drawing.Point(494, 61);
      this.cbUnklassifiziert.Name = "cbUnklassifiziert";
      this.cbUnklassifiziert.Size = new System.Drawing.Size(112, 17);
      this.cbUnklassifiziert.TabIndex = 8;
      this.cbUnklassifiziert.Text = "nur unklassifizierte";
      this.cbUnklassifiziert.UseVisualStyleBackColor = true;
      // 
      // lKlasse
      // 
      this.lKlasse.AutoSize = true;
      this.lKlasse.Location = new System.Drawing.Point(11, 105);
      this.lKlasse.Name = "lKlasse";
      this.lKlasse.Size = new System.Drawing.Size(44, 13);
      this.lKlasse.TabIndex = 7;
      this.lKlasse.Text = "Klasse?";
      // 
      // lPosition
      // 
      this.lPosition.AutoSize = true;
      this.lPosition.Location = new System.Drawing.Point(11, 79);
      this.lPosition.Name = "lPosition";
      this.lPosition.Size = new System.Drawing.Size(50, 13);
      this.lPosition.TabIndex = 6;
      this.lPosition.Text = "Position?";
      // 
      // lVorkommen
      // 
      this.lVorkommen.AutoSize = true;
      this.lVorkommen.Location = new System.Drawing.Point(11, 92);
      this.lVorkommen.Name = "lVorkommen";
      this.lVorkommen.Size = new System.Drawing.Size(69, 13);
      this.lVorkommen.TabIndex = 5;
      this.lVorkommen.Text = "Vorkommen?";
      // 
      // rbStop
      // 
      this.rbStop.Appearance = System.Windows.Forms.Appearance.Button;
      this.rbStop.AutoSize = true;
      this.rbStop.Location = new System.Drawing.Point(346, 42);
      this.rbStop.Name = "rbStop";
      this.rbStop.Size = new System.Drawing.Size(43, 23);
      this.rbStop.TabIndex = 3;
      this.rbStop.TabStop = true;
      this.rbStop.Text = "stopp";
      this.rbStop.UseVisualStyleBackColor = true;
      this.rbStop.Click += new System.EventHandler(this.rbStop_Click);
      // 
      // rbNeg
      // 
      this.rbNeg.Appearance = System.Windows.Forms.Appearance.Button;
      this.rbNeg.AutoSize = true;
      this.rbNeg.Location = new System.Drawing.Point(117, 42);
      this.rbNeg.Name = "rbNeg";
      this.rbNeg.Size = new System.Drawing.Size(52, 23);
      this.rbNeg.TabIndex = 2;
      this.rbNeg.TabStop = true;
      this.rbNeg.Text = "negativ";
      this.rbNeg.UseVisualStyleBackColor = true;
      this.rbNeg.Click += new System.EventHandler(this.rbNeg_Click);
      // 
      // rbPos
      // 
      this.rbPos.Appearance = System.Windows.Forms.Appearance.Button;
      this.rbPos.AutoSize = true;
      this.rbPos.Location = new System.Drawing.Point(6, 42);
      this.rbPos.Name = "rbPos";
      this.rbPos.Size = new System.Drawing.Size(47, 23);
      this.rbPos.TabIndex = 1;
      this.rbPos.TabStop = true;
      this.rbPos.Text = "positiv";
      this.rbPos.UseVisualStyleBackColor = true;
      this.rbPos.Click += new System.EventHandler(this.rbPos_Click);
      // 
      // tbType
      // 
      this.tbType.Dock = System.Windows.Forms.DockStyle.Top;
      this.tbType.Location = new System.Drawing.Point(3, 16);
      this.tbType.Name = "tbType";
      this.tbType.Size = new System.Drawing.Size(794, 20);
      this.tbType.TabIndex = 0;
      // 
      // rbNeutral
      // 
      this.rbNeutral.Appearance = System.Windows.Forms.Appearance.Button;
      this.rbNeutral.AutoSize = true;
      this.rbNeutral.Location = new System.Drawing.Point(59, 42);
      this.rbNeutral.Name = "rbNeutral";
      this.rbNeutral.Size = new System.Drawing.Size(49, 23);
      this.rbNeutral.TabIndex = 9;
      this.rbNeutral.TabStop = true;
      this.rbNeutral.Text = "neutral";
      this.rbNeutral.UseVisualStyleBackColor = true;
      this.rbNeutral.Click += new System.EventHandler(this.rbNeutral_Click);
      // 
      // fKlassifikation
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.gbKlassi);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "fKlassifikation";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Klassifikation";
      this.Shown += new System.EventHandler(this.fKlassifikation_Shown);
      this.gbKlassi.ResumeLayout(false);
      this.gbKlassi.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox gbKlassi;
    private System.Windows.Forms.Label lKlasse;
    private System.Windows.Forms.Label lPosition;
    private System.Windows.Forms.Label lVorkommen;
    private System.Windows.Forms.RadioButton rbStop;
    private System.Windows.Forms.RadioButton rbNeg;
    private System.Windows.Forms.RadioButton rbPos;
    private System.Windows.Forms.TextBox tbType;
    private System.Windows.Forms.CheckBox cbUnklassifiziert;
    private System.Windows.Forms.RadioButton rbNeutral;
  }
}