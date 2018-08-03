using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Typist.Models
{
	public class Score
	{
		public int WordsPerMinute { get; set; }

		public int CorrectWords { get; set; }

		public int WrongWords { get; set; }
	}
}
