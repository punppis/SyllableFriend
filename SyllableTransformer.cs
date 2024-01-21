interface ISyllableTransformer
{
    static abstract public bool IsConsonant(char c);
    static abstract public bool IsVowel(char c);
    static abstract public bool IsDiftong(string str);
    static abstract List<string> GetSyllables(string word);
}
