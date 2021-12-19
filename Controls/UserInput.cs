using System;

namespace BlackJack.Controls
{
    internal class UserInput
    {
        internal int HandleBet()
        {
            bool isValid = false;
            string input = Console.ReadLine();
            while (!isValid)
            {
                if (Int32.TryParse(input, out int betAmount))
                {
                    if (betAmount > 0)
                    {
                        isValid = true;
                        return betAmount;
                    }
                }
                Console.WriteLine("Invalid input. Try again:");
                input = Console.ReadLine();
            }
            return 0;

        }

        public string HandleInput()
        {
            ConsoleKey keyPressed = Console.ReadKey().Key;
            bool isValid = false;
            while (!isValid)
            {
                if (keyPressed == ConsoleKey.DownArrow)
                {
                    isValid = true;
                    return "stand";
                }
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    isValid = true;
                    return "hit";
                }
                if (keyPressed == ConsoleKey.Spacebar)
                {
                    isValid = true;
                    return "split";
                }
                if (keyPressed == ConsoleKey.RightArrow)
                {
                    isValid = true;
                    return "double";
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