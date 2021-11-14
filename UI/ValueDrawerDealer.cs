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

        public ValueDrawerDealer(int verticalOffset, int handValue)
        {
            horizontalOffset = 8;
            this.verticalOffset = verticalOffset;
            this.handValue = handValue;
        }
        public void Draw()
    {
        Console.SetCursorPosition(horizontalOffset + 25, verticalOffset - 4);
        Console.Write("       ");
        Console.SetCursorPosition(horizontalOffset + 25, verticalOffset - 4);
        Console.Write(handValue + " + ?");
    }
    }
}
