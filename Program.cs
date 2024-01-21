using System.Xml.Linq;

namespace SyllableFriend
{
    class Program
    {
        static void Main(string[] args)
        {
            var words = XElement.Load("data/kotus-sanalista_v1/kotus-sanalista_v1.xml").Descendants("st").Select(word => new WordItem(word.Element("s").Value, FinnishSyllableTransformer.GetSyllables)).ToList();
            foreach(var word in words.OrderBy(o => Guid.NewGuid()).Take(100))
            {
                Console.WriteLine($"Word: {word.Word} ({word.SyllableString})");
            }
        }
    }
}
