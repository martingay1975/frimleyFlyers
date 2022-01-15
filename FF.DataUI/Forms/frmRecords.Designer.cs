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
            this.ucAthletesCombo1 = new FF.DataUI.Controls.ucAthletesCombo();
            this.btnDeleteAthlete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstNames
            // 
            this.lstNames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName});
            this.lstNames.Location = new System.Drawing.Point(10, 9);
            this.lstNames.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lstNames.MultiSelect = false;
            this.lstNames.Name = "lstNames";
            this.lstNames.Size = new System.Drawing.Size(182, 338);
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
            this.btnAdd.Location = new System.Drawing.Point(217, 353);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(82, 22);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(354, 230);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(82, 22);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(275, 13);
            this.txtName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(161, 23);
            this.txtName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(224, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(224, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "5km:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(217, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "10km:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(200, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "10 miles:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(209, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Half M:";
            // 
            // ucTime5km
            // 
            this.ucTime5km.Location = new System.Drawing.Point(275, 62);
            this.ucTime5km.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ucTime5km.Name = "ucTime5km";
            this.ucTime5km.Size = new System.Drawing.Size(176, 33);
            this.ucTime5km.TabIndex = 9;
            this.ucTime5km.Time = System.TimeSpan.Parse("00:00:00");
            // 
            // ucTime10km
            // 
            this.ucTime10km.Location = new System.Drawing.Point(275, 92);
            this.ucTime10km.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ucTime10km.Name = "ucTime10km";
            this.ucTime10km.Size = new System.Drawing.Size(176, 33);
            this.ucTime10km.TabIndex = 10;
            this.ucTime10km.Time = System.TimeSpan.Parse("00:00:00");
            // 
            // ucTime10m
            // 
            this.ucTime10m.Location = new System.Drawing.Point(275, 123);
            this.ucTime10m.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ucTime10m.Name = "ucTime10m";
            this.ucTime10m.Size = new System.Drawing.Size(176, 33);
            this.ucTime10m.TabIndex = 11;
            this.ucTime10m.Time = System.TimeSpan.Parse("00:00:00");
            // 
            // ucTimeHalfM
            // 
            this.ucTimeHalfM.Location = new System.Drawing.Point(275, 153);
            this.ucTimeHalfM.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ucTimeHalfM.Name = "ucTimeHalfM";
            this.ucTimeHalfM.Size = new System.Drawing.Size(176, 33);
            this.ucTimeHalfM.TabIndex = 12;
            this.ucTimeHalfM.Time = System.TimeSpan.Parse("00:00:00");
            // 
            // btnResetAllTimes
            // 
            this.btnResetAllTimes.Location = new System.Drawing.Point(275, 190);
            this.btnResetAllTimes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnResetAllTimes.Name = "btnResetAllTimes";
            this.btnResetAllTimes.Size = new System.Drawing.Size(82, 22);
            this.btnResetAllTimes.TabIndex = 13;
            this.btnResetAllTimes.Text = "Reset Times";
            this.btnResetAllTimes.UseVisualStyleBackColor = true;
            this.btnResetAllTimes.Click += new System.EventHandler(this.btnResetAllTimes_Click);
            // 
            // ucAthletesCombo1
            // 
            this.ucAthletesCombo1.Location = new System.Drawing.Point(10, 353);
            this.ucAthletesCombo1.Name = "ucAthletesCombo1";
            this.ucAthletesCombo1.Size = new System.Drawing.Size(201, 150);
            this.ucAthletesCombo1.TabIndex = 14;
            // 
            // btnDeleteAthlete
            // 
            this.btnDeleteAthlete.Location = new System.Drawing.Point(217, 325);
            this.btnDeleteAthlete.Name = "btnDeleteAthlete";
            this.btnDeleteAthlete.Size = new System.Drawing.Size(82, 23);
            this.btnDeleteAthlete.TabIndex = 15;
            this.btnDeleteAthlete.Text = "Delete";
            this.btnDeleteAthlete.UseVisualStyleBackColor = true;
            this.btnDeleteAthlete.Click += new System.EventHandler(this.btnDeleteAthlete_Click);
            // 
            // frmRecords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 382);
            this.Controls.Add(this.btnDeleteAthlete);
            this.Controls.Add(this.ucAthletesCombo1);
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
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
        private Controls.ucAthletesCombo ucAthletesCombo1;
        private Button btnDeleteAthlete;
    }
}