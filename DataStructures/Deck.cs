using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.DataStructures
{
    class Deck
    {
        //private readonly string[] suites = {"diamonds","clubs","hearts","spades"};
        private readonly string[] suites = { "+", "@","O", "#" };
        private static Random rng = new Random();
        private List<Card> deckOfCards;

        public Deck()
        { 
            deckOfCards = new List<Card>();
        }

        public List<Card> DeckOfCards { get => deckOfCards; set => deckOfCards = value; }

        public void GenerateDeck()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 14; j++)
                {
                    Card card = new Card(j,suites[i],j);
                    deckOfCards.Add(card);          
                }
            }
            //return deckOfCards;
        }
        public void ShuffleDeck()
        {
            deckOfCards = deckOfCards.OrderBy(a => rng.Next()).ToList();
        }
        public Card GetTopCard()
        {
            Card topCard = deckOfCards[0];
            deckOfCards.RemoveAt(0);
            return topCard;
        }
    }
}
