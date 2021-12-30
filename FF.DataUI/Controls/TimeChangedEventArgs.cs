namespace FF.DataUI.Controls
{
    public class TimeChangedEventArgs : EventArgs
    {
        public TimeChangedEventArgs(TimeSpan time) => Time = time;

        public TimeSpan Time { get; }
    }
}
