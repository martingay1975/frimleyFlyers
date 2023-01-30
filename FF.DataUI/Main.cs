using FF.DataEntry.Api;
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

            this.filePath = @"C:\git\frimleyFlyers\site\res\json\raceData2022.json";
            //LoadFilePath().Wait();

        }

        private async void UcOpenFile1_NewFileOpenedEvent(object sender, Controls.NewFileOpenedEventArgs e)
        {
            this.filePath = e.FilePath;
            await LoadFilePath();
        }

        private async Task LoadFilePath()
        {
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
            await this.manager.SaveAsync($"{this.filePath}");
            await this.manager.SaveAsync($"{this.filePath}-{DateTime.Now.Ticks}.json");
            UpdateProgress($"Saved {this.filePath}");
        }

        private void UpdateProgress(string value)
        {
            this.lblProgress.Text = value;
        }

        private void ProgressHandler(int num, int outOf, string value)
        {
            var text = $"{num} / {outOf} - {value}";
            UpdateProgress(text);
        }

        private async void btnRefreshParkrunData_Click(object sender, EventArgs e)
        {
            UpdateProgress("Getting parkrun data for each athlete");
            await GetParkrunData();
            this.manager.Calculate2023();
            UpdateProgress("Got parkrun data");
        }

        private async Task GetParkrunData()
        {
            var seasonsAthletes = this.manager.RecordsManager.Records.Select(record => record.Name).ToList();
            await this.manager.AthletesManager.PopulateWithParkrunListAsync(this.manager.GetBasePath(this.filePath), seasonsAthletes, true, this.ProgressHandler);
        }

        private async void btnNewSeason_Click(object sender, EventArgs e)
        {
            this.filePath = @"C:\git\frimleyFlyers\site\res\json\raceData2023.json";
            await this.manager.CreateNewAsync(this.filePath, async () => await this.GetParkrunData());
            await this.manager.SaveAsync(this.filePath);
            EnableButtons();

            UpdateProgress($"Created new file '{this.filePath}' with {this.manager.AthletesManager.Athletes.Count()} athletes and {this.manager.RaceManager.RaceFinder.GetAllEvents().Count()} events");
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