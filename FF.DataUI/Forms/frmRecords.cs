using FF.DataEntry;
using FF.DataEntry.Dto;

namespace FF.DataUI.Forms
{
    public partial class frmRecords : Form
    {
        public frmRecords(List<Record> records)
        {
            InitializeComponent();
            Records = records;
            
            foreach (var record in records)
            {
                this.lstNames.Items.Add(record.Name);
            }
        }

        public List<Record> Records { get; }

        private Record GetSelectedRecord()
        {
            if (this.lstNames.SelectedItems.Count == 0)
            {
                return null;
            }

            var name = this.lstNames.SelectedItems[0].Text;
            return Records.Single(record => record.Name == name);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var record = this.GetSelectedRecord();
            if (record == null)
            {
                return;
            }

            record.FiveKm = UpdateRecord(this.ucTime5km.Time, this.ucTime5km.Time);
            record.TenKm = UpdateRecord(this.ucTime10km.Time, this.ucTime5km.Time);
            record.TenMiles = UpdateRecord(this.ucTime10m.Time, this.ucTime5km.Time);
            record.HalfMarathon = UpdateRecord(this.ucTimeHalfM.Time, this.ucTime5km.Time);
            UpdateUI();
        }

        private Time UpdateRecord(TimeSpan timeSpan, TimeSpan backup5kmTime)
        {
            if (timeSpan == TimeSpan.Zero)
            {
                timeSpan = RaceTimePredictor.GetPredictor(backup5kmTime);
            }

            var time = new Time();
            time.SetTime(timeSpan);
            return time;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var frmTextBox = new frmTextBox("Name");
            if (frmTextBox.ShowDialog() == DialogResult.OK)
            {
                var record = new Record(frmTextBox.Value);
                this.lstNames.Items.Add(frmTextBox.Value);
                Records.Add(record);
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
    }
}
