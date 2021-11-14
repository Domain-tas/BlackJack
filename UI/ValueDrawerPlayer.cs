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
        //private int alternativeHandValue;
        private int horizontalOffset;

        public ValueDrawerPlayer(int verticalOffset, int handValue)
        {
            horizontalOffset = 8;
            this.verticalOffset = verticalOffset;
            this.handValue = handValue;
            //this.alternativeHandValue = alternativeHandValue;
        }

        public void Draw()
        {
            Console.SetCursorPosition(horizontalOffset + 25, verticalOffset - 4);
            Console.Write("        ");
            Console.SetCursorPosition(horizontalOffset + 25, verticalOffset - 4);
            Console.Write(handValue);
        }
    }
}