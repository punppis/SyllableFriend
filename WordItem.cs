public class WordItem
{
    public string Word { get; set; }
    public List<string> Syllables { get; set; }
    public string SyllableString => Syllables.Aggregate((a, b) => a + "-" + b);

    public WordItem(string word, Func<string, List<string>> getSyllables)
    {
        Word = word;
        Syllables = getSyllables(word);
    }
}