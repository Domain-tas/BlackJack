using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Interfaces;

namespace BlackJack.DataStructures
{
    class Player : IParticipant
    {
        private int chips;
        private Stack<Card> hand;
        private bool isSplit;
        private int handValue;
        //private string playerName;

        public Player(int chips)
        {
            Chips = chips;
            IsSplit = false;
            handValue = 0;
            hand = new Stack<Card>();
        }

        public int Chips { get => chips; set => chips = value; }
        public int HandValue { get => handValue; set => handValue = value; }
        internal Stack<Card> Hand { get => hand; set => hand = value; }
        public bool IsSplit { get => isSplit; set => isSplit = value; }
        public Stack<Card> SplitHand { get; set; }

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
            handValue += value;
        }
        public void Reset()
        {
            Hand=new Stack<Card>();
            SplitHand = new Stack<Card>();
            IsSplit = false;
            HandValue = 0;
        }
    }
}
