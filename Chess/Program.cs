using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{
    static Dictionary<string, char> pieceSymbols = new Dictionary<string, char>
    {
        {"K", '♔'},  // King
        {"Q", '♕'},  // Queen
        {"R", '♖'},  // Rook
        {"B", '♗'},  // Bishop
        {"N", '♘'}   // Knight
    };

    static void Main()
    {
        var pieces = new Dictionary<string, char>();
        DisplayChessboard(pieces);
        HandleUserInput(pieces);
    }

    static void DisplayChessboard(Dictionary<string, char> pieces)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        int size = 8;
        char[] columns = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
        Console.WriteLine("  " + string.Join("  ", columns));

        for (int row = 0; row < size; row++)
        {
            Console.Write((size - row) + " ");
            for (int column = 0; column < size; column++)
            {
                string coord = "" + columns[column] + (size - row);
                if ((row + column) % 2 == 0)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                char pieceChar = pieces.ContainsKey(coord) ? pieces[coord] : ' ';
                Console.Write($" {pieceChar} ");
                Console.ResetColor();
            }
            Console.WriteLine(" " + (size - row));
        }

        Console.WriteLine("  " + string.Join("  ", columns));
        Console.ResetColor();
    }

    static void HandleUserInput(Dictionary<string, char> pieces)
    {
        while (true)
        {
            Console.WriteLine("Enter position (e.g., A8), or 'exit' to quit:");
            string position = Console.ReadLine().ToUpper();
            if (position == "EXIT") break;

            if (!Regex.IsMatch(position, "^[A-H][1-8]$"))
            {
                Console.WriteLine("Invalid position. Please try again.");
                continue;
            }

            Console.WriteLine("Enter piece (K, Q, R, B, N), or 'clear' to remove a piece:");
            string pieceInput = Console.ReadLine().ToUpper();
            if (pieceInput == "CLEAR")
            {
                pieces.Remove(position);
            }
            else if (pieceSymbols.ContainsKey(pieceInput))
            {
                pieces[position] = pieceSymbols[pieceInput];
            }
            else
            {
                Console.WriteLine("Invalid piece. Please try again.");
                continue;
            }

            DisplayChessboard(pieces);
        }
    }
}
