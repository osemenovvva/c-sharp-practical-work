namespace ListCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();
            AddRandomNumbers(numbers);
            PrintList(numbers);
            DeleteNumbers(numbers);
            PrintList(numbers);
        }

        /// <summary>
        /// Метод заполнения списка случайными числами
        /// </summary>
        /// <param name="numbers">Список чисел</param>
        public static void AddRandomNumbers(List<int> numbers)
        {
            var rnd = new Random();
            for (var i = 0; i < 100; i++)
            {
                numbers.Add(rnd.Next(0, 100));
            }
        }

        /// <summary>
        /// Метод для вывода элементов списка в консоль
        /// </summary>
        /// <param name="numbers">Список чисел</param>
        public static void PrintList(List<int> numbers)
        {
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }
            Console.WriteLine("\n");
            Console.ReadKey();
        }
        
        /// <summary>
        /// Метод для удаления чисел из списка по значению
        /// </summary>
        /// <param name="numbers">Список чисел</param>
        public static void DeleteNumbers(List<int> numbers)
        {
            numbers.RemoveAll(x => x > 25 && x < 50);
        }
        
    }
}

