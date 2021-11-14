using BlackJack.Interfaces;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.DataStructures
{
    class Player : IParticipant
    {
        //private Stack<Card> splitHand;
        
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
            AceCount = 0;
        }

        public int Chips { get; set; }
        public int HandValue { get; set; }
        public Stack<Card> Hand { get; set; }
        public bool IsSplit { get; set; }
        public Stack<Card> SplitHand { get; set; }
        public int AceCount { get; set; }
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
            if (value == 1&& HandValue<11)
            {
                HandValue += 11;
                AceCount++;
            }
            else if(AceCount!=0&&HandValue+value>21)
            {
                HandValue += value-10;
                AceCount--;
            }
            else
            {
                HandValue += value;
            }
            
        }
    }
}
