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
        private const int StandNumber = 17;
        public int HandValue { get; set; }

        public int HiddenValue { get; set; }

        internal Stack<Card> Hand { get; set; }

        public bool HasRevealed { get; set; }

        public Dealer()
        {
            Hand = new Stack<Card>();
            HandValue = 0;
            HasRevealed = false;
            HiddenValue = 0;
        }

        public void IncreaseHand(Card card)
        {
            Hand.Push(card);
            CountHandValue(card.Value);
        }
        public void CountHandValue(int value)
        {
            if (value == 1) value = 11;
            if (Hand.Count == 1)
            {
                HiddenValue += value;
            }
            else
            {
                HandValue += value;
            }

        }
        //public void DealerStand(Deck deck)
        //{
        //    while(HandValue<=17)
        //    {
        //        IncreaseHand(deck.GetTopCard());
        //    }
        //}

        public void Reset()
        {
            Hand = new Stack<Card>();
            HandValue = 0;
            HasRevealed = false;
            HiddenValue = 0;
        }
    }
}
