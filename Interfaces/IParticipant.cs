using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.DataStructures;

namespace BlackJack.Interfaces
{
    interface IParticipant
    {
        public void IncreaseHand(Card card);
        public void CountHandValue(int value);
        public Stack<Card> Hand { get; set; }
    }
}
