using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Typist.Controls
{
	public sealed partial class TimerControl : UserControl
	{
		public event EventHandler TimesUp;

		private readonly DispatcherTimer _timer = new DispatcherTimer();

		private TimeSpan _timeSpan;

		public TimerControl()
		{
			this.InitializeComponent();

			_timer.Interval = TimeSpan.FromSeconds(1);

			_timer.Tick += HandleTick;

			RefreshLabel();
		}

		public void StartCountDown(int seconds)
		{
			_timeSpan = TimeSpan.FromSeconds(seconds);
			_timer.Start();
		}

		private void HandleTick(object sender, object o)
		{
			_timeSpan = _timeSpan - TimeSpan.FromSeconds(1);

			if (_timeSpan == TimeSpan.Zero)
			{
				_timer.Stop();
				OnTimesUp();
			}

			RefreshLabel();
		}

		private void RefreshLabel() => TimeBlock.Text = _timeSpan.ToString("mm\\:ss");

		private void OnTimesUp()
		{
			TimesUp?.Invoke(this, EventArgs.Empty);
		}
	}
}
