using BlackJack.Interfaces;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.DataStructures
{
    class Player : IParticipant
    {
        //private Stack<Card> splitHand;
        private int aceCount;
        //private Stack<Card> hand;
        //private int handValue;
        //private string playerName;

        public Player(int chips)
        {
            Chips = chips;
            IsSplit = false;
            HandValue = 0;
            Hand = new Stack<Card>();
            SplitHand = new Stack<Card>();
            aceCount = 0;
        }

        public int Chips { get; set; }

        public int HandValue { get; set; }

        internal Stack<Card> Hand { get; set; }

        public bool IsSplit { get; set; }

        public Stack<Card> SplitHand { get; set; }

        //public int AlternativeHandValue { get; set; }

        public void IncreaseHand(Card card)
        {
            Hand.Push(card);
            CountHandValue(card.Value);
        }
        public void IncreaseSplitHand(Card card)
        {
            SplitHand.Push(card);
            CountHandValue(card.Value);
        }
        public void CountHandValue(int value)
        {
            if (value == 1&& HandValue<10)
            {
                HandValue += 11;
                aceCount++;
            }
            else if(aceCount!=0&&HandValue+value>21)
            {
                HandValue += value-10;
                //AlternativeHandValue += value;
                aceCount--;
            }
            else
            {
                HandValue += value;
            }
            
        }
        public void Reset()
        {
            Hand=new Stack<Card>();
            SplitHand = new Stack<Card>();
            IsSplit = false;
            HandValue = 0;
            //AlternativeHandValue = 0;
            aceCount = 0;
        }
    }
}
