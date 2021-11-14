using BlackJack.Controls;
using BlackJack.DataStructures;
using BlackJack.UI;
using System;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "BlackJack";
            int playerChips = 100;
            bool isPlaying = true;
            Player player = new Player(playerChips);
            while (isPlaying)
            {
                Deck deck = new Deck();
                deck.GenerateDeck();
                deck.ShuffleDeck();
                Card card;
                Dealer dealer = new Dealer();
                DiplsayManager displayManager = new DiplsayManager();
                GameManager gameManager = new GameManager(deck, player, dealer, displayManager);
                UserInput userInput = new UserInput();
                Console.Clear();
                displayManager.DrawInitialScreen(player.Chips);
                if (player.Chips == 0)
                {
                    gameManager.GameOver();
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                gameManager.UpdateChips(userInput.HandleBet());
                Console.Clear();
                Console.CursorVisible=false;
                displayManager.DrawAnnotations();
                gameManager.DealFirstHand();
                while (gameManager.IsPlayingRound)
                {
                    gameManager.EndGameCheck();
                    if (gameManager.IsPlayingRound==false)
                    {
                        break;
                    }
                    gameManager.Action(userInput.HandleInput());
                }
                gameManager.Action(userInput.HandleRepeat());
                isPlaying = gameManager.IsPlaying;
            }
        }
    }
}
