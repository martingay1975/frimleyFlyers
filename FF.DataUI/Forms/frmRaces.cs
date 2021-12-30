using FF.DataEntry;
using FF.DataEntry.Api;
using FF.DataEntry.Dto;
using FF.DataUI.Controls;
using System.Data;

namespace FF.DataUI.Forms
{
    public partial class frmRaces : Form
    {
        private readonly Manager manager;

        public frmRaces(Manager manager)
        {
            InitializeComponent();
            lstAthlete.Columns[0].Width = lstAthlete.Width;
            lstRaces.Columns[0].Width = lstRaces.Width;
            lstEvents.Columns[0].Width = lstEvents.Width;
            this.manager = manager;
        }

        private void frmRaces_Load(object sender, EventArgs e)
        {
            PopulateRaces();
            PopulateAthletes();
            ucTime1.TimeChanged += UcTime1_TimeChanged;
        }

        private void UcTime1_TimeChanged(object? sender, EventArgs e)
        {
            var timeChangedEventArgs = (TimeChangedEventArgs)e;
            var selected = GetSelected();
            if (selected.IsValidResult)
            {
                if (selected.RacePersonTime == null)
                {
                    if (selected.RaceEvent.Results == null)
                    {
                        selected.RaceEvent.Results = new List<RacePersonTime>();
                    }

                    selected.RacePersonTime = new RacePersonTime() { Name = selected.SelectedAthleteName };
                    selected.RaceEvent.Results.Add(selected.RacePersonTime);
                }

                selected.RacePersonTime.Time.SetTime(timeChangedEventArgs.Time);
            }
        }

        private void PopulateRaces()
        {
            lstRaces.Items.Clear();
            foreach (var raceLabel in manager.RaceManager.RaceFinder.GetRaces())
            {
                lstRaces.Items.Add(raceLabel);
            }
        }

        private string? GetSeletedRaceLabel()
        {
            if (lstRaces.SelectedItems.Count != 1)
            {
                return null;
            }
            return lstRaces.SelectedItems[0].Text;
        }

        private DateTime? GetSelectedEventDate()
        {
            if (lstEvents.SelectedItems.Count != 1)
            {
                return null;
            }
            return (DateTime)lstEvents.SelectedItems[0].Tag;
        }

        private string? GetSelectedAthleteName()
        {
            if (lstAthlete.SelectedItems.Count != 1)
            {
                return null;
            }
            return lstAthlete.SelectedItems[0].Text;
        }


        private void PopulateEvents(string raceLabel)
        {
            lstEvents.Items.Clear();
            foreach (var raceEventDate in manager.RaceManager.RaceFinder.GetEvents(raceLabel))
            {
                var listViewItem = new ListViewItem(raceEventDate.ToString("d MMM"));
                listViewItem.Tag = raceEventDate;
                lstEvents.Items.Add(listViewItem);
            }
        }

        private void PopulateAthletes()
        {
            lstAthlete.Items.Clear();
            foreach (var athlete in manager.AthletesManager.Athletes.OrderBy(athlete => athlete.Name))
            {
                lstAthlete.Items.Add(athlete.Name);
            }
        }

        private void lstRaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            var raceLabel = GetSeletedRaceLabel();
            if (raceLabel != null)
            {
                PopulateEvents(raceLabel);
                lstEvents.Items[0].Selected = true;
            }
        }

        private void lstAthlete_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = GetSelected();

            if (selected.RacePersonTime != null)
            {
                ucTime1.Time = selected.RacePersonTime.Time.GetTime();
            }
            else
            {
                ucTime1.Time = TimeSpan.Zero;
            }

            ucTime1.SetFocus();
        }

        private GetSelectedResults GetSelected()
        {
            var getSelectedResults = new GetSelectedResults();

            getSelectedResults.SelectedRaceLabel = GetSeletedRaceLabel();
            if (getSelectedResults.SelectedRaceLabel == null)
            {
                return getSelectedResults;
            }

            getSelectedResults.SelectedEventDate = GetSelectedEventDate();
            if (getSelectedResults.SelectedEventDate == null)
            {
                return getSelectedResults;
            }

            getSelectedResults.SelectedAthleteName = GetSelectedAthleteName();
            if (getSelectedResults.SelectedAthleteName == null)
            {
                return getSelectedResults;
            }

            getSelectedResults.RaceEvent = manager.RaceManager.RaceFinder.FindEvent(
                getSelectedResults.SelectedRaceLabel, getSelectedResults.SelectedEventDate.Value);

            getSelectedResults.RacePersonTime = getSelectedResults.RaceEvent.Results
                ?.Where(result => result.Name == getSelectedResults.SelectedAthleteName)
                .FirstOrDefault();

            return getSelectedResults;
        }

        private class GetSelectedResults
        {
            public bool IsValidResult => RaceEvent != null;

            public string? SelectedRaceLabel { get; set; }
            public DateTime? SelectedEventDate { get; set; }
            public string? SelectedAthleteName { get; set; }
            public RaceEvent RaceEvent { get; set; }
            public RacePersonTime RacePersonTime { get; set; }
        }
    }
}
