using FF.DataEntry;
using FF.DataEntry.Api;
using FF.DataEntry.Dto;
using FF.DataEntry.Utils;
using FF.DataUI.Forms;

namespace FF.DataUI
{
    public partial class Main : Form
    {
        private Manager manager = new Manager();
        private string? filePath;

        public Main()
        {
            InitializeComponent();
            this.ucOpenFile1.NewFileOpenedEvent += UcOpenFile1_NewFileOpenedEvent;
        }

        private async void UcOpenFile1_NewFileOpenedEvent(object sender, Controls.NewFileOpenedEventArgs e)
        {
            this.filePath = e.FilePath;
            await this.manager.InitAsync(this.filePath);
            EnableButtons();
        }

        private void btnRecords_Click(object sender, EventArgs e)
        {
            var recordsForm = new frmRecords(this.manager.RecordsManager, manager.AthletesManager, Manager.Year);
            recordsForm.ShowDialog();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            await this.manager.SaveAsync($"{this.filePath}-1.json");
        }

        private async void btnGetLastYearParkrun_Click(object sender, EventArgs e)
        {
            await this.manager.AthletesManager.PopulateWithParkrunList(this.manager.GetBasePath(this.filePath), true);
        }

        private async void btnNewSeason_Click(object sender, EventArgs e)
        {
            this.filePath = @"C:\git\frimleyFlyers\site\res\json\raceData2022.json";
            await this.manager.CreateNewAsync(this.filePath);
            await this.manager.SaveAsync(this.filePath);
            EnableButtons();
        }

        private void EnableButtons()
        {
            foreach (var control in this.Controls)
            {
                if (control is Button button)
                {
                    button.Enabled = true;
                }
            }
        }

        private void btnRaces_Click(object sender, EventArgs e)
        {
            var frmRaces = new frmRaces(this.manager);
            frmRaces.ShowDialog();
        }
    }
}