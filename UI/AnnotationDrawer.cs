using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Interfaces;

namespace BlackJack.UI
{
    class AnnotationDrawer : IDrawer
    {
        private int horizontalOffset;
        private int dealerVerticalOffset;
        private int playerVerticalOffset;

        public AnnotationDrawer(int dealerDrawHeight, int playerDrawHeight)
        {
            this.horizontalOffset = 15;
            this.dealerVerticalOffset = dealerDrawHeight;
            this.playerVerticalOffset = playerDrawHeight;
        }

        public void Draw()
        {
            
            Console.SetCursorPosition(horizontalOffset, dealerVerticalOffset - 5);
            Console.Write("#################");
            Console.SetCursorPosition(horizontalOffset, dealerVerticalOffset - 4);
            Console.Write("# Dealer's hand #");
            Console.SetCursorPosition(horizontalOffset, dealerVerticalOffset - 3);
            Console.Write("#################");

            Console.SetCursorPosition(horizontalOffset+16, dealerVerticalOffset - 5);
            Console.Write("#################");
            Console.SetCursorPosition(horizontalOffset+16, dealerVerticalOffset - 4);
            Console.Write("# Value:        #");
            Console.SetCursorPosition(horizontalOffset+16, dealerVerticalOffset - 3);
            Console.Write("#################");

            Console.SetCursorPosition(horizontalOffset, playerVerticalOffset - 5);
            Console.Write("#################");
            Console.SetCursorPosition(horizontalOffset, playerVerticalOffset - 4);
            Console.Write("#   Your hand   #");
            Console.SetCursorPosition(horizontalOffset, playerVerticalOffset - 3);
            Console.Write("#################");

            Console.SetCursorPosition(horizontalOffset+16, playerVerticalOffset - 5);
            Console.Write("#################");
            Console.SetCursorPosition(horizontalOffset+16, playerVerticalOffset - 4);
            Console.Write("# Value:        #");
            Console.SetCursorPosition(horizontalOffset+16, playerVerticalOffset - 3);
            Console.Write("#################");
        }

        public void updateHandValue(int verticalOffset, int handValue)
        {
            Console.SetCursorPosition(horizontalOffset + 25, verticalOffset - 4);
            Console.Write("   ");
            Console.SetCursorPosition(horizontalOffset + 25, verticalOffset - 4);
            Console.Write(handValue);
        }
    }
}
