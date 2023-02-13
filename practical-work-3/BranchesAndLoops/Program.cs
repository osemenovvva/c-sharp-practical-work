using System;

namespace BranchesAndLoops
{
    class Program
    {
        static void Main(string[] args)
        {
            /// <summary>
            /// Задание 1. Приложение по определению чётного или нечётного числа
            /// </summary>

            Console.Write("Введите число: ");
            int number = int.Parse(Console.ReadLine());

            if (number % 2 == 0)
            {
                Console.WriteLine($"Число {number} - чётное.\n");
            }
            else
            {
                Console.WriteLine($"Число {number} - нечётное.\n");
            }

            Console.ReadKey();


            /// <summary>
            /// Задание 2. Программа подсчёта суммы карт в игре «21»
            /// </summary>

            Console.Write("Добро пожаловать в игру \"21\"! \nУкажите количество карт на руках: ");
            int cardsCount = int.Parse(Console.ReadLine());
            int cardsSum = 0;

            for (int i = 0; i < cardsCount; i++)
            {
                Console.Write($"Введите номинал {i + 1} карты: ");
                string cardNumber = Console.ReadLine().ToUpper();

                if (cardNumber == "J" || cardNumber == "Q" || cardNumber == "K" || cardNumber == "T")
                {
                    cardsSum += 10;
                }
                else if (int.TryParse(cardNumber, out int cardNumberValue) && cardNumberValue > 0 && cardNumberValue < 11)
                {
                    cardsSum += cardNumberValue;
                }
                else
                {
                    Console.WriteLine("Введено некорректное значение карты!");
                    i--;
                }
            }

            Console.WriteLine($"Сумма карт пользователя: {cardsSum}");

            Console.ReadLine();

            /// <summary>
            /// Задание 3. Проверка простого числа
            /// </summary>

            Console.Write("Введите число: ");
            int n = int.Parse(Console.ReadLine());
            bool isPrime = true;

            while (isPrime)
            {
                for (int i = 2; i <= n - 1; i++)
                {
                    if (n % i == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                break;
            }

            Console.WriteLine(isPrime ? "Число является простым." : "Не является простым числом.");

            Console.ReadLine();

            /// <summary>
            /// Задание 4. Наименьший элемент в последовательности
            /// </summary>

            Console.Write("Введите количество чисел в последовательности: ");
            int countElements = int.Parse(Console.ReadLine());
            int min = int.MaxValue;

            Console.WriteLine("Введите последовательность чисел: ");

            while (countElements > 0)
            {
                int element = int.Parse(Console.ReadLine());
                min = min > element ? element : min;
                countElements--;
            }

            Console.WriteLine($"Минимальное число в последовательности: {min}");

            Console.ReadLine();

            /// <summary>
            /// Задание 5. Игра «Угадай число»
            /// </summary>

            Console.Write("Введите максимальное число диапазона: ");

            Random random = new Random();
            int value = random.Next(0, int.Parse(Console.ReadLine()) + 1);

            while (true)
            {
                Console.Write("Введите загаданное число: ");
                string userInput = Console.ReadLine();

                if (userInput == "")
                {
                    Console.WriteLine($"Спасибо за игру! Загаданное число: {value}");
                    break;
                }

                int userValue = int.Parse(userInput);

                if (userValue == value)
                {
                    Console.WriteLine($"Поздравляем! Вы отгадали число {value}!");
                    break;
                }
                else if (userValue > value)
                {
                    Console.WriteLine($"Загаданное число меньше.");
                }
                else
                {
                    Console.WriteLine($"Загаданное число больше.");
                }
            }

            Console.ReadLine();
        }
    }
}

