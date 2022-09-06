namespace Arrays
{
    class Program
    {
        static void Main(string[] args)
        {
            /// <summary>
            /// Задание 1. Случайная матрица
            /// </summary>
            Console.Write("Введите количество строк матрицы: ");
            int n = int.Parse(Console.ReadLine());

            Console.Write("Введите количество столбцов матрицы: ");
            int m = int.Parse(Console.ReadLine());

            int[,] matrixA = new int[n, m];
            int sum = 0;

            Random r = new Random();

            Console.WriteLine("Матрица А: ");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matrixA[i, j] = r.Next(-100,100);
                    sum += matrixA[i, j];
                    Console.Write($"{matrixA[i, j]} ");
                }
                Console.WriteLine();
            }

            Console.WriteLine($"Сумма всех элементов матрицы A: {sum}\n");

            /// <summary>
            /// Задание 2. Сложение матриц
            /// </summary>
            int[,] matrixB = new int[n, m];

            Console.WriteLine("Матрица B: ");

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matrixB[i, j] = r.Next(-100, 100);
                    Console.Write($"{matrixB[i, j]} ");
                }
                Console.WriteLine();
            }

            int[,] matrixC = new int[n, m];

            Console.WriteLine("Сумма матриц A и B: ");

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matrixC[i, j] = matrixA[i, j] + matrixB[i, j];
                    Console.Write($"{matrixC[i, j]} ");
                }
                Console.WriteLine();
            }

        }
    }
}

