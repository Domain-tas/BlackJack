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
        private bool hasRevealed;

        public ValueDrawerDealer(int verticalOffset, int handValue, bool hasRevealed)
        {
            horizontalOffset = 8;
            this.verticalOffset = verticalOffset;
            this.handValue = handValue;
            this.hasRevealed = hasRevealed;
        }

        public void Draw()
        {
            Console.SetCursorPosition(horizontalOffset + 25, verticalOffset - 4);
            Console.Write("       ");
            Console.SetCursorPosition(horizontalOffset + 25, verticalOffset - 4);
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
