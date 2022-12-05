using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.UI
{
    class ValueDrawerPlayer
    {
        private int verticalOffset;
        private int handValue;
        private int handValueHorizontalOffset;
        private int horizontalOffset;

        public ValueDrawerPlayer(int verticalOffset, int handValue)
        {
            horizontalOffset = 8;
            this.verticalOffset = verticalOffset;
            this.handValue = handValue;
            handValueHorizontalOffset = 25;
        }

        public void Draw()
        {
            Console.SetCursorPosition(horizontalOffset + handValueHorizontalOffset, verticalOffset - 4);
            Console.Write("        ");
            Console.SetCursorPosition(horizontalOffset + handValueHorizontalOffset, verticalOffset - 4);
            Console.Write(handValue);
        }
    }
}