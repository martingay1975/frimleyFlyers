using FF.DataEntry.Api;
using FF.DataEntry.Dto;
using FF.DataEntry.Utils;
using System.Linq;

namespace FF.DataUI.Forms
{
    public partial class frmRecords : Form
    {
        private readonly AthletesManager athletesManager;
        private int year;
        public RecordsManager RecordsManager { get; }

        public frmRecords(RecordsManager recordsManager, AthletesManager athletesManager, int year)
        {
            InitializeComponent();
            this.RecordsManager = recordsManager;
            this.athletesManager = athletesManager ?? throw new ArgumentNullException(nameof(athletesManager));
            this.year = year;
            this.ucAthletesCombo1.Initialize(athletesManager);
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

        private async Task UpdateAsync()
        {
            var record = this.GetSelectedRecord();
            if (record == null)
            {
                return;
            }

            var fastestParkrun5km = this.ucTime5km.Time;
            if (fastestParkrun5km == TimeSpan.Zero)
            {
                var startDate = new DateTime(year - 1, 1, 1);
                var endDate = new DateTime(year - 1, 12, 31);
                try
                {
                    fastestParkrun5km = athletesManager.GetQuickestParkrun(record.Name, startDate, endDate);
                }
                catch
                {
                    MessageBox.Show($"Unable to get parkrun times for {record.Name} between {startDate} and {endDate}");
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
            this.ucTime5km.Time = record.FiveKm?.GetTime() ?? TimeSpan.Zero;
            this.ucTime10km.Time = record.TenKm?.GetTime() ?? TimeSpan.Zero;
            this.ucTime10m.Time = record.TenMiles?.GetTime() ?? TimeSpan.Zero;
            this.ucTimeHalfM.Time = record.HalfMarathon?.GetTime() ?? TimeSpan.Zero;
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
            this.lstNames.Items[Math.Min(index, this.lstNames.Items.Count)].Selected = true;
            this.lstNames.Select();
        }
    }
}
