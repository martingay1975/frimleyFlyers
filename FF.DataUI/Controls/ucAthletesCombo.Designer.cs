namespace FF.DataUI.Controls
{
    partial class ucAthletesCombo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cboAthletes = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cboAthletes
            // 
            this.cboAthletes.FormattingEnabled = true;
            this.cboAthletes.Location = new System.Drawing.Point(3, 3);
            this.cboAthletes.Name = "cboAthletes";
            this.cboAthletes.Size = new System.Drawing.Size(190, 23);
            this.cboAthletes.TabIndex = 0;
            // 
            // ucAthletesCombo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cboAthletes);
            this.Name = "ucAthletesCombo";
            this.Size = new System.Drawing.Size(196, 28);
            this.ResumeLayout(false);

        }

        #endregion

        private ComboBox cboAthletes;
    }
}
