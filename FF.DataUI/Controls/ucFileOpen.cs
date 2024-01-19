namespace FF.DataUI.Controls
{
    public partial class ucOpenFile : UserControl
    {
        public ucOpenFile()
        {
            InitializeComponent();
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\git\frimleyFlyers\site\res\json";
            openFileDialog1.Filter = "Json|*.json";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                RaiseNewFileOpenedEvent(openFileDialog1.FileName);
            }
        }

        public event NewFileOpenedEventEventHandler? NewFileOpenedEvent;
        public delegate void NewFileOpenedEventEventHandler(object sender, NewFileOpenedEventArgs e);

        private void RaiseNewFileOpenedEvent(string filePath)
        {
            this.textBox1.Text = openFileDialog1.FileName;

            NewFileOpenedEventEventHandler handler = NewFileOpenedEvent;
            if (handler != null)
            {
                handler?.Invoke(this, new NewFileOpenedEventArgs(filePath));
            }
        }
    }

    public class NewFileOpenedEventArgs : EventArgs
    {
        public NewFileOpenedEventArgs(string filePath)
        {
            this.FilePath = filePath;
        }

        public string FilePath { get; }
    }
}
