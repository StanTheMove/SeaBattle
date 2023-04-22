using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle_Copy_paste____
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to SeaBattle Shit!");

            while (true)
            {
                int mode = Menu();

                switch (mode)
                {
                    case 1:
                        PlayHumanVsHuman();
                        break;
                    case 2:
                        PlayComputerVsHuman();
                        {

                        }
                        break;
                    case 3:
                        PlayComputerVsComputer();
                        break;
                    case 0:
                        Console.WriteLine("Game Over.");
                        return;
                    default:
                        Console.WriteLine("Choose game mode .");
                        break;
                }
            }
        }
        static int Menu()
        {
            Console.WriteLine("\nWhat mode you choose:");
            Console.WriteLine("1 - Human vs Human");
            Console.WriteLine("2 - Human vs AI");
            Console.WriteLine("3 - AI vs AI");
            Console.WriteLine("0 - Exit");
            Console.Write("Your choice: ");
            int mode = int.Parse(Console.ReadLine());
            return mode;
        }
        static void PlayHumanVsHuman()
        {
            Console.WriteLine("\n Game Human vs Human:");
        }

        static void PlayComputerVsHuman(int boardSize, int[,] ships)
        {
            Console.WriteLine("\n Game Human vs AI:");
            int[,] playerBoard = new int[boardSize, boardSize];
            int[,] computerBoard = new int[boardSize, boardSize];

            DrawBoard(playerBoard);
            Console.WriteLine("Player, please place your ships.");
            for (int i = 0; i < ships.GetLength(0); i++)
            {
                int shipSize = ships[i, 0];
                bool isVertical = ships[i, 1] == 1;
                int row, col;
                bool isValid;

                do
                {
                    Console.Write($"Place your ship of size {shipSize} ({(isVertical ? "vertical" : "horizontal")}, top-left corner): ");
                    string input = Console.ReadLine();

                    col = input[0] - 'A';
                    row = int.Parse(input.Substring(1)) - 1;

                    isValid = PlaceShips(playerBoard, shipSize, isVertical, row, col);

                    if (!isValid)
                    {
                        Console.WriteLine("Invalid ship placement.");
                    }
                } while (!isValid);
            }
            Console.Clear();
            DrawBoard(playerBoard);
            Console.WriteLine("Player has placed all the ships.");
            Console.WriteLine("Computer is placing its ships...");
            for (int i = 0; i < ships.GetLength(0); i++)
            {
                int shipSize = ships[i, 0];
                bool isVertical = ships[i, 1] == 1;
                int row, col;
                bool isValid;
                do
                {
                    int[] target = GetRandomTarget(computerBoard);
                    col = target[0];
                    row = target[1];

                    isValid = PlaceShips(computerBoard, shipSize, isVertical, row, col);
                } while (!isValid);
            }

            Console.Clear();
            DrawBoard(playerBoard);
            while (true)
            {
                Console.WriteLine("\nPlayer's turn.");
                int[] target;

                do
                {
                    Console.Write("Enter target coordinates (e.g. A1): ");
                    string input = Console.ReadLine();

                    int col = input[0] - 'A';
                    int row = int.Parse(input.Substring(1)) - 1;

                    if (computerBoard[row, col] != 0)
                    {
                        Console.WriteLine("You've already");
                    }
                    else
                    {
                        target = new int[] { row, col };
                        break;
                    }
                } while (true);
                if (computerBoard[target[0], target[1]] == 1)
                {
                    Console.WriteLine("You hit a ship!");
                    computerBoard[target[0], target[1]] = -2;
                    if (IsGameOver(computerBoard))
                    {
                        Console.WriteLine("You won!");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("You missed.");
                    computerBoard[target[0], target[1]] = -1;
                }
                Console.WriteLine("\nComputer's turn.");
                int[] computerTarget = GenerateComputerTarget(playerBoard);
                if (playerBoard[computerTarget[0], computerTarget[1]] == 1)
                {
                    Console.WriteLine("The computer hit your ship!");
                    playerBoard[computerTarget[0], computerTarget[1]] = -2;
                    if (IsGameOver(playerBoard))
                    {
                        Console.WriteLine("You lost.");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("The computer missed.");
                    playerBoard[computerTarget[0], computerTarget[1]] = -1;
                }
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

            static void PlayComputerVsComputer()
            {
            Console.WriteLine("\n Game AI vs AI:");
            }
        static int[] GetRandomTarget(int[,] board)
        {
        Random random = new Random();
        int x = random.Next(board.GetLength(0));
        int y = random.Next(board.GetLength(1));
        int[] target = { x, y };
        return target;
        }
        static void DrawBoard(int[,] board)
        {
            Console.WriteLine("  A B C D E F G H I J");
            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.Write((i + 1).ToString().PadLeft(2) + " ");
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    switch (board[i, j])
                    {
                        case -2:
                            Console.Write("X ");
                            break;
                        case -1:
                            Console.Write(". ");
                            break;
                        case 0:
                            Console.Write("| ");
                            break;
                        case 1:
                            Console.Write("O ");
                            break;
                        default:
                            break;
                    }
                }
                Console.WriteLine();
            }
        }
        static bool PlaceShips(int[,] board, int shipSize, bool isVertical, int row, int col)
        {
            int boardSize = board.GetLength(0);

            if (isVertical)
            {
                if (row + shipSize > boardSize)
                {
                    return false;
                }

                for (int i = row; i < row + shipSize; i++)
                {
                    if (board[i, col] != 0)
                    {
                        return false;
                    }
                }

                for (int i = row; i < row + shipSize; i++)
                {
                    board[i, col] = 1;
                }
            }
            else
            {
                if (col + shipSize > boardSize)
                {
                    return false;
                }

                for (int j = col; j < col + shipSize; j++)
                {
                    if (board[row, j] != 0)
                    {
                        return false;
                    }
                }

                for (int j = col; j < col + shipSize; j++)
                {
                    board[row, j] = 1;
                }
            }

            return true;
        }
        static int[] GenerateComputerTarget(int[,] playerBoard)
        {
            int[] target = new int[2];
            int boardSize = playerBoard.GetLength(0);

            Random rand = new Random();
            int row, col;

            do
            {
                row = rand.Next(boardSize);
                col = rand.Next(boardSize);
            }
            while (playerBoard[row, col] < 0);

            target[0] = row;
            target[1] = col;

            return target;
        }
        static bool IsGameOver(int[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
