using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Typist.Controls
{
	public sealed partial class TypingPad : UserControl
	{
		private const int TypingTime = 5;
		public TypingPad()
		{
			this.InitializeComponent();
			this.TimerControl.ResetTimer(TypingTime);
			this.TextPad.TypingStarted += (sender, args) => TimerControl.StartCountDown(TypingTime);

			this.TimerControl.TimesUp += TimerControlOnTimesUp;
		}



		private void TimerControlOnTimesUp(object o, EventArgs eventArgs)
		{
			this.TextPad.FinishTypingSession();
		}

		private async void HandleResetClick(object sender, RoutedEventArgs e)
		{
			this.TimerControl.ResetTimer(TypingTime);
			await this.TextPad.PrepareTypingSession();
		}
	}
}
