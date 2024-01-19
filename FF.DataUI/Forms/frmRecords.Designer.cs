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
            lstNames = new ListView();
            columnName = new ColumnHeader();
            btnAdd = new Button();
            btnUpdate = new Button();
            txtName = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            ucTime5km = new Controls.ucTime();
            ucTime10km = new Controls.ucTime();
            ucTime10m = new Controls.ucTime();
            ucTimeHalfM = new Controls.ucTime();
            btnResetAllTimes = new Button();
            ucAthletesCombo1 = new Controls.ucAthletesCombo();
            btnDeleteAthlete = new Button();
            btnUpdate5kmPreviousYear = new Button();
            SuspendLayout();
            // 
            // lstNames
            // 
            lstNames.Columns.AddRange(new ColumnHeader[] { columnName });
            lstNames.Location = new Point(10, 9);
            lstNames.Margin = new Padding(3, 2, 3, 2);
            lstNames.MultiSelect = false;
            lstNames.Name = "lstNames";
            lstNames.Size = new Size(182, 338);
            lstNames.TabIndex = 0;
            lstNames.UseCompatibleStateImageBehavior = false;
            lstNames.View = View.Details;
            lstNames.SelectedIndexChanged += lstNames_SelectedIndexChanged;
            // 
            // columnName
            // 
            columnName.Text = "Name";
            columnName.Width = 200;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(217, 353);
            btnAdd.Margin = new Padding(3, 2, 3, 2);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(82, 22);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(354, 230);
            btnUpdate.Margin = new Padding(3, 2, 3, 2);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(82, 22);
            btnUpdate.TabIndex = 2;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // txtName
            // 
            txtName.Location = new Point(275, 13);
            txtName.Margin = new Padding(3, 2, 3, 2);
            txtName.Name = "txtName";
            txtName.Size = new Size(161, 23);
            txtName.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(224, 15);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 4;
            label1.Text = "Name:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(224, 69);
            label2.Name = "label2";
            label2.Size = new Size(33, 15);
            label2.TabIndex = 5;
            label2.Text = "5km:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(217, 100);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 6;
            label3.Text = "10km:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(200, 130);
            label4.Name = "label4";
            label4.Size = new Size(53, 15);
            label4.TabIndex = 7;
            label4.Text = "10 miles:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(209, 161);
            label5.Name = "label5";
            label5.Size = new Size(46, 15);
            label5.TabIndex = 8;
            label5.Text = "Half M:";
            // 
            // ucTime5km
            // 
            ucTime5km.Location = new Point(275, 62);
            ucTime5km.Margin = new Padding(3, 2, 3, 2);
            ucTime5km.Name = "ucTime5km";
            ucTime5km.Size = new Size(176, 33);
            ucTime5km.TabIndex = 9;
            ucTime5km.Time = TimeSpan.Parse("00:00:00");
            // 
            // ucTime10km
            // 
            ucTime10km.Location = new Point(275, 92);
            ucTime10km.Margin = new Padding(3, 2, 3, 2);
            ucTime10km.Name = "ucTime10km";
            ucTime10km.Size = new Size(176, 33);
            ucTime10km.TabIndex = 10;
            ucTime10km.Time = TimeSpan.Parse("00:00:00");
            // 
            // ucTime10m
            // 
            ucTime10m.Location = new Point(275, 123);
            ucTime10m.Margin = new Padding(3, 2, 3, 2);
            ucTime10m.Name = "ucTime10m";
            ucTime10m.Size = new Size(176, 33);
            ucTime10m.TabIndex = 11;
            ucTime10m.Time = TimeSpan.Parse("00:00:00");
            // 
            // ucTimeHalfM
            // 
            ucTimeHalfM.Location = new Point(275, 153);
            ucTimeHalfM.Margin = new Padding(3, 2, 3, 2);
            ucTimeHalfM.Name = "ucTimeHalfM";
            ucTimeHalfM.Size = new Size(176, 33);
            ucTimeHalfM.TabIndex = 12;
            ucTimeHalfM.Time = TimeSpan.Parse("00:00:00");
            // 
            // btnResetAllTimes
            // 
            btnResetAllTimes.Location = new Point(275, 190);
            btnResetAllTimes.Margin = new Padding(3, 2, 3, 2);
            btnResetAllTimes.Name = "btnResetAllTimes";
            btnResetAllTimes.Size = new Size(82, 22);
            btnResetAllTimes.TabIndex = 13;
            btnResetAllTimes.Text = "Reset Times";
            btnResetAllTimes.UseVisualStyleBackColor = true;
            btnResetAllTimes.Click += btnResetAllTimes_Click;
            // 
            // ucAthletesCombo1
            // 
            ucAthletesCombo1.Location = new Point(10, 353);
            ucAthletesCombo1.Name = "ucAthletesCombo1";
            ucAthletesCombo1.Size = new Size(201, 150);
            ucAthletesCombo1.TabIndex = 14;
            // 
            // btnDeleteAthlete
            // 
            btnDeleteAthlete.Location = new Point(217, 325);
            btnDeleteAthlete.Name = "btnDeleteAthlete";
            btnDeleteAthlete.Size = new Size(82, 23);
            btnDeleteAthlete.TabIndex = 15;
            btnDeleteAthlete.Text = "Delete";
            btnDeleteAthlete.UseVisualStyleBackColor = true;
            btnDeleteAthlete.Click += btnDeleteAthlete_Click;
            // 
            // btnUpdate5kmPreviousYear
            // 
            btnUpdate5kmPreviousYear.Location = new Point(337, 353);
            btnUpdate5kmPreviousYear.Name = "btnUpdate5kmPreviousYear";
            btnUpdate5kmPreviousYear.Size = new Size(92, 23);
            btnUpdate5kmPreviousYear.TabIndex = 16;
            btnUpdate5kmPreviousYear.Text = "Update 5km";
            btnUpdate5kmPreviousYear.UseVisualStyleBackColor = true;
            btnUpdate5kmPreviousYear.Click += btnUpdate5kmPreviousYear_Click;
            // 
            // frmRecords
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(448, 382);
            Controls.Add(btnUpdate5kmPreviousYear);
            Controls.Add(btnDeleteAthlete);
            Controls.Add(ucAthletesCombo1);
            Controls.Add(btnResetAllTimes);
            Controls.Add(ucTimeHalfM);
            Controls.Add(ucTime10m);
            Controls.Add(ucTime10km);
            Controls.Add(ucTime5km);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtName);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(lstNames);
            Margin = new Padding(3, 2, 3, 2);
            Name = "frmRecords";
            Text = "frmRecords";
            ResumeLayout(false);
            PerformLayout();
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
        private Button btnUpdate5kmPreviousYear;
    }
}