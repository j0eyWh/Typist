namespace Typist.Models
{
    public class IndexedWord
    {
        public string Word { get; set; }

        public int Index { get; set; }

        public override string ToString() => Word;
    }
}
