using System.Linq;
using Typist.Services;
using Xunit;

namespace Typist.Test
{

	public class WordsLoaderTest
	{
		private readonly WordsLoader _loader;

		public WordsLoaderTest()
		{
			_loader = new WordsLoader();
		}

		[Fact]
		public async void LoadBatchTest()
		{
			var firstBatch = (await _loader.LoadRandomBatch(2)).ToList();
			var secondBatch = (await _loader.LoadRandomBatch(2)).ToList();

			Assert.NotEqual(firstBatch[0].Word, secondBatch[0].Word);
			Assert.NotEqual(firstBatch[1].Word, secondBatch[1].Word);
		}
	}
}