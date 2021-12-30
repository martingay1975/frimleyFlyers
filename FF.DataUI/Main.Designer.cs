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
            this.btnRecords.Enabled = false;
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
            this.btnRaces.Enabled = false;
            this.btnRaces.Location = new System.Drawing.Point(106, 113);
            this.btnRaces.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRaces.Name = "btnRaces";
            this.btnRaces.Size = new System.Drawing.Size(86, 31);
            this.btnRaces.TabIndex = 2;
            this.btnRaces.Text = "Races";
            this.btnRaces.UseVisualStyleBackColor = true;
            this.btnRaces.Click += new System.EventHandler(this.btnRaces_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(589, 113);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(86, 31);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnGetLastYearParkrun
            // 
            this.btnGetLastYearParkrun.Location = new System.Drawing.Point(14, 151);
            this.btnGetLastYearParkrun.Name = "btnGetLastYearParkrun";
            this.btnGetLastYearParkrun.Size = new System.Drawing.Size(178, 29);
            this.btnGetLastYearParkrun.TabIndex = 4;
            this.btnGetLastYearParkrun.Text = "Get Parkrun Last Year";
            this.btnGetLastYearParkrun.UseVisualStyleBackColor = true;
            this.btnGetLastYearParkrun.Click += new System.EventHandler(this.btnGetLastYearParkrun_Click);
            // 
            // btnNewSeason
            // 
            this.btnNewSeason.Location = new System.Drawing.Point(218, 151);
            this.btnNewSeason.Name = "btnNewSeason";
            this.btnNewSeason.Size = new System.Drawing.Size(178, 29);
            this.btnNewSeason.TabIndex = 5;
            this.btnNewSeason.Text = "New Season";
            this.btnNewSeason.UseVisualStyleBackColor = true;
            this.btnNewSeason.Click += new System.EventHandler(this.btnNewSeason_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 199);
            this.Controls.Add(this.btnNewSeason);
            this.Controls.Add(this.btnGetLastYearParkrun);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnRaces);
            this.Controls.Add(this.btnRecords);
            this.Controls.Add(this.ucOpenFile1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Main";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ucOpenFile ucOpenFile1;
        private Button btnRecords;
        private Button btnRaces;
        private Button btnSave;
        private Button btnGetLastYearParkrun;
        private Button btnNewSeason;
    }
}