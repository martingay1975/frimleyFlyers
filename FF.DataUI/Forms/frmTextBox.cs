using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FF.DataUI.Forms
{
    public partial class frmTextBox : Form
    {
        public frmTextBox(string label, string value = "")
        {
            InitializeComponent();

            label1.Text = label;
            textBox1.Text = value;
        }

        public string Value { get => textBox1.Text; }
    }
}
