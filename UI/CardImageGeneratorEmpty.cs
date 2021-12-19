using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Interfaces;

namespace BlackJack.UI
{
    class CardImageGeneratorEmpty : ICardDrawer
    {
        private char[,] cardImage;

        public CardImageGeneratorEmpty()
        {
            this.cardImage = new char[9, 11];
        }

        public char[,] Image { get => cardImage; set => cardImage = value; }

        public void Draw()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (i == 0)
                    {
                        cardImage[i, j] = '_';
                    }
                    else if (i == 8)
                    {
                        cardImage[i, j] = '~';
                    }
                    else if (j == 0 || j == 10)
                    {
                        cardImage[i, j] = '|';
                    }
                    else
                        cardImage[i, j] = '*';
                }
            }
        }
    }
}
