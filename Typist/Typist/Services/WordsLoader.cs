using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Typist.Models;

namespace Typist.Services
{
	public class WordsLoader
	{
		private List<string> _words;
		private readonly Random _random;

		public WordsLoader()
		{
			_random = new Random();
		}

		public async Task<IEnumerable<IndexedWord>> LoadBatch(int size)
		{
			await LoadWords();

			return Enumerable.Range(0, size)
				.Select(i => new IndexedWord()
				{
					Index = i,
					Word = GetRandomWord()
				});
		}

		private string GetRandomWord()
		{
			var randomIndex = _random.Next(_words.Count);

			return _words.ElementAt(randomIndex);
		}

		private async Task LoadWords()
		{
			var json = File.ReadAllText("Mocks\\Words.json");
			_words = await Task.Run(() => JsonConvert.DeserializeObject<List<string>>(json));
		}
	}
}
