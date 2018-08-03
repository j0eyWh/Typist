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
using Typist.Models;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Typist.Controls
{
	public sealed partial class ScoresBoard : UserControl
	{
		public ScoresBoard()
		{
			this.InitializeComponent();
		}

		public void SetScore(Score score)
		{
			this.WpmTb.Text = score.WordsPerMinute.ToString();
			this.CorrectTb.Text = score.CorrectWords.ToString();
			this.WrongTb.Text = score.WrongWords.ToString();
		}
	}
}
