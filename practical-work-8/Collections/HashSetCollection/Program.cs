namespace HashSetCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<int> numbers = new HashSet<int>();
            while (true)
            {
                Console.Write("Введите число: ");
                int number = int.Parse(Console.ReadLine());
                if (AddNumber(numbers, number))
                {
                    Console.WriteLine("Число успешно добавлено в коллекцию\n");
                }
                else
                {
                    Console.WriteLine("Число уже есть в коллекции\n");
                }
            }
            
        }

        public static bool AddNumber(HashSet<int> numbers, int number)
        {
            return numbers.Add(number);
        }
    }
}