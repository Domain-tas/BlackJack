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
        private int lineCount;
        private string[] text;
        public TextDrawer(int verticalSpace, int horizontalSpace, int lineCount, string[] text)
        {
            this.horizontalSpace = horizontalSpace;
            this.verticalSpace = verticalSpace;
            this.lineCount = lineCount;
            this.text = text;
        }
        public void Draw()
        {
            for (int i = 0; i < lineCount; i++)
            {
                Console.SetCursorPosition(horizontalSpace,verticalSpace);
                Console.Write(text[i]);
            }
        }
    }
}
