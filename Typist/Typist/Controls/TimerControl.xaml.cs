using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Typist.Controls
{
	public sealed partial class TimerControl : UserControl
	{
		private readonly DispatcherTimer _timer = new DispatcherTimer();

		private TimeSpan _timeSpan = TimeSpan.FromSeconds(59);

		public TimerControl()
		{
			this.InitializeComponent();

			_timer.Interval = TimeSpan.FromSeconds(1);

			_timer.Tick += HandleTick;

			RefreshLabel();
		}

		public void Start() => _timer.Start();

		private void HandleTick(object sender, object o)
		{
			_timeSpan = _timeSpan - TimeSpan.FromSeconds(1);
			RefreshLabel();
		}

		private void RefreshLabel() => TimeBlock.Text = _timeSpan.ToString("mm\\:ss");
	}
}
