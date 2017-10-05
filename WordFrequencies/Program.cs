using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFrequencies
{
    public class Word
    {
        public string Value { get; set; }
        public int Count { get; set; }
        public List<int> Sentences { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string text = "Georgie Porgie was a cheeky little boy. " +
                          "He liked to tease people especially little girls. " +
                          "One afternoon, he went to the park near his house." +
                          "He found a little girl and tried to kiss her. " +
                          "The girl cried and sobbed because she did not like Georgie." +
                          "Then, some boys came to the park and saw Georgie chasing after the girl." +
                          "They shouted and laughed loudly at Georgie." +
                          "Georgie stopped chasing the girl and ran away feeling embarrassed. " +
                          "Thereafter Georgie hesitated to play with his friends because he remembered his embarrassment that he faced in front of his friends. " +
                          "This incident prohibited him from chasing girls thereafter.";

            var words = text.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            var wordsWithSentences = new List<KeyValuePair<int, string>>();

            // set sentences for words
            for (int i = 0; i < words.Length; i++)
            {
                var splitedWords = words[i].Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                wordsWithSentences.AddRange(splitedWords.Select(t => new KeyValuePair<int, string>(i + 1, t)));
            }

            var orderedWordsWithSentences = wordsWithSentences.OrderBy(c => c.Value).ToList();


            //set all words details( count, value, sentences)
            var resultWordsDetails = new List<Word>();

            foreach (KeyValuePair<int, string> wordAndSentance in orderedWordsWithSentences)
            {
                if (resultWordsDetails.FirstOrDefault(c => c.Value == wordAndSentance.Value) == null)
                {
                    resultWordsDetails.Add(new Word()
                    {
                        Value = wordAndSentance.Value,
                        Count = 1,
                        Sentences = new List<int>() { wordAndSentance.Key }
                    });
                }
                else
                {
                    var existsIn_resultWordsDetails = resultWordsDetails.FirstOrDefault(c => c.Value == wordAndSentance.Value);
                    existsIn_resultWordsDetails.Sentences.Add(wordAndSentance.Key);
                    existsIn_resultWordsDetails.Count++;
                }
            }


            //output result 
            var point = (char)96;
            int pointTimes = 1;
            for (int i = 0; i < resultWordsDetails.Count; i++)
            {
                point++;

                if (i % 26 == 0 && i != 0)
                {
                    pointTimes++;
                    point = (char)(Convert.ToInt32(point) - 26);
                }
                Console.WriteLine(
                    $"{PrintCharSomeTimes(point, pointTimes)}. {resultWordsDetails[i].Value} {{{resultWordsDetails[i].Count}:{PrintNumbersWithComma(resultWordsDetails[i].Sentences)}}}");
            }
            Console.ReadKey();
        }

        public static string PrintNumbersWithComma(List<int> list)
        {
            StringBuilder sb = new StringBuilder();
            var listEnumerator = list.GetEnumerator();
            for (var i = 0; i < list.Count; i++)
            {
                if (listEnumerator.MoveNext() && listEnumerator.Current != list[0])
                {
                    sb.Append(',' + listEnumerator.Current.ToString());
                }
                else
                {
                    sb.Append(listEnumerator.Current.ToString());
                }
            }
            return sb.ToString();

        }

        public static string PrintCharSomeTimes(char point, int times)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < times; i++)
                sb.Append(point);
            return sb.ToString();
        }
    }
}
