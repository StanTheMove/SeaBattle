using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle_Copy_paste____
{
    internal class Program
    {
        static void PlaceShips(int[,] board)
        {
            Random random = new Random();
            int numShips = 3;

            for (int i = 0; i < numShips; i++)
            {
                int x = random.Next(0, 10);
                int y = random.Next(0, 10);
                if (board[x, y] == 1)
                {
                    i--;
                }
                else
                {
                    board[x, y] = 1;
                }
            }
        }
        static void DrawBoard(int[,] board)
        {
            Console.Write("  ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();

            for (int i = 0; i < 10; i++)
            {
                Console.Write(i + " ");
                for (int j = 0; j < 10; j++)
                {
                    if (board[i, j] == 0)
                    {
                        Console.Write(". ");
                    }
                    else if (board[i, j] == 1)
                    {
                        Console.Write("S ");
                    }
                    else if (board[i, j] == 2)
                    {
                        Console.Write("X ");
                    }
                    else if (board[i, j] == 3)
                    {
                        Console.Write("O ");
                    }
                }
                Console.WriteLine();
            }
        }
        static void PlayerTurn(int[,] computerBoard)
        {
            Console.WriteLine("Enter coordinates to attack (for example A5):");
            int x, y;
            while (true)
            {
                string input = Console.ReadLine();
                x = (int)input[0] - 65;
                y = int.Parse(input.Substring(1)) - 1;

                if (x < 0 || x > 9 || y < 0 || y > 9)
                {
                    Console.WriteLine("Invalid input. Please enter coordinates within the range of A1 to J10:");
                }
                else if (computerBoard[x, y] == 2 || computerBoard[x, y] == 3)
                {
                    Console.WriteLine("You have already attacked that position. Please choose a different position:");
                }
                else
                {
                    break;
                }
            }
            if (computerBoard[x, y] == 1)
            {
                Console.WriteLine("Hit!");
                computerBoard[x, y] = 2;
            }
            else
            {
                Console.WriteLine("Miss!");
                computerBoard[x, y] = 3;
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
        static void ComputerTurn(int[,] playerBoard)
        {
            Console.WriteLine("Computer's turn:");

            // Згенеруйте випадкові координати для атаки.
            Random random = new Random();
            int x, y;
            do
            {
                x = random.Next(10);
                y = random.Next(10);
            } while (playerBoard[x, y] == 2 || playerBoard[x, y] == 3);

            // Обробіть атаку на вказаних координатах.
            if (playerBoard[x, y] == 1)
            {
                Console.WriteLine($"Computer hit {Convert.ToChar(x + 65)}{y + 1}!");
                playerBoard[x, y] = 2;
            }
            else
            {
                Console.WriteLine($"Computer missed {Convert.ToChar(x + 65)}{y + 1}.");
                playerBoard[x, y] = 3;
            }

            // Зачекайте на введення користувачем будь-якої клавіші, щоб продовжити гру.
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
        static bool IsWinner(int[,] board)
        {
            // Перевірте, чи всі кораблі потоплені.
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if (board[x, y] > 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        static void Main(string[] args)
        {
            int[,] playerBoard = new int[10, 10];
            int[,] computerBoard = new int[10, 10];
            PlaceShips(playerBoard);
            PlaceShips(computerBoard);

            while (true)
            {
                // Виведіть поле бою.
                Console.Clear();
                Console.WriteLine("Your Board:");
                DrawBoard(playerBoard);
                Console.WriteLine("Computer's Board:");
                DrawBoard(computerBoard);

                // Обробіть хід користувача.
                PlayerTurn(computerBoard);

                // Перевірте, чи є переможець.
                if (IsWinner(playerBoard))
                {
                    Console.WriteLine("Congratulations, you won!");
                    break;
                }

                // Обробіть хід комп'ютера.
                ComputerTurn(playerBoard);

                // Перевірте, чи є переможець.
                if (IsWinner(computerBoard))
                {
                    Console.WriteLine("Sorry, you lost.");
                    break;
                }
            }

            // Завершіть гру.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
