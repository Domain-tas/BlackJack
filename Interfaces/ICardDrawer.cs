using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Interfaces
{
    interface ICardDrawer : IDrawer
    {
        public char[,] Image { get; set; }
    }
}
