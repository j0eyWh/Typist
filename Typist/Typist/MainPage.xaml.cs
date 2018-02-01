using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Typist.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Typist
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //private string _text = "text! This is a super fancy, sample text! This text is super fancy and long This is a super fancy, sample text! This text is super fancy and long";
        private string _text = "text! This is";

        private readonly List<IndexedWord> _words;
        private readonly List<IndexedWord> _correctWords = new List<IndexedWord>();
        private readonly List<IndexedWord> _wrongWords = new List<IndexedWord>();

        private readonly List<Run> _runsDone = new List<Run>();
        private readonly List<Run> _runsCurrent = new List<Run>();
        private List<Run> _runsLeft = new List<Run>();

        private readonly Paragraph _paragraph;

        private int _wordIndex = 0;

        public MainPage()
        {
            this.InitializeComponent();

            int s = 0;
            _words = _text.Split(' ')
                .Select(x =>
                {
                    var i = new IndexedWord() { Index = s, Word = x };
                    s++;
                    return i;
                })
                .ToList();

            _paragraph = new Paragraph();

            GenerateInitialRuns();

            foreach (var run in _runsDone.Concat(_runsCurrent).Concat(_runsLeft))
            {
                _paragraph.Inlines.Add(run);
            }

            TextBlock.Blocks.Add(_paragraph);
        }

        private void GenerateInitialRuns()
        {
            _runsDone.Clear();

            _runsCurrent.Add(new Run()
            {
                Text = $"{_words.ElementAt(_wordIndex).Word} "
            });

            _runsLeft = _words.Skip(_wordIndex + 1).Select(x => new Run()
            {
                Text = $"{x.Word} "
            }).ToList();
        }

        private void HandleKeyUp(object sender, KeyRoutedEventArgs e)
        {
            var currentWord = _words.ElementAt(_wordIndex);

            var input = InputTextBox.Text;

            _runsCurrent.Clear();

            if (currentWord.Word.StartsWith(input))
            {
                string x;

                if (input.Length == 1)
                {
                    x = currentWord.Word.Split(new char[] { input.First() }, 2)
                        .LastOrDefault();
                }
                else
                {
                    x = currentWord.Word.Split(new[] { input }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                }

                _runsCurrent.Add(new Run()
                {
                    Text = input,
                    Foreground = new SolidColorBrush(Colors.ForestGreen)
                });
                if (x != null)
                {
                    _runsCurrent.Add(new Run()
                    {
                        Text = x,
                    });
                }
            }

            Redraw();
        }

        private void Redraw()
        {
            _paragraph.Inlines.Clear();

            foreach (var run in _runsDone.Concat(_runsCurrent.Concat(_runsLeft)))
            {
                _paragraph.Inlines.Add(run);
            }
        }

        private void RerenderParagraph(string input)
        {
            var runs = GenerateRuns(input);
            _paragraph.Inlines.Clear();

            foreach (var run in runs)
            {
                _paragraph.Inlines.Add(run);
            }
        }

        private string _nicePrerun = string.Empty;
        private List<Run> GenerateRuns(string input)
        {
            var runs = new List<Run>();

            foreach (var word in _words)
            {
                if (word.Index == _wordIndex && input != string.Empty)
                {
                    if (word.Word.StartsWith(input))
                    {
                        string x;

                        if (input.Length == 1)
                        {
                            x = word.Word.Split(new char[] { input.First() }, 2)
                                .LastOrDefault();
                        }
                        else
                        {
                            x = word.Word.Split(new[] { input }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                        }

                        var prerun = new Run()
                        {
                            Text = input,
                            Foreground = new SolidColorBrush(Colors.Green)
                        };

                        var postrun = new Run()
                        {
                            Text = $"{x} ",
                            Foreground = new SolidColorBrush(Colors.Black)
                        };

                        _nicePrerun = prerun.Text;
                        runs.Add(prerun);
                        runs.Add(postrun);

                        continue;
                    }
                    if (!string.IsNullOrEmpty(_nicePrerun))
                    {
                        var prerun = new Run()
                        {
                            Text = _nicePrerun,
                            Foreground = new SolidColorBrush(Colors.Green)
                        };
                        var postrun = new Run()
                        {
                            Text = $"{word.Word.Substring(_nicePrerun.Length)} ",
                            Foreground = new SolidColorBrush(Colors.Red)
                        };

                        runs.Add(prerun);
                        runs.Add(postrun);

                        continue;
                    }
                }
                var run = new Run()
                {
                    Text = $"{word} ",
                    Foreground = new SolidColorBrush(Colors.Black)
                };

                if (_wrongWords.Any(x => x.Index == word.Index))
                    run.Foreground = new SolidColorBrush(Colors.Red);

                if (_correctWords.Any(x => x.Index == word.Index))
                    run.Foreground = new SolidColorBrush(Colors.ForestGreen);

                runs.Add(run);
            }

            return runs;
        }
    }

}
