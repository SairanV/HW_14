using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    public class Game
    {
        private List<Player> players = new List<Player>();
        private List<Karta> deck = new List<Karta>();
        string[] masts = { "Черви", "Бубны", "Трефы", "Пики" };
        string[] tips = { "6", "7", "8", "9", "10", "Валет", "Дама", "Король", "Туз" };

        public Game(int numberOfPlayers)
        {
            InitializeDeck();  // Инициализация колоды карт

            for (int i = 0; i < numberOfPlayers; i++)
            {
                players.Add(new Player());
            }

            ShuffleDeck(); // Перетасовка карт

            DealCards(); // Раздача карт игрокам
        }

        private void InitializeDeck()
        {
            foreach (var mast in masts)
            {
                foreach (var tip in tips)
                {
                    deck.Add(new Karta(mast, tip));
                }
            }
        }

        private void ShuffleDeck()
        {
            Random random = new Random();
            deck = deck.OrderBy(card => random.Next()).ToList();
        }

        private void DealCards()
        {
            int playerIndex = 0;
            foreach (var card in deck)
            {
                players[playerIndex].Cards.Add(card);
                playerIndex = (playerIndex + 1) % players.Count;
            }
        }

        public void Play()
        {
            while (players.Any(player => player.Cards.Count > 0))
            {
                List<Karta> cardsInPlay = players.Select(player => player.Cards.First()).ToList();
                int maxIndex = GetMaxCardIndex(cardsInPlay);

                players[maxIndex].Cards.AddRange(cardsInPlay);
                foreach (var player in players)
                {
                    player.Cards.RemoveAt(0);
                }
            }

            int winnerIndex = GetWinnerIndex();
            Console.WriteLine($"Игрок {winnerIndex + 1} выиграл!");
        }

        private int GetMaxCardIndex(List<Karta> cards)
        {
            int maxIndex = 0;
            for (int i = 1; i < cards.Count; i++)
            {
                if (CompareCards(cards[i], cards[maxIndex]) > 0)
                {
                    maxIndex = i;
                }
            }
            return maxIndex;
        }

        private int CompareCards(Karta card1, Karta card2)
        {
            if (card1.Tip == card2.Tip)
            {
                return 0;
            }
            else if (card1.Tip == "6" && card2.Tip == "Туз")
            {
                return -1;
            }
            else if (card1.Tip == "Туз" && card2.Tip == "6")
            {
                return 1;
            }
            else
            {
                int tipComparison = Array.IndexOf(tips, card1.Tip) - Array.IndexOf(tips, card2.Tip);

                if (tipComparison == 0)
                {
                    return string.Compare(card1.Mast, card2.Mast, StringComparison.Ordinal);
                }

                return tipComparison;
            }
        }


        private int GetWinnerIndex()
        {
            int maxCards = 0;
            int winnerIndex = 0;

            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Cards.Count > maxCards)
                {
                    maxCards = players[i].Cards.Count;
                    winnerIndex = i;
                }
            }

            return winnerIndex;
        }
    }
}