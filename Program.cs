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
            //Deck deck = new Deck();
            //deck.GenerateDeck();
            //deck.ShuffleDeck();

            //foreach (Card card in deck.DeckOfCards)
            //{
            //    Console.WriteLine(card.Suite + " " + card.Rank);
            //}
            //Console.WriteLine(deck.DeckOfCards.Count);
            //Console.WriteLine("\n\n\n");
            //UserInput userInput = new UserInput();
            //Card ccard = deck.getTopCard();
            //Console.WriteLine(ccard.Suite + " " + ccard.Rank);
            //Console.WriteLine(deck.DeckOfCards.Count);
            //Card card = deck.getTopCard();
            //Player player = new Player(100);
            //player.increaseHand(card);
            //card = deck.getTopCard();
            //player.increaseHand(card);
            //Console.WriteLine("YourHand: " + player.HandValue);
            //CardImageGenerator drawc = new CardImageGenerator();
            //drawc.Draw(card);
            //for (int i = 0; i < 9; i++)
            //{
            //    for (int j = 0; j < 11; j++)
            //    {
            //        Console.Write(drawc.Image[i, j]);
            //    }
            //    Console.Write("\n");
            //}

            Console.Title = "BlackJack";
            int playerChips = 100;
            bool isPlaying = true;
            Deck deck = new Deck();
            deck.GenerateDeck();
            deck.ShuffleDeck();
            Card card;
            Player player = new Player(playerChips);
            Dealer dealer = new Dealer();
            DiplsayManager displayManager = new DiplsayManager();
            GameManager gameManager = new GameManager(deck, player, dealer, displayManager);
            UserInput userInput = new UserInput();
            while (isPlaying)
            {

                Console.Clear();
                displayManager.DrawBet(player.Chips);
                gameManager.UpdateChips(userInput.HandleBet());
                Console.Clear();
                Console.CursorVisible=false;
                displayManager.DrawAnnotations();
                gameManager.DealFirstHand();
                while (gameManager.IsPlayingRound)
                {
                    //Console.WriteLine("Your current hand value:" + player.HandValue);
                    //Console.WriteLine("Dealer's current hand value:" + dealer.HandValue);
                    gameManager.EndGameCheck();
                    if (gameManager.IsPlayingRound==false)
                    {
                        break;
                    }
                    gameManager.Action(userInput.HandleInput());
                    //if (!gameManager.IsPlayingRound)
                    //{
                    //    break;
                    //}
                }
                //isPlayingRound = false;
                //Console.WriteLine("Do you want to play another round? Y/N");
                gameManager.Action(userInput.HandleRepeat());
                isPlaying = gameManager.IsPlaying;
            }
        }
        public void DealingInitialHand()
        {

        }
    }
}
