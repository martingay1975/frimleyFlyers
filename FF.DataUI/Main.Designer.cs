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
            ucOpenFile1 = new Controls.ucOpenFile();
            btnRecords = new Button();
            btnRaces = new Button();
            btnSave = new Button();
            btnGetLastYearParkrun = new Button();
            btnNewSeason = new Button();
            lblProgress = new Label();
            btnStats = new Button();
            SuspendLayout();
            // 
            // ucOpenFile1
            // 
            ucOpenFile1.Location = new Point(12, 39);
            ucOpenFile1.Margin = new Padding(3, 4, 3, 4);
            ucOpenFile1.Name = "ucOpenFile1";
            ucOpenFile1.Size = new Size(584, 29);
            ucOpenFile1.TabIndex = 0;
            // 
            // btnRecords
            // 
            btnRecords.Enabled = false;
            btnRecords.Location = new Point(12, 85);
            btnRecords.Name = "btnRecords";
            btnRecords.Size = new Size(75, 23);
            btnRecords.TabIndex = 1;
            btnRecords.Text = "Records";
            btnRecords.UseVisualStyleBackColor = true;
            btnRecords.Click += btnRecords_Click;
            // 
            // btnRaces
            // 
            btnRaces.Enabled = false;
            btnRaces.Location = new Point(93, 85);
            btnRaces.Name = "btnRaces";
            btnRaces.Size = new Size(75, 23);
            btnRaces.TabIndex = 2;
            btnRaces.Text = "Races";
            btnRaces.UseVisualStyleBackColor = true;
            btnRaces.Click += btnRaces_Click;
            // 
            // btnSave
            // 
            btnSave.Enabled = false;
            btnSave.Location = new Point(515, 85);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnGetLastYearParkrun
            // 
            btnGetLastYearParkrun.Location = new Point(12, 113);
            btnGetLastYearParkrun.Margin = new Padding(3, 2, 3, 2);
            btnGetLastYearParkrun.Name = "btnGetLastYearParkrun";
            btnGetLastYearParkrun.Size = new Size(156, 22);
            btnGetLastYearParkrun.TabIndex = 4;
            btnGetLastYearParkrun.Text = "Refresh Parkrun Data";
            btnGetLastYearParkrun.UseVisualStyleBackColor = true;
            btnGetLastYearParkrun.Click += btnRefreshParkrunData_Click;
            // 
            // btnNewSeason
            // 
            btnNewSeason.Location = new Point(191, 113);
            btnNewSeason.Margin = new Padding(3, 2, 3, 2);
            btnNewSeason.Name = "btnNewSeason";
            btnNewSeason.Size = new Size(156, 22);
            btnNewSeason.TabIndex = 5;
            btnNewSeason.Text = "New Season";
            btnNewSeason.UseVisualStyleBackColor = true;
            btnNewSeason.Click += btnNewSeason_Click;
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = true;
            lblProgress.Location = new Point(12, 153);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(10, 15);
            lblProgress.TabIndex = 6;
            lblProgress.Text = ".";
            // 
            // btnStats
            // 
            btnStats.Location = new Point(374, 113);
            btnStats.Name = "btnStats";
            btnStats.Size = new Size(75, 23);
            btnStats.TabIndex = 7;
            btnStats.Text = "Stats";
            btnStats.UseVisualStyleBackColor = true;
            btnStats.Click += btnStats_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(601, 177);
            Controls.Add(btnStats);
            Controls.Add(lblProgress);
            Controls.Add(btnNewSeason);
            Controls.Add(btnGetLastYearParkrun);
            Controls.Add(btnSave);
            Controls.Add(btnRaces);
            Controls.Add(btnRecords);
            Controls.Add(ucOpenFile1);
            Name = "Main";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.ucOpenFile ucOpenFile1;
        private Button btnRecords;
        private Button btnRaces;
        private Button btnSave;
        private Button btnGetLastYearParkrun;
        private Button btnNewSeason;
        private Label lblProgress;
        private Button btnStats;
    }
}