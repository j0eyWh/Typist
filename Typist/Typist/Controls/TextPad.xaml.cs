﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Typist.Models;
using Typist.Services;
using Typist.UiServices;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Typist.Controls
{
	public sealed partial class TextPad : UserControl
	{
		private const int BatchSize = 20;

		private readonly WordsLoader _wordsLoader;

		private readonly List<IndexedWord> _words = new List<IndexedWord>();
		private readonly List<IndexedWord> _correctWords = new List<IndexedWord>();
		private readonly List<IndexedWord> _wrongWords = new List<IndexedWord>();

		private readonly List<Run> _runsDone = new List<Run>();
		private readonly RunsGenerator _runGenerationService;
		private readonly Paragraph _paragraph;

		private string _lastGoodInput = null;

		private List<Run> RunsLeft => _words.Skip(_wordIndex + 1)
			.Select(x => new Run()
			{
				Text = x.Word
			})
			.ToList();


		private int _wordIndex;
		private bool _isTyping = false;

		public event EventHandler TypingStarted;

		public TextPad()
		{
			InitializeComponent();
			_wordsLoader = App.DepedencyResolver.Get<WordsLoader>();
			_runGenerationService = new RunsGenerator();
			_paragraph = new Paragraph();

			this.Loaded += HandleLoaded;
		}

		public void FinishTypingSession()
		{
			_isTyping = false;
			_paragraph.Inlines.Clear();
			InputTextBox.Text = string.Empty;
			InputTextBox.IsEnabled = false;
		}

		public async Task PrepareTypingSession()
		{
			InputTextBox.IsEnabled = true;
			InputTextBox.Focus(FocusState.Pointer);
			await LoadNewBatch();
			await Redraw();
		}

		private async void HandleLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			await PrepareTypingSession();
			TextBlock.Blocks.Add(_paragraph);
		}

		private async void HandleKeyUp(object sender, KeyRoutedEventArgs e)
		{
			if (!_isTyping)
			{
				_isTyping = true;
				OnTypingStarted();
			}

			var input = InputTextBox.Text;

			if (e.Key == VirtualKey.Space && string.IsNullOrEmpty(input))
			{
				InputTextBox.Text = string.Empty;
				return;
			}

			var currentWord = _words.ElementAt(_wordIndex);

			if (e.Key == VirtualKey.Space)
			{
				_lastGoodInput = null;
				++_wordIndex;

				InputTextBox.Text = string.Empty;

				var run = _runGenerationService.GenerateRun(currentWord.Word, true);

				if (input.Trim() == currentWord.Word)
				{
					_correctWords.Add(currentWord);
					run.Foreground = new SolidColorBrush(Colors.ForestGreen);
				}
				else
				{
					_wrongWords.Add(currentWord);
				}
				_runsDone.Add(run);
			}

			await Redraw();
		}


		private async Task LoadNewBatch()
		{
			_words.Clear();
			_wordIndex = 0;
			_words.AddRange(await _wordsLoader.LoadRandomBatch(BatchSize));
			_runsDone.Clear();
			_paragraph.Inlines.Clear();
		}

		private async Task<List<Run>> GetCurrentRuns()
		{
			if (_wordIndex == _words.Count)
			{
				await LoadNewBatch();
			}

			var currentWord = _words.ElementAt(_wordIndex);
			var input = InputTextBox.Text;

			var l = new List<Run>();

			if (currentWord.Word.StartsWith(input))
			{
				string x;

				_lastGoodInput = input;

				if (input.Length == 1)
				{
					x = currentWord.Word.Split(new[] { input.First() }, 2)
						.LastOrDefault();
				}
				else
				{
					x = currentWord.Word.Split(new[] { input }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
				}

				l.Add(new Run()
				{
					Text = input,
					Foreground = new SolidColorBrush(Colors.ForestGreen),
				});
				if (x != null)
				{
					l.Add(new Run()
					{
						Text = x,
					});
				}
			}

			else
			{
				if (_lastGoodInput != null)
				{
					l.Add(new Run()
					{
						Text = _lastGoodInput,
						Foreground = new SolidColorBrush(Colors.ForestGreen),
					});
					l.Add(new Run()
					{
						Text = string.Join(string.Empty, currentWord.Word.Skip(_lastGoodInput.Length)),
						Foreground = new SolidColorBrush(Colors.Red),
					});

				}
				else
				{
					l.Add(new Run()
					{
						Text = currentWord.Word,
						Foreground = new SolidColorBrush(Colors.Red)
					});
				}
			}

			return l;
		}

		private async Task Redraw()
		{
			_paragraph.Inlines.Clear();

			foreach (var run in _runsDone)
			{
				_paragraph.Inlines.Add(run);
				_paragraph.Inlines.Add(new Run() { Text = " " });
			}
			foreach (var run in await GetCurrentRuns())
			{
				_paragraph.Inlines.Add(run);
			}

			foreach (var run in RunsLeft)
			{
				_paragraph.Inlines.Add(new Run() { Text = " " });
				_paragraph.Inlines.Add(run);
			}
		}

		private void OnTypingStarted()
		{
			TypingStarted?.Invoke(this, EventArgs.Empty);
		}

	}
}
