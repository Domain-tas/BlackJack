using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlackJack.Interfaces;

namespace BlackJack.UI
{
    class TextDrawer : IDrawer
    {
        private int verticalSpace;
        private int horizontalSpace;
        private string[] text;
        private int drawableWidth;
        public TextDrawer(int verticalSpace, int horizontalSpace, int drawableWidth, string[] text)
        {
            this.horizontalSpace = horizontalSpace;
            this.verticalSpace = verticalSpace;
            this.text = text;
            this.drawableWidth = drawableWidth;
        }
        public void Draw()
        {
            int lineCount = 0;
            for (int i = 0; i < text.Length; i++)
            {
                
                Console.SetCursorPosition(horizontalSpace, verticalSpace + i + lineCount);
                for (int j = 0; j < text[i].Length; j++)
                {
                    Console.Write(text[i][j]);
                    if (Console.GetCursorPosition().Left == drawableWidth - 3 && j+1<text[i].Length)
                    {
                        if (text[i][j + 1] != ' ')
                        {
                            Console.Write('-');
                        }
                        else
                        {
                            j++;
                        }
                        lineCount++;
                        Console.SetCursorPosition(horizontalSpace, verticalSpace + i + lineCount);
                    }
                }
                lineCount++;
            }
        }

        //public void Clean()
        //{
        //    for (int i = 0; i < text.Length; i++)
        //    {
        //        Console.SetCursorPosition(horizontalSpace, verticalSpace + i);
        //        for (int j = 0; j < drawableWidth; j++)
        //        {
        //            Console.Write(' ');
        //        }
                
        //    }
        //}
    }
}