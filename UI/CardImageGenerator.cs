using BlackJack.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Interfaces;

namespace BlackJack.UI
{
    class CardImageGenerator : ICardDrawer
    {
        private char[,] cardImage;
        private Card card;

        public CardImageGenerator(Card card)
        {
            this.cardImage = new char[9, 11];
            this.card = card;
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
                    else if ((i == 1 && j == 1))
                    {
                        if (card.RankSymbol == "10")
                        {
                            cardImage[i, j] = card.RankSymbol[0];
                            cardImage[i, j + 1] = card.RankSymbol[1];
                            j++;
                        }
                        else
                        {
                            cardImage[i, j] = card.RankSymbol[0];
                        }
                    }
                    else if ((i == 7 && j == 9))
                    {
                        if (card.RankSymbol == "10")
                        {
                            cardImage[i, j - 1] = card.RankSymbol[0];
                            cardImage[i, j] = card.RankSymbol[1];
                        }
                        else
                        {
                            cardImage[i, j] = card.RankSymbol[0];
                        }
                    }
                    else if ((i == 2 && j == 1) || (i == 6 && j == 9))
                    {
                        cardImage[i, j] = card.Suite[0];
                    }
                    else
                        cardImage[i, j] = ' ';


                }
            }
            //return cardImage;
        }
    }
}
