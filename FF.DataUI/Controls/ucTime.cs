namespace FF.DataUI.Controls
{
    public partial class ucTime : UserControl
    {
        public ucTime()
        {
            InitializeComponent();
        }

        public TimeSpan Time
        {
            get
            {
                try
                {
                    return new TimeSpan(
                        Convert.ToInt32(txtHours.Text),
                        Convert.ToInt32(txtMinutes.Text),
                        Convert.ToInt32(txtSeconds.Text));
                }
                catch 
                {
                    return TimeSpan.Zero;
                }
            }
            set
            {
                txtHours.Text = value.Hours.ToString();
                txtMinutes.Text = value.Minutes.ToString();
                txtSeconds.Text = value.Seconds.ToString();
            }
        }
    }
}
