namespace FF.DataUI.Controls
{

    public partial class ucTime : UserControl
    {
        public event EventHandler TimeChanged;

        private void RaiseTimeChangedEvent(TimeSpan timeSpan)
        {
            TimeChanged?.Invoke(this, new TimeChangedEventArgs(timeSpan));
        }

        public ucTime()
        {
            InitializeComponent();
        }

        public void SetFocus()
        {
            this.txtHours.Focus();
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

        private void txtSeconds_TextChanged(object sender, EventArgs e)
        {
            RaiseTimeChangedEvent(Time);
        }

        private void txtMinutes_TextChanged(object sender, EventArgs e)
        {
            RaiseTimeChangedEvent(Time);
        }

        private void txtHours_TextChanged(object sender, EventArgs e)
        {
            RaiseTimeChangedEvent(Time);
        }
    }
}
