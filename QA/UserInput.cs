using System;

namespace BlackJack.Controls
{
    internal class UserInput
    {
        internal int HandleBet()
        {
            string input = Console.ReadLine();
            while (true)
            {
                if (Int32.TryParse(input, out int betAmount) && betAmount > 0)
                {
                    return betAmount;
                }
                Console.WriteLine("Invalid input. Try again:");
                input = Console.ReadLine();
            }
        }

        public string HandleInput()
        {
            ConsoleKey keyPressed = Console.ReadKey().Key;
            while (true)
            {
                if (keyPressed == ConsoleKey.DownArrow)
                {
                    return "stand";
                }

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    return "hit";
                }

                if (keyPressed == ConsoleKey.Spacebar)
                {
                    return "split";
                }

                if (keyPressed == ConsoleKey.RightArrow)
                {
                    return "double";
                }

                if (keyPressed == ConsoleKey.Q)
                {
                    return "quit";
                }
                keyPressed = Console.ReadKey().Key;
            }
        }

        internal string HandleRepeat()
        {
            ConsoleKey keyPressed = Console.ReadKey().Key;
            bool isValid = false;
            while (!isValid)
            {
                if (keyPressed == ConsoleKey.Y)
                {
                    isValid = true;
                    return "continue";
                }
                if (keyPressed == ConsoleKey.N)
                {
                    isValid = true;
                    return "end";
                }
                if (keyPressed == ConsoleKey.Q)
                {
                    isValid = true;
                    return "quit";
                }

                keyPressed = Console.ReadKey().Key;

            }
            return "error";
        }
    }
}