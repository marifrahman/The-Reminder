using System;
using GalaSoft.MvvmLight;
using System.Windows.Threading;
using System.Runtime.InteropServices;


namespace The_Reminder.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        string _currentTime = DateTime.Now.ToLongTimeString();
        public string CurrentTime
        {
            get
            {
                return _currentTime;
            }
            set
            {
                _currentTime = value;
                RaisePropertyChanged("CurrentTime");
            }
        }

        DispatcherTimer timer = new DispatcherTimer();
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            //this.Activate();//This is to Focus on the windows
            App.Current.MainWindow.Activate();
        }
        int count = 0;
        void timer_Tick(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToLongTimeString();

            if (count >= 20)
            {
                timer.Stop();
                LockWorkStation();
                //this.Close();
                App.Current.MainWindow.Close();
            }
            else
            {
                count++;
            }
        }


        private const int WmSyscommand = 0x0112;
        private const int ScMonitorpower = 0xF170;
        private const int HwndBroadcast = 0xFFFF;
        private const int ShutOffDisplay = 2;
        [DllImport("user32.dll")]
        private static extern void LockWorkStation();
    }
}