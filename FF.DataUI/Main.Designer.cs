namespace FF.DataUI
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ucOpenFile1 = new FF.DataUI.Controls.ucOpenFile();
            this.btnRecords = new System.Windows.Forms.Button();
            this.btnRaces = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ucOpenFile1
            // 
            this.ucOpenFile1.Location = new System.Drawing.Point(14, 52);
            this.ucOpenFile1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.ucOpenFile1.Name = "ucOpenFile1";
            this.ucOpenFile1.Size = new System.Drawing.Size(667, 39);
            this.ucOpenFile1.TabIndex = 0;
            // 
            // btnRecords
            // 
            this.btnRecords.Location = new System.Drawing.Point(14, 113);
            this.btnRecords.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRecords.Name = "btnRecords";
            this.btnRecords.Size = new System.Drawing.Size(86, 31);
            this.btnRecords.TabIndex = 1;
            this.btnRecords.Text = "Records";
            this.btnRecords.UseVisualStyleBackColor = true;
            this.btnRecords.Click += new System.EventHandler(this.btnRecords_Click);
            // 
            // btnRaces
            // 
            this.btnRaces.Location = new System.Drawing.Point(106, 113);
            this.btnRaces.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRaces.Name = "btnRaces";
            this.btnRaces.Size = new System.Drawing.Size(86, 31);
            this.btnRaces.TabIndex = 2;
            this.btnRaces.Text = "Races";
            this.btnRaces.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(589, 113);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(86, 31);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 199);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnRaces);
            this.Controls.Add(this.btnRecords);
            this.Controls.Add(this.ucOpenFile1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Main";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ucOpenFile ucOpenFile1;
        private Button btnRecords;
        private Button btnRaces;
        private Button btnSave;
    }
}