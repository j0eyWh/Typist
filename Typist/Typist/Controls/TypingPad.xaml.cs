using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Typist.Controls
{
	public sealed partial class TypingPad : UserControl
	{
		public TypingPad()
		{
			this.InitializeComponent();
			this.TextPad.TypingStarted += (sender, args) => TimerControl.StartCountDown(59);
		}
	}
}
