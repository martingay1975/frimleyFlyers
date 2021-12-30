using FF.DataEntry;
using FF.DataEntry.Api;
using FF.DataEntry.Dto;
using FF.DataEntry.Utils;

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
            foreach (var record in this.RecordsManager.Records.OrderBy(record => record.Name))
            {
                this.lstNames.Items.Add(record.Name);
            }
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
                fastestParkrun5km = athletesManager.GetQuickestParkrun(record.Name, startDate, endDate);
            }

            record.FiveKm = await AthletesManager.GetTimeAsync(RaceDistance.FiveKm, fastestParkrun5km, fastestParkrun5km);
            record.TenKm = await AthletesManager.GetTimeAsync(RaceDistance.TenKm, fastestParkrun5km, this.ucTime10km.Time);
            record.TenMiles = await AthletesManager.GetTimeAsync(RaceDistance.TenMiles, fastestParkrun5km, this.ucTime10m.Time);
            record.HalfMarathon = await AthletesManager.GetTimeAsync(RaceDistance.HalfMarathon, fastestParkrun5km, this.ucTimeHalfM.Time);
            UpdateUI();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var frmTextBox = new frmTextBox("Name");
            if (frmTextBox.ShowDialog() == DialogResult.OK)
            {
                var record = new Record(frmTextBox.Value);
                this.lstNames.Items.Add(frmTextBox.Value);
                RecordsManager.Records.Add(record);
            }
        }

        private void lstNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
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
            UpdateUI();
        }
    }
}
