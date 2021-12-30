namespace FF.DataUI.Forms
{
    partial class frmRecords
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
            this.lstNames = new System.Windows.Forms.ListView();
            this.columnName = new System.Windows.Forms.ColumnHeader();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ucTime5km = new FF.DataUI.Controls.ucTime();
            this.ucTime10km = new FF.DataUI.Controls.ucTime();
            this.ucTime10m = new FF.DataUI.Controls.ucTime();
            this.ucTimeHalfM = new FF.DataUI.Controls.ucTime();
            this.btnResetAllTimes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstNames
            // 
            this.lstNames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName});
            this.lstNames.Location = new System.Drawing.Point(12, 12);
            this.lstNames.MultiSelect = false;
            this.lstNames.Name = "lstNames";
            this.lstNames.Size = new System.Drawing.Size(207, 450);
            this.lstNames.TabIndex = 0;
            this.lstNames.UseCompatibleStateImageBehavior = false;
            this.lstNames.View = System.Windows.Forms.View.Details;
            this.lstNames.SelectedIndexChanged += new System.EventHandler(this.lstNames_SelectedIndexChanged);
            // 
            // columnName
            // 
            this.columnName.Text = "Name";
            this.columnName.Width = 200;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 468);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(94, 29);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(426, 468);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(94, 29);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(337, 117);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(183, 27);
            this.txtName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(279, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(279, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "5km:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(271, 233);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "10km:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(252, 273);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "10 miles:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(262, 314);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Half M:";
            // 
            // ucTime5km
            // 
            this.ucTime5km.Location = new System.Drawing.Point(337, 183);
            this.ucTime5km.Name = "ucTime5km";
            this.ucTime5km.Size = new System.Drawing.Size(201, 44);
            this.ucTime5km.TabIndex = 9;
            this.ucTime5km.Time = System.TimeSpan.Parse("00:00:00");
            // 
            // ucTime10km
            // 
            this.ucTime10km.Location = new System.Drawing.Point(337, 223);
            this.ucTime10km.Name = "ucTime10km";
            this.ucTime10km.Size = new System.Drawing.Size(201, 44);
            this.ucTime10km.TabIndex = 10;
            this.ucTime10km.Time = System.TimeSpan.Parse("00:00:00");
            // 
            // ucTime10m
            // 
            this.ucTime10m.Location = new System.Drawing.Point(337, 264);
            this.ucTime10m.Name = "ucTime10m";
            this.ucTime10m.Size = new System.Drawing.Size(201, 44);
            this.ucTime10m.TabIndex = 11;
            this.ucTime10m.Time = System.TimeSpan.Parse("00:00:00");
            // 
            // ucTimeHalfM
            // 
            this.ucTimeHalfM.Location = new System.Drawing.Point(337, 304);
            this.ucTimeHalfM.Name = "ucTimeHalfM";
            this.ucTimeHalfM.Size = new System.Drawing.Size(201, 44);
            this.ucTimeHalfM.TabIndex = 12;
            this.ucTimeHalfM.Time = System.TimeSpan.Parse("00:00:00");
            // 
            // btnResetAllTimes
            // 
            this.btnResetAllTimes.Location = new System.Drawing.Point(252, 12);
            this.btnResetAllTimes.Name = "btnResetAllTimes";
            this.btnResetAllTimes.Size = new System.Drawing.Size(94, 29);
            this.btnResetAllTimes.TabIndex = 13;
            this.btnResetAllTimes.Text = "Reset All";
            this.btnResetAllTimes.UseVisualStyleBackColor = true;
            this.btnResetAllTimes.Click += new System.EventHandler(this.btnResetAllTimes_Click);
            // 
            // frmRecords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 520);
            this.Controls.Add(this.btnResetAllTimes);
            this.Controls.Add(this.ucTimeHalfM);
            this.Controls.Add(this.ucTime10m);
            this.Controls.Add(this.ucTime10km);
            this.Controls.Add(this.ucTime5km);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lstNames);
            this.Name = "frmRecords";
            this.Text = "frmRecords";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListView lstNames;
        private ColumnHeader columnName;
        private Button btnAdd;
        private Button btnUpdate;
        private TextBox txtName;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Controls.ucTime ucTime5km;
        private Controls.ucTime ucTime10km;
        private Controls.ucTime ucTime10m;
        private Controls.ucTime ucTimeHalfM;
        private Button btnResetAllTimes;
    }
}