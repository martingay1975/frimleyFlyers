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
            this.btnGetLastYearParkrun = new System.Windows.Forms.Button();
            this.btnNewSeason = new System.Windows.Forms.Button();
            this.lblProgress = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ucOpenFile1
            // 
            this.ucOpenFile1.Location = new System.Drawing.Point(12, 39);
            this.ucOpenFile1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucOpenFile1.Name = "ucOpenFile1";
            this.ucOpenFile1.Size = new System.Drawing.Size(584, 29);
            this.ucOpenFile1.TabIndex = 0;
            // 
            // btnRecords
            // 
            this.btnRecords.Enabled = false;
            this.btnRecords.Location = new System.Drawing.Point(12, 85);
            this.btnRecords.Name = "btnRecords";
            this.btnRecords.Size = new System.Drawing.Size(75, 23);
            this.btnRecords.TabIndex = 1;
            this.btnRecords.Text = "Records";
            this.btnRecords.UseVisualStyleBackColor = true;
            this.btnRecords.Click += new System.EventHandler(this.btnRecords_Click);
            // 
            // btnRaces
            // 
            this.btnRaces.Enabled = false;
            this.btnRaces.Location = new System.Drawing.Point(93, 85);
            this.btnRaces.Name = "btnRaces";
            this.btnRaces.Size = new System.Drawing.Size(75, 23);
            this.btnRaces.TabIndex = 2;
            this.btnRaces.Text = "Races";
            this.btnRaces.UseVisualStyleBackColor = true;
            this.btnRaces.Click += new System.EventHandler(this.btnRaces_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(515, 85);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnGetLastYearParkrun
            // 
            this.btnGetLastYearParkrun.Location = new System.Drawing.Point(12, 113);
            this.btnGetLastYearParkrun.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGetLastYearParkrun.Name = "btnGetLastYearParkrun";
            this.btnGetLastYearParkrun.Size = new System.Drawing.Size(156, 22);
            this.btnGetLastYearParkrun.TabIndex = 4;
            this.btnGetLastYearParkrun.Text = "Refresh Parkrun Data";
            this.btnGetLastYearParkrun.UseVisualStyleBackColor = true;
            this.btnGetLastYearParkrun.Click += new System.EventHandler(this.btnRefreshParkrunData_Click);
            // 
            // btnNewSeason
            // 
            this.btnNewSeason.Location = new System.Drawing.Point(191, 113);
            this.btnNewSeason.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNewSeason.Name = "btnNewSeason";
            this.btnNewSeason.Size = new System.Drawing.Size(156, 22);
            this.btnNewSeason.TabIndex = 5;
            this.btnNewSeason.Text = "New Season";
            this.btnNewSeason.UseVisualStyleBackColor = true;
            this.btnNewSeason.Click += new System.EventHandler(this.btnNewSeason_Click);
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(12, 153);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(10, 15);
            this.lblProgress.TabIndex = 6;
            this.lblProgress.Text = ".";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 177);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.btnNewSeason);
            this.Controls.Add(this.btnGetLastYearParkrun);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnRaces);
            this.Controls.Add(this.btnRecords);
            this.Controls.Add(this.ucOpenFile1);
            this.Name = "Main";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ucOpenFile ucOpenFile1;
        private Button btnRecords;
        private Button btnRaces;
        private Button btnSave;
        private Button btnGetLastYearParkrun;
        private Button btnNewSeason;
        private Label lblProgress;
    }
}