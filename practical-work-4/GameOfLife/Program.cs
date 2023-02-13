using System;
using System.Threading;

namespace GameOfLife
{
    class Program
    {
        static void Main()
        {
            GameOfLife gameOfLife = new GameOfLife(40, 90);

            while (true)
            {
                gameOfLife.DrawGrid();
                gameOfLife.GetNewGeneration();
                Thread.Sleep(200);
            }
        }

    }

    class GameOfLife
    {
        public int X { get; }
        public int Y { get; }
        public bool[,] grid { get; set; }

        public GameOfLife(int x, int y)
        {
            X = x;
            Y = y;
            grid = new bool[X, Y];
            GenerateGrid();
        }

        /// <summary>
        /// Заполнение поля бактериями.
        /// </summary>

        private void GenerateGrid()
        {
            Random r = new Random();
            int number;
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    number = r.Next(2);
                    grid[i, j] = ((number == 0) ? false : true);
                }
            }
        }

        /// <summary>
        /// Отображение поля в консоли.
        /// </summary>

        public void DrawGrid()
        {
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    Console.Write(grid[i, j] ? "x" : " ");
                    if (j == Y - 1) Console.WriteLine("\r");
                }
            }
            Console.CursorVisible = false;
        }

        /// <summary>
        /// Подсчет живых бактерий в соседних клетках.
        /// </summary>
        /// <param name="x">X-координата клетки.</param>
        /// <param name="y">Y-координата клетки.</param>
        /// <returns>Число живых клеток.</returns>

        private int GetLiveNeighbours(int x, int y)
        {
            int liveNeighbors = 0;

            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    if (!((i < 0 || j < 0) || (i >= X || j >= Y)))
                    {
                        if (grid[i, j] == true) liveNeighbors++;
                    }
                }
            }
            return liveNeighbors;
        }

        /// <summary>
        /// Изменение состояния бактерий в соответствии с правилами.
        /// </summary>

        public void GetNewGeneration()
        {
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    int numOfAliveNeighbors = GetLiveNeighbours(i, j);

                    if (grid[i, j] && (numOfAliveNeighbors == 1 || numOfAliveNeighbors > 4))
                    {
                        grid[i, j] = false;
                    }
                    else if (numOfAliveNeighbors > 1 || numOfAliveNeighbors < 3)
                    {
                        grid[i, j] = true;
                    }
                }
            }
        }
    }
}


