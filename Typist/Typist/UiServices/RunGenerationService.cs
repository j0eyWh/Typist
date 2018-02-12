using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;

namespace Typist.UiServices
{
    public class RunGenerationService
    {
        public Run GenerateRun(string text, bool failed) => new Run()
        {
            Text = text,
            Foreground = new SolidColorBrush(failed ? Colors.Red : Colors.ForestGreen)
        };
    }

    public static class RunExtensions
    {
        public static Run SetState(this Run r, bool failed)
        {
            r.Foreground = new SolidColorBrush(failed ? Colors.Red : Colors.ForestGreen);
            return r;
        }
    }
}
