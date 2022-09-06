namespace WordsInString
{
    class Program
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
            ReverseWords(sentence2);

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

        public static void ReverseWords(string sentence)
        {
            string[] reverseSentence = GetWords(sentence).Reverse().ToArray();

            for (int i = 0; i < reverseSentence.Length; i++)
            {
                if (reverseSentence[i].Contains("."))
                {
                    reverseSentence[i] = reverseSentence[i].Remove(reverseSentence[i].Length - 1);
                    reverseSentence[i] = reverseSentence[i].Insert(0, ".");
                }

                Console.Write($"{reverseSentence[i]} ");
            }

        }
    }
}

