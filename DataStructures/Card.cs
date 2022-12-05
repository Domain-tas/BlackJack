using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.DataStructures
{
    public class Card
    {
        private int value;
        private string suite;
        private int rank;
        private string rankSymbol;

        public Card(int value, string suite, int rank)
        {
            value = (value > 10) ? 10 : value;
            this.value = value;
            this.suite = suite;
            this.rank = rank;
            if(rank==1)
            {
                this.rankSymbol = "A";
            }
            else if (rank==11)
            {
                this.rankSymbol = "J";
            }
            else if (rank == 12)
            {
                this.rankSymbol = "Q";
            }
            else if (rank == 13)
            {
                this.rankSymbol = "K";
            }
            else 
                this.rankSymbol = rank.ToString();
        }

        public int Value { get => value; set => this.value = value; }
        public string Suite { get => suite; set => suite = value; }
        public int Rank { get => rank; set => rank = value; }
        public string RankSymbol { get => rankSymbol; set => rankSymbol = value; }
    }
}
