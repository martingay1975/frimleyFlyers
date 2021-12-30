namespace FF.DataUI.Forms
{
    partial class frmRaces
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
            this.lstRaces = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lstEvents = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.label3 = new System.Windows.Forms.Label();
            this.lstAthlete = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.ucTime1 = new FF.DataUI.Controls.ucTime();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.SuspendLayout();
            // 
            // lstRaces
            // 
            this.lstRaces.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstRaces.Location = new System.Drawing.Point(12, 46);
            this.lstRaces.MultiSelect = false;
            this.lstRaces.Name = "lstRaces";
            this.lstRaces.Size = new System.Drawing.Size(137, 198);
            this.lstRaces.TabIndex = 0;
            this.lstRaces.UseCompatibleStateImageBehavior = false;
            this.lstRaces.View = System.Windows.Forms.View.Details;
            this.lstRaces.SelectedIndexChanged += new System.EventHandler(this.lstRaces_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Races";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(176, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Events";
            // 
            // lstEvents
            // 
            this.lstEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.lstEvents.Location = new System.Drawing.Point(176, 46);
            this.lstEvents.MultiSelect = false;
            this.lstEvents.Name = "lstEvents";
            this.lstEvents.Size = new System.Drawing.Size(115, 198);
            this.lstEvents.TabIndex = 2;
            this.lstEvents.UseCompatibleStateImageBehavior = false;
            this.lstEvents.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(324, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Athlete";
            // 
            // lstAthlete
            // 
            this.lstAthlete.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.lstAthlete.Location = new System.Drawing.Point(319, 46);
            this.lstAthlete.MultiSelect = false;
            this.lstAthlete.Name = "lstAthlete";
            this.lstAthlete.Size = new System.Drawing.Size(155, 198);
            this.lstAthlete.TabIndex = 4;
            this.lstAthlete.UseCompatibleStateImageBehavior = false;
            this.lstAthlete.View = System.Windows.Forms.View.Details;
            this.lstAthlete.SelectedIndexChanged += new System.EventHandler(this.lstAthlete_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "";
            this.columnHeader3.Width = 120;
            // 
            // ucTime1
            // 
            this.ucTime1.Location = new System.Drawing.Point(480, 46);
            this.ucTime1.Name = "ucTime1";
            this.ucTime1.Size = new System.Drawing.Size(201, 44);
            this.ucTime1.TabIndex = 6;
            this.ucTime1.Time = System.TimeSpan.Parse("00:00:00");
            // 
            // frmRaces
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 282);
            this.Controls.Add(this.ucTime1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstAthlete);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstEvents);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstRaces);
            this.Name = "frmRaces";
            this.Text = "frmRaces";
            this.Load += new System.EventHandler(this.frmRaces_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListView lstRaces;
        private Label label1;
        private Label label2;
        private ListView lstEvents;
        private Label label3;
        private ListView lstAthlete;
        private Controls.ucTime ucTime1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private HelpProvider helpProvider1;
    }
}