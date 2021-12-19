using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.UI
{
    //ŠALINAMA
    class CardDrawer
    {
        private int dealerOffsetVertical;
        private int offsetHorizontal;
        private int offsetVertical;
        private int playerOffsetVertical;

        public CardDrawer(int cardDrawingHeightDealer, int cardDrawingHeightPlayer)
        {
            this.dealerOffsetVertical = cardDrawingHeightDealer;
            this.playerOffsetVertical = cardDrawingHeightPlayer;
            this.offsetHorizontal = 5;
            this.offsetVertical = cardDrawingHeightDealer;
        }

        public void DrawCards(char[,] cardImage, string participant, int currentCardCount, bool isSplit)
        {
            if (participant == "player")
            {
                offsetVertical = playerOffsetVertical;
            }
            else
            {
                offsetVertical = dealerOffsetVertical;
            }

            offsetVertical += currentCardCount;
            offsetHorizontal += currentCardCount * 3;
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
