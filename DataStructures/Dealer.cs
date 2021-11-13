using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Interfaces;

namespace BlackJack.DataStructures
{
    class Dealer : IParticipant
    {
        private const int standNumber = 17;
        private Stack<Card> hand;
        private bool hasRevealed;
        private int handValue;

        public bool HasRevealed { get => hasRevealed; set => hasRevealed = value; }
        public int HandValue { get => handValue; set => handValue = value; }
        internal Stack<Card> Hand { get => hand; set => hand = value; }

        public Dealer()
        {
            this.Hand = new Stack<Card>();
            this.hasRevealed = false;
            this.handValue = 0;
        }

        public void IncreaseHand(Card card)
        {
            Hand.Push(card);
            CountHandValue(card.Value);
        }
        public void CountHandValue(int value)
        {
            HandValue += value;
        }
        public void DealerStand(Deck deck)
        {
            while(HandValue<=17)
            {
                IncreaseHand(deck.GetTopCard());
            }
        }

        public void Reset()
        {
            Hand = new Stack<Card>();
            HandValue = 0;
            hasRevealed = false;
        }
    }
}
