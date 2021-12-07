using FF.DataEntry.Api;

namespace FF.DataUI
{
    public partial class Main : Form
    {
        private Manager manager = new Manager();

        public Main()
        {
            InitializeComponent();
            this.ucOpenFile1.NewFileOpenedEvent += UcOpenFile1_NewFileOpenedEvent;
        }

        private void UcOpenFile1_NewFileOpenedEvent(object sender, Controls.NewFileOpenedEventArgs e)
        {
            _ = this.manager.InitAsync(e.FilePath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnRecords_Click(object sender, EventArgs e)
        {

        }
    }
}