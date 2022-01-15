using FF.DataEntry.Dto;
using FF.DataEntry.Utils;

namespace FF.DataUI.Controls
{
    public partial class ucAthletesCombo : UserControl
    {
        private AthletesManager? athletesManager;

        public ucAthletesCombo()
        {
            InitializeComponent();
        }

        public void Initialize(AthletesManager athletesManager)
        {
            this.athletesManager = athletesManager;
        }

        public void RefreshControl(List<string> dontInclude)
        {
            if (dontInclude == null)
            {
                throw new ArgumentException(nameof(dontInclude));
            }

            this.cboAthletes.Items.Clear();
            var names = athletesManager?.Athletes
                .Select(athlete => athlete.Name)
                .Except(dontInclude)?
                .OrderBy(name => name)
                .ToArray();

            this.cboAthletes.Items.AddRange(names);
        }

        public string SelectedAthleteName => this.cboAthletes.SelectedItem.ToString();
    }
}
