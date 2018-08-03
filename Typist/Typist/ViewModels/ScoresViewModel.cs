using Typist.Models;

namespace Typist.ViewModels
{
	public class ScoresViewModel : ViewModelBase
	{
		private Score _score = new Score();
		public Score Score
		{
			get => _score;
			set
			{
				_score = value;
				OnPropertyChanged();
			}
		}
	}
}
