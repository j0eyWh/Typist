using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Typist.Controls
{
    public sealed partial class TimerControl : UserControl
    {
        private DispatcherTimer _timer = new DispatcherTimer();

        private TimeSpan _timeSpan = TimeSpan.FromMinutes(1);

        public TimerControl()
        {
            this.InitializeComponent();

            _timer.Interval = TimeSpan.FromSeconds(1);

            _timer.Tick += HandleTick;

            TimeBlock.Text = $"{_timeSpan.Minutes} : {_timeSpan.Seconds}";
        }

        public void Start() => _timer.Start();

        private void HandleTick(object sender, object o)
        {
            _timeSpan = _timeSpan - TimeSpan.FromSeconds(1);
        }
    }
}
