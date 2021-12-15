using FF.DataEntry.Api;
using FF.DataUI.Forms;

namespace FF.DataUI
{
    public partial class Main : Form
    {
        private Manager manager = new Manager();
        private string filePath;

        public Main()
        {
            InitializeComponent();
            this.ucOpenFile1.NewFileOpenedEvent += UcOpenFile1_NewFileOpenedEvent;
        }

        private void UcOpenFile1_NewFileOpenedEvent(object sender, Controls.NewFileOpenedEventArgs e)
        {
            _ = this.manager.InitAsync(e.FilePath);
            this.filePath = e.FilePath;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnRecords_Click(object sender, EventArgs e)
        {
            var recordsForm = new frmRecords(this.manager.RecordsManager.Records);
            recordsForm.ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.manager.Save($"{this.filePath}-1.json");
        }
    }
}