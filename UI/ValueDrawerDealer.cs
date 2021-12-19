using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Interfaces;

namespace BlackJack.UI
{
    class ValueDrawerDealer : IDrawer
    {
        private int verticalOffset;
        private int handValue;
        private int horizontalOffset;
        private int handValueHorizontalOffset;
        private bool hasRevealed;

        public ValueDrawerDealer(int verticalOffset, int handValue, bool hasRevealed)
        {
            horizontalOffset = 8;
            this.verticalOffset = verticalOffset;
            this.handValue = handValue;
            this.hasRevealed = hasRevealed;
            this.handValueHorizontalOffset = 25;
        }

        public void Draw()
        {
            Console.SetCursorPosition(horizontalOffset + handValueHorizontalOffset, verticalOffset - 4);
            Console.Write("       ");
            Console.SetCursorPosition(horizontalOffset + handValueHorizontalOffset, verticalOffset - 4);
            if (hasRevealed)
            {
                Console.Write(handValue);
            }
            else
            {
                Console.Write(handValue + " + ?");
            }
        }
    }
}
