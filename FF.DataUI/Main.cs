using FF.DataEntry.Api;
using FF.DataUI.Forms;
using System.Diagnostics;

namespace FF.DataUI
{
    public partial class Main : Form
    {
        private Manager manager = new Manager();
        private string filePath;
        private const string folderPath = "C:\\git\\frimleyFlyers\\site\\res\\json";

        public Main()
        {
            InitializeComponent();
            this.ucOpenFile1.NewFileOpenedEvent += UcOpenFile1_NewFileOpenedEvent;
            this.filePath = Path.Combine(folderPath, @$"raceData{Manager.Year}.json");
            if (!File.Exists(filePath))
            {
                filePath = "";
            }
            else
            {
                LoadFilePathAsync();
            }

        }

        private async void UcOpenFile1_NewFileOpenedEvent(object sender, Controls.NewFileOpenedEventArgs e)
        {
            this.filePath = e.FilePath;
            await LoadFilePathAsync();
        }

        private async Task LoadFilePathAsync()
        {
            await this.manager.InitAsync(this.filePath);
            EnableButtons();
        }

        private void btnRecords_Click(object sender, EventArgs e)
        {
            var recordsForm = new frmRecords(this.manager.RecordsManager, manager.AthletesManager, Manager.Year, this.manager.GetBasePath(this.filePath));
            recordsForm.ShowDialog();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            await this.manager.SaveAsync($"{this.filePath}");
            await this.manager.SaveAsync($"{this.filePath}-{DateTime.Now.Ticks}.json");
            UpdateProgress($"Saved {this.filePath}");
        }

        public static void CheckThread()
        {
            if (Thread.CurrentThread.ManagedThreadId != 1)
            {
                Console.WriteLine("Not running on the UI thread");
            }
        }

        private void OpenExplorer()
        {
            var explorer = new Process
            {
                StartInfo = new ProcessStartInfo("explorer.exe", folderPath)
            };
            explorer.Start();
        }

        private void UpdateProgress(string value)
        {
            CheckThread();
            this.lblProgress.Text = value;
        }

        private void ProgressHandler(int num, int outOf, string value)
        {
            var text = $"{num} / {outOf} - {value}";
            UpdateProgress(text);
        }

        private async void btnRefreshParkrunDatandMakeCSV_Click(object sender, EventArgs e)
        {
            UpdateProgress("Fetching parkrun data for each athlete");
            CheckThread();
            await FetchParkrunDataAsync();
            CheckThread();
            UpdateProgress("Fetched parkrun data. Done");
        }

        private async Task FetchParkrunDataAsync()
        {
            Main.CheckThread();
            var seasonsAthletes = this.manager.RecordsManager.Records.Select(record => record.Name).ToList();
            await this.manager.AthletesManager.PopulateWithParkrunListAsync(this.manager.GetBasePath(this.filePath), seasonsAthletes, true, this.ProgressHandler);
            Main.CheckThread();
        }

        private async void btnNewSeason_Click(object sender, EventArgs e)
        {
            // Update Manager.Year with the new year.
            // Add names to AthletesManager before running
            // Goto Records and add or remove names as required afterwards

            this.filePath = @$"C:\git\frimleyFlyers\site\res\json\raceData{Manager.Year}.json";
            await this.manager.CreateNewAsync(this.filePath, async () => await this.FetchParkrunDataAsync());
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

        private async void btnStats_Click(object sender, EventArgs e)
        {
            var athletesPath = this.manager.GetBasePath(this.filePath);
            await this.manager.AthletesManager.PopulateAllAthletesThrottled(athletesPath, true, this.ProgressHandler);
        }

        private void CreateLeagueCsvs_Click(object sender, EventArgs e)
        {
            this.manager.CreateFFLeagueCsv(this.filePath);
            OpenExplorer();
        }
    }
}