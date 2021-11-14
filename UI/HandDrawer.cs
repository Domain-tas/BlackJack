using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlackJack.Interfaces;

namespace BlackJack.UI
{
    class HandDrawer : IDrawer
    {
        private int offsetHorizontal;
        private int offsetVertical;
        private char[,] cardImage;
        private int currentCardCount;
        private bool isSplit;

        public HandDrawer(int offsetVertical, char[,] cardImage, int currentCardCount, bool isSplit)
        {
            offsetHorizontal = 5;
            this.offsetVertical = offsetVertical;
            this.cardImage = cardImage;
            this.currentCardCount = currentCardCount;
            this.isSplit = isSplit;
        }

        public void Draw()
        {
            Thread.Sleep(400);
            offsetVertical += currentCardCount;
            offsetHorizontal += currentCardCount * 3;
            if (isSplit)
                offsetHorizontal *= 3;
            int numberDrawn = 0;
            int linesDrawn = 0;
            Console.SetCursorPosition(offsetHorizontal, offsetVertical);
            foreach (char symbol in cardImage)
            {
                Console.SetCursorPosition(offsetHorizontal + numberDrawn, offsetVertical + linesDrawn);
                Console.Write(symbol);
                numberDrawn++;
                if (numberDrawn == 11)
                {
                    linesDrawn++;
                    numberDrawn = 0;
                }
            }
        }
    }
}
