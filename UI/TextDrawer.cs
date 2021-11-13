using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Interfaces;

namespace BlackJack.UI
{
    class TextDrawer : IDrawer
    {
        private int verticalSpace;
        private int horizontalSpace;
        private string[] text;
        public TextDrawer(int verticalSpace, int horizontalSpace, string[] text)
        {
            this.horizontalSpace = horizontalSpace;
            this.verticalSpace = verticalSpace;
            this.text = text;
        }
        public void Draw()
        {
            Clean();
            for (int i =0; i < text.Length;i++)
            {
                Console.SetCursorPosition(horizontalSpace,verticalSpace+i);
                Console.Write(text[i]);
            }
        }

        public void Clean()
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.SetCursorPosition(horizontalSpace, verticalSpace + i);
                Console.Write("                                                      ");
            }
        }
    }
}
