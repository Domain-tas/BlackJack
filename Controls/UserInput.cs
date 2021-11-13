using System;

namespace BlackJack.Controls
{
    internal class UserInput
    {
        internal int HandleBet()
        {
            bool isValid = false;
            string input = Console.ReadLine();
            while(!isValid)
            {
                if (Int32.TryParse(input, out int betAmount))
                {
                    isValid = true;
                    return betAmount;
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again:");
                    input = Console.ReadLine();
                }
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
                else if (keyPressed == ConsoleKey.UpArrow)
                {
                    isValid = true;
                    return "hit";
                }
                else if (keyPressed == ConsoleKey.Spacebar)
                {
                    isValid = true;
                    return "split";
                }
                else if (keyPressed == ConsoleKey.RightArrow)
                {
                    isValid = true;
                    return "double";
                }
                else
                {
                    //Console.WriteLine("Invalid input. Try again:");
                    keyPressed = Console.ReadKey().Key;
                }
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
                else if (keyPressed == ConsoleKey.N)
                {
                    isValid = true;
                    return "end";
                }
                else if (keyPressed == ConsoleKey.Q)
                {
                    isValid = true;
                    return "quit";
                }
                else
                {
                    keyPressed = Console.ReadKey().Key;
                }
            }
            return "error";
        }
    }
}