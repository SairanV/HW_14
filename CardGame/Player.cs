using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    public class Player
    {
        public List<Karta> Cards { get; set; } = new List<Karta>();

        public void DisplayCards()
        {
            Console.WriteLine("Имеющиеся карты:");
            foreach (var card in Cards)
            {
                Console.WriteLine($"{card.Mast} {card.Tip}");
            }
            Console.WriteLine();
        }
    }
}
