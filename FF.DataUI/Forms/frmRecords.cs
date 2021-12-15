using FF.DataEntry;

namespace FF.DataUI.Forms
{
    public partial class frmRecords : Form
    {
        public frmRecords(List<Record> records)
        {
            InitializeComponent();
            Records = records;

            dataGridView1.DataSource = records;
        }

        public List<Record> Records { get; }
    }
}
