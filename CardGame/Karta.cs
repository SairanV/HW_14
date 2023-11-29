using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    public class Karta
    {
        public string Mast { get; set; }
        public string Tip { get; set; }

        public Karta(string mast, string tip)
        {
            Mast = mast;
            Tip = tip;
        }
    }
}
