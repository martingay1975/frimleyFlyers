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
            ucOpenFile1 = new FF.DataUI.Controls.ucOpenFile();
            btnRecords = new Button();
            btnRaces = new Button();
            btnSave = new Button();
            btnRefreshParkrunData = new Button();
            btnNewSeason = new Button();
            lblProgress = new Label();
            btnStats = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            chkTopTrumps = new CheckBox();
            SuspendLayout();
            // 
            // ucOpenFile1
            // 
            ucOpenFile1.Location = new Point(12, 34);
            ucOpenFile1.Margin = new Padding(3, 4, 3, 4);
            ucOpenFile1.Name = "ucOpenFile1";
            ucOpenFile1.Size = new Size(584, 29);
            ucOpenFile1.TabIndex = 0;
            // 
            // btnRecords
            // 
            btnRecords.Enabled = false;
            btnRecords.Location = new Point(16, 67);
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
            btnRaces.Location = new Point(97, 67);
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
            btnSave.Location = new Point(514, 70);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnRefreshParkrunData
            // 
            btnRefreshParkrunData.Location = new Point(16, 129);
            btnRefreshParkrunData.Margin = new Padding(3, 2, 3, 2);
            btnRefreshParkrunData.Name = "btnRefreshParkrunData";
            btnRefreshParkrunData.Size = new Size(107, 22);
            btnRefreshParkrunData.TabIndex = 4;
            btnRefreshParkrunData.Text = "Get Parkrun Data";
            btnRefreshParkrunData.UseVisualStyleBackColor = true;
            btnRefreshParkrunData.Click += btnRefreshParkrunData_Click;
            // 
            // btnNewSeason
            // 
            btnNewSeason.Location = new Point(65, 5);
            btnNewSeason.Margin = new Padding(3, 2, 3, 2);
            btnNewSeason.Name = "btnNewSeason";
            btnNewSeason.Size = new Size(75, 22);
            btnNewSeason.TabIndex = 5;
            btnNewSeason.Text = "New";
            btnNewSeason.UseVisualStyleBackColor = true;
            btnNewSeason.Click += btnNewSeason_Click;
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = true;
            lblProgress.Location = new Point(22, 238);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(10, 15);
            lblProgress.TabIndex = 6;
            lblProgress.Text = ".";
            // 
            // btnStats
            // 
            btnStats.Location = new Point(12, 202);
            btnStats.Name = "btnStats";
            btnStats.Size = new Size(75, 23);
            btnStats.TabIndex = 7;
            btnStats.Text = "Stats";
            btnStats.UseVisualStyleBackColor = true;
            btnStats.Click += btnStats_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 8;
            label1.Text = "Season:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 103);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 9;
            label2.Text = "Parkrun:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 174);
            label3.Name = "label3";
            label3.Size = new Size(48, 15);
            label3.TabIndex = 11;
            label3.Text = "Output:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(148, 133);
            label4.Name = "label4";
            label4.Size = new Size(230, 15);
            label4.TabIndex = 12;
            label4.Text = "... and updates Athletes json and RaceData";
            // 
            // chkTopTrumps
            // 
            chkTopTrumps.AutoSize = true;
            chkTopTrumps.Location = new Point(93, 206);
            chkTopTrumps.Name = "chkTopTrumps";
            chkTopTrumps.Size = new Size(89, 19);
            chkTopTrumps.TabIndex = 13;
            chkTopTrumps.Text = "Top Trumps";
            chkTopTrumps.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(601, 304);
            Controls.Add(chkTopTrumps);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnStats);
            Controls.Add(lblProgress);
            Controls.Add(btnNewSeason);
            Controls.Add(btnRefreshParkrunData);
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
        private Button btnRefreshParkrunData;
        private Label label1;
        private Label label2;
        //private Button button1;
        private Label label3;
        private Label label4;
        private CheckBox chkTopTrumps;
    }
}