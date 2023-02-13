using System;

namespace WordsInString
{
    public class Program
    {
        static void Main(string[] args)
        {
            /// <summary>
            /// Задание 1. Метод разделения строки на слова
            /// </summary>

            Console.Write("Введите предложение: ");
            string sentence1 = Console.ReadLine();
            PrintWords(GetWords(sentence1));

            Console.ReadKey();


            /// <summary>
            /// Задание 2. Перестановка слов в предложении
            /// </summary>

            Console.Write("\nВведите предложение: ");
            string sentence2 = Console.ReadLine();
            Console.Write(ReverseWords(sentence2));

            Console.ReadKey();

        }

        public static string[] GetWords(string sentence)
        {
            string[] words = sentence.Split(' ');

            return words;
        }

        public static void PrintWords(string[] words)
        {
            foreach (var word in words)
            {
                Console.WriteLine(word);
            }
        }

        public static string ReverseWords(string sentence)
        {
            string[] words = GetWords(sentence).Reverse().ToArray();
            string reverseSentence = "";

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Contains("."))
                {
                    words[i] = words[i].Remove(words[i].Length - 1);
                    words[i] = words[i].Insert(0, ".");
                }
                reverseSentence += $"{words[i]} ";
            }

            return reverseSentence;
        }
    }
}


