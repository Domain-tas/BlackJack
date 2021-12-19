using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.UI
{
    //ŠALINAMA
    class Texts
    {
        private string[] betText;
        private string[] rulesText;

        public Texts(int chips)
        {
            betText = new[] {"Your current credits: " + chips, "Place your bet: "};

        }
    }
}
