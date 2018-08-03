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
		private readonly Random _random;
		private List<string> _words;

		public WordsLoader()
		{
			_random = new Random();
		}

		/// <summary>
		/// Loads a random batch of words
		/// </summary>
		/// <param name="size">Size of the batch to be returned</param>
		/// <returns></returns>
		public async Task<IEnumerable<IndexedWord>> LoadRandomBatch(int size)
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
