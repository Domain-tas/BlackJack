using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.UI
{
    class TableCleaner
    {
        private int verticalSpace;
        private int horizontalSpace;
        private int lineCount;
        private int leftPosition;
        public TableCleaner(int verticalSpace, int horizontalSpace, int lineCount, int leftPosition)
        {
            this.horizontalSpace = horizontalSpace;
            this.verticalSpace = verticalSpace;
            this.lineCount = lineCount;
            this.leftPosition = leftPosition;
        }

        public void Clean()
        {
            //int width = 0;
            for (int i = 0; i < lineCount; i++)
            {
                Console.SetCursorPosition(leftPosition, verticalSpace+i);
                for (int j = 0; j <horizontalSpace;j++) 
                {
                    Console.Write(' ');
                    //width++;
                }
            }
            
        }
    }
}
