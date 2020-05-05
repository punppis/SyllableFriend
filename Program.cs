using System;
using System.Linq;

namespace SyllableFriend
{
    class Program
    {
        static void Main(string[] args)
        {
            var words = WordItem.LoadXml("kotus-sanalista_v1/kotus-sanalista_v1.xml");
            foreach(var word in words.OrderBy(o => Guid.NewGuid()).Take(100))
            {
                Console.WriteLine($"Word: {word.Word} ({word.SyllableString})");
            }
        }
    }
}
