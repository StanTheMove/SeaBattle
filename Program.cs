using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle_Copy_paste____
{
    internal class Program
    {
        static int[,] playerBoard = new int[10, 10];
        static int[,] computerBoard = new int[10, 10];
        static void PlaceShips(int[,] board)
        {
            Random random = new Random();
            int numShips = 3;

            for (int i = 0; i < numShips; i++)
            {
                int x = random.Next(0, 10);
                int y = random.Next(0, 10);

                // Перевірте, чи є корабель в цій позиції.
                if (board[x, y] == 1)
                {
                    // Якщо так, повторіть спробу.
                    i--;
                }
                else
                {
                    // Якщо ні, розмістіть корабель.
                    board[x, y] = 1;
                }
            }
        }
        static void Main(string[] args)
        {
        }
    }
}
