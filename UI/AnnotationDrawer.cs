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
        private int horizontalOffsetScore;

        public AnnotationDrawer(int dealerDrawHeight, int playerDrawHeight)
        {
            this.horizontalOffset = 8;
            this.horizontalOffsetScore = 16;
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

            Console.SetCursorPosition(horizontalOffset+horizontalOffsetScore, dealerVerticalOffset - 5);
            Console.Write("#################");
            Console.SetCursorPosition(horizontalOffset+ horizontalOffsetScore, dealerVerticalOffset - 4);
            Console.Write("# Value:        #");
            Console.SetCursorPosition(horizontalOffset+ horizontalOffsetScore, dealerVerticalOffset - 3);
            Console.Write("#################");

            Console.SetCursorPosition(horizontalOffset, playerVerticalOffset - 5);
            Console.Write("#################");
            Console.SetCursorPosition(horizontalOffset, playerVerticalOffset - 4);
            Console.Write("#   Your hand   #");
            Console.SetCursorPosition(horizontalOffset, playerVerticalOffset - 3);
            Console.Write("#################");

            Console.SetCursorPosition(horizontalOffset+ horizontalOffsetScore, playerVerticalOffset - 5);
            Console.Write("##################");
            Console.SetCursorPosition(horizontalOffset+ horizontalOffsetScore, playerVerticalOffset - 4);
            Console.Write("# Value:         #");
            Console.SetCursorPosition(horizontalOffset+ horizontalOffsetScore, playerVerticalOffset - 3);
            Console.Write("##################");
        }

    }
}
