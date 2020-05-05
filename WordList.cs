using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

public class WordItem
{
    static public readonly string Alphabet = "abcdefghijklmnopqrstuywvxyzöä";
    static public readonly string Vowels = "aeiouyäö";
    static public readonly string Consonants = string.Join("", Alphabet.Except(Vowels));
    static public readonly string[] Diftongs = new string[]
    {
        "ai", "ei", "oi", "äi", "öi", "ey", "äy", "öy", "au",
        "eu", "ou", "ui", "yi", "iu", "iy", "ie", "uo", "yö"
    };
    static public bool IsConsonant(char c) => Consonants.Contains(c);
    static public bool IsVowel(char c) => Vowels.Contains(c);
    static public bool IsDiftong(string str) => Diftongs.Contains(str);

    public string Word { get; set; }
    public List<string> Syllables { get; set; }
    public string SyllableString => Syllables.Aggregate((a, b) => a + "-" + b);

    public WordItem(string word)
    {
        Word = word;
        Syllables = GetSyllables(word);
    }

    static public List<string> GetSyllables(string word)
    {
        List<string> syllables = new List<string>();
        int start = 0;
        for (int i = 1; i < word.Length - 1; i++)
        {
            char? prev = i > 0 ? (char?)word[i - 1] : null;
            char a = word[i];
            char b = word[i + 1];
            char? next = i < word.Length - 2 ? (char?)word[i + 2] : null;

            if(IsConsonant(a) && IsVowel(b))
            {
                syllables.Add(word.Substring(start, i - start));
                start = i;
            }
            else if(IsVowel(a) && IsVowel(b))
            {
                bool DiftongA = prev.HasValue && IsDiftong(prev.Value.ToString() + a.ToString());
                bool DiftongB = next.HasValue && IsDiftong(b.ToString() + next.Value.ToString());
                if (!DiftongA && !DiftongB)
                {
                    //todo: find next!!!!??? instead of start + 1
                    syllables.Add(word.Substring(start, i - start + 1).ToUpper());
                    start = ++i;
                }
            }
        }
        var last = syllables.LastOrDefault();

        if (last == null || word != syllables.Join("").ToLower())
        {
            syllables.Add("|" + word.Substring(start));
        }
        return syllables;
    }

    static public List<WordItem> LoadXml(string filepath)
    {
        return XElement.Load(filepath).Descendants("st").Select(word => new WordItem(word.Element("s").Value)).ToList();
    }
}