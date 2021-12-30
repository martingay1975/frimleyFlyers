namespace FF.DataUI.Controls
{
    partial class ucTime
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
            this.txtHours = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMinutes = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSeconds = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtHours
            // 
            this.txtHours.Location = new System.Drawing.Point(2, 6);
            this.txtHours.Name = "txtHours";
            this.txtHours.Size = new System.Drawing.Size(33, 27);
            this.txtHours.TabIndex = 0;
            this.txtHours.Text = "0";
            this.txtHours.TextChanged += new System.EventHandler(this.txtHours_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "h";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(87, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "m";
            // 
            // txtMinutes
            // 
            this.txtMinutes.Location = new System.Drawing.Point(56, 6);
            this.txtMinutes.Name = "txtMinutes";
            this.txtMinutes.Size = new System.Drawing.Size(33, 27);
            this.txtMinutes.TabIndex = 2;
            this.txtMinutes.Text = "0";
            this.txtMinutes.TextChanged += new System.EventHandler(this.txtMinutes_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(142, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "s";
            // 
            // txtSeconds
            // 
            this.txtSeconds.Location = new System.Drawing.Point(111, 7);
            this.txtSeconds.Name = "txtSeconds";
            this.txtSeconds.Size = new System.Drawing.Size(33, 27);
            this.txtSeconds.TabIndex = 4;
            this.txtSeconds.Text = "0";
            this.txtSeconds.TextChanged += new System.EventHandler(this.txtSeconds_TextChanged);
            // 
            // ucTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSeconds);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMinutes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtHours);
            this.Name = "ucTime";
            this.Size = new System.Drawing.Size(161, 35);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtHours;
        private Label label1;
        private Label label2;
        private TextBox txtMinutes;
        private Label label3;
        private TextBox txtSeconds;
    }
}
