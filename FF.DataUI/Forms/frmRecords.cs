using FF.DataEntry.Api;
using FF.DataEntry.Dto;
using FF.DataEntry.Utils;

namespace FF.DataUI.Forms
{
    public partial class frmRecords : Form
    {
        private readonly AthletesManager athletesManager;
        private int year;
        private string basePath;
        public RecordsManager RecordsManager { get; }

        public frmRecords(RecordsManager recordsManager, AthletesManager athletesManager, int year, string basePath)
        {
            InitializeComponent();
            this.RecordsManager = recordsManager;
            this.athletesManager = athletesManager ?? throw new ArgumentNullException(nameof(athletesManager));
            this.year = year;
            this.ucAthletesCombo1.Initialize(athletesManager);
            this.basePath = basePath;
            RefreshAthleteList();

        }

        private Record? GetSelectedRecord()
        {
            if (this.lstNames.SelectedItems.Count == 0)
            {
                return null;
            }

            var name = this.lstNames.SelectedItems[0].Text;
            return RecordsManager.Records.Single(record => record.Name == name);
        }

        private void RefreshAthleteList()
        {
            this.lstNames.Items.Clear();

            foreach (var record in this.RecordsManager.Records.OrderBy(record => record.Name))
            {
                this.lstNames.Items.Add(record.Name);
            }

            var athleteNames = this.lstNames.Items
                .Cast<ListViewItem>()
                .Select(i => i.Text)
                .ToList();

            this.ucAthletesCombo1.RefreshControl(athleteNames);
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            await UpdateAsync();
        }

        private void UpdateAllPreviousYearsBestParkrun()
        {
            foreach (var record in this.RecordsManager.Records)
            {
                var name = record.Name;

                // get last years best 5km time and set the record.FiveKm time with it
                var time5km = GetQuickestParkrunTime(name, year);
                record.FiveKm.SetTime(time5km);
            }
        }

        private Athlete GetAthlete(string name) => athletesManager.FindAthleteByName(name);

        private TimeSpan GetQuickestParkrunTime(string name, int year) => GetAthlete(name)?.GetQuickestParkrun(year)?.RaceTime ?? TimeSpan.Zero;

        private async Task UpdateAsync()
        {
            var record = this.GetSelectedRecord();
            if (record == null)
            {
                return;
            }

            // goto the parkrun website and get their latest parkrun data
            await this.athletesManager.PopulateWithParkrunListAsync(this.basePath, new List<string> { record.Name }, true);

            var fastestParkrun5km = this.ucTime5km.Time;
            if (fastestParkrun5km == TimeSpan.Zero)
            {
                try
                {
                    fastestParkrun5km = GetQuickestParkrunTime(record.Name, year);
                }
                catch
                {
                    MessageBox.Show($"Unable to get parkrun times for {record.Name} in {year}");
                    return;
                }
            }

            record.FiveKm = await AthletesManager.GetTimeAsync(RaceDistance.FiveKm, fastestParkrun5km, fastestParkrun5km);
            record.TenKm = await AthletesManager.GetTimeAsync(RaceDistance.TenKm, fastestParkrun5km, this.ucTime10km.Time);
            record.TenMiles = await AthletesManager.GetTimeAsync(RaceDistance.TenMiles, fastestParkrun5km, this.ucTime10m.Time);
            record.HalfMarathon = await AthletesManager.GetTimeAsync(RaceDistance.HalfMarathon, fastestParkrun5km, this.ucTimeHalfM.Time);
            UpdateSelectedRecordUI();
        }

        private void lstNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelectedRecordUI();
        }

        private void UpdateSelectedRecordUI()
        {
            var record = this.GetSelectedRecord();
            if (record == null)
            {
                return;
            }

            this.txtName.Text = record.Name;
            this.ucTime5km.Time = record.FiveKm?.GetTimeSpan() ?? TimeSpan.Zero;
            this.ucTime10km.Time = record.TenKm?.GetTimeSpan() ?? TimeSpan.Zero;
            this.ucTime10m.Time = record.TenMiles?.GetTimeSpan() ?? TimeSpan.Zero;
            this.ucTimeHalfM.Time = record.HalfMarathon?.GetTimeSpan() ?? TimeSpan.Zero;
        }

        private void btnResetAllTimes_Click(object sender, EventArgs e)
        {
            this.RecordsManager.ResetAllTimes();
            UpdateSelectedRecordUI();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var name = this.ucAthletesCombo1.SelectedAthleteName;
            var record = new Record(name);
            RecordsManager.Records.Add(record);
            RefreshAthleteList();
            var newItem = this.lstNames.Items.Cast<ListViewItem>().SingleOrDefault(item => item.Text == name);
            if (newItem != null)
            {
                newItem.Selected = true;
            }
        }

        private void btnDeleteAthlete_Click(object sender, EventArgs e)
        {
            var index = this.lstNames.SelectedIndices[0];
            var record = this.GetSelectedRecord();
            RecordsManager.Records.Remove(record);
            this.RefreshAthleteList();
            this.lstNames.Items[Math.Min(index, this.lstNames.Items.Count - 1)].Selected = true;
            this.lstNames.Select();
        }

        private void btnUpdate5kmPreviousYear_Click(object sender, EventArgs e)
        {
            UpdateAllPreviousYearsBestParkrun();
            UpdateSelectedRecordUI();
        }
    }
}
