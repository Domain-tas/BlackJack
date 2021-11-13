using BlackJack.DataStructures;
using BlackJack.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Controls
{
    class GameManager
    {
        private int betSize;
        private Deck deck;
        private Card card;
        private Player player;
        private Dealer dealer;
        private bool isPlayingRound;
        private bool isPlaying;
        private DiplsayManager displayManager;
        private bool usingSplit;

        public GameManager(Deck deck, Player player, Dealer dealer, DiplsayManager displayManager)
        {
            this.deck = deck;
            this.player = player;
            this.betSize = 0;
            this.dealer = dealer;
            this.IsPlayingRound = true;
            this.isPlaying = true;
            this.displayManager = displayManager;
            this.usingSplit = false;
        }
        public int BetSize { get => betSize; set => betSize = value; }
        public bool IsPlaying { get => isPlaying; set => isPlaying = value; }
        public bool IsPlayingRound { get => isPlayingRound; set => isPlayingRound = value; }

        internal void Action(string action)
        {
            if (action == "hit")
            {
                card = deck.GetTopCard();
                player.IncreaseHand(card);
                displayManager.DrawCards(card,"player", player.Hand.Count(), usingSplit);
                //Console.WriteLine("hit");
            }
            if (action == "stand")
            {
                if (usingSplit)
                {
                    usingSplit = false;
                }
                else
                {
                    dealer.HasRevealed = true;
                    DealerStand(deck);
                    //Console.WriteLine("stand");
                    EndGameCheck();
                }

            }
            if (action == "double")
            {
                //Console.WriteLine("double");
                UpdateChips(BetSize);
            }
            if (action == "split")
            {
                ManageSplit();
                //Console.WriteLine("split");
            }
            if (action == "quit" || action == "end")
            {
                //Console.WriteLine("ending...");
                EndApplication();

            }
            if (action == "continue")
            {
                ResetGameManager();
            }
        }

        public void DealerStand(Deck deck)
        {
            while (dealer.HandValue <= 17)
            {
                card = deck.GetTopCard();
                dealer.IncreaseHand(card);
                displayManager.DrawCards(card,"dealer", dealer.Hand.Count(), false);
            }
        }

        public void EndGameCheck()
        {
            bool isBust = player.HandValue > 21;
            bool hasWon = dealer.HasRevealed && player.HandValue > dealer.HandValue;
            bool hasWonBust = dealer.HasRevealed && dealer.HandValue>21;
            bool isDraw = dealer.HasRevealed && player.HandValue == dealer.HandValue;
            bool hasLost = dealer.HasRevealed && player.HandValue < dealer.HandValue;
            bool hasLostBlackJack = !dealer.HasRevealed && dealer.HandValue == 21 && player.HandValue != dealer.HandValue;
            bool hasWonBlackJack = !dealer.HasRevealed && player.HandValue == 21 && player.HandValue != dealer.HandValue;
            bool isDrawBlackJack = !dealer.HasRevealed && player.HandValue == 21 && player.HandValue == dealer.HandValue;
            if (isBust)
            { 
                displayManager.CleanTable();
                Console.WriteLine("It's a bust. Your hand value: " + player.HandValue);
                EndRound();
            }
            if (hasWon)
            {
                Console.WriteLine("You've won! Your hand value: " + player.HandValue);
                Console.WriteLine("Dealer's hand value: " + dealer.HandValue);
                AwardPlayer();
                EndRound();
            }
            if (hasWonBust)
            {
                Console.WriteLine("You've won! Your hand value: " + player.HandValue);
                Console.WriteLine("Dealer has busted: " + dealer.HandValue);
                AwardPlayer();
                EndRound();
            }
            if (isDraw)
            {
                Console.WriteLine("It's a draw. Your hand value: " + player.HandValue);
                Console.WriteLine("Dealer's hand value: " + dealer.HandValue);
                UpdateChips(-BetSize);
                EndRound();
            }
            if (hasLost)
            {
                Console.WriteLine("You've lost. Your hand value: " + player.HandValue);
                Console.WriteLine("Dealer's hand value: " + dealer.HandValue);
                UpdateChips(-BetSize);
                EndRound();
            }
            if (hasLostBlackJack)
            {
                Console.WriteLine("You've lost. Your hand value: " + player.HandValue);
                Console.WriteLine("Dealer has a BlackJack: " + dealer.HandValue);
                EndRound();
            }
            if (hasWonBlackJack)
            {
                Console.WriteLine("You've won! It's a BlackJack: " + player.HandValue);
                Console.WriteLine("Dealer's hand value: " + dealer.HandValue);
                UpdateChips(BetSize / 2);
                AwardPlayer();
                EndRound();
            }
            if (isDrawBlackJack)
            {
                Console.WriteLine("It's a draw! you've got a BlackJack:" + player.HandValue);
                Console.WriteLine("Dealer has a BlackJack too:" + dealer.HandValue);
                UpdateChips(-BetSize);
                EndRound();
            }
            Console.WriteLine("Error");
        }

        public void EndRound()
        {
            IsPlayingRound = false;
        }

        public void EndApplication()
        {
            IsPlaying = false;
        }

        private void ResetGameManager()
        {
            player.Reset();
            dealer.Reset();
            IsPlayingRound = true;
        }

        internal void UpdateChips(int betAmount)
        {
            BetSize += betAmount;
            player.Chips -= betAmount;
        }
        internal void AwardPlayer()
        {
            player.Chips += BetSize * 2;
        }

        public void DealFirstHand()
        {
            card = deck.GetTopCard();
            player.IncreaseHand(card);
            displayManager.DrawCards(card, "player", player.Hand.Count, player.IsSplit);
            card = deck.GetTopCard();
            player.IncreaseHand(card);
            displayManager.DrawCards(card, "player", player.Hand.Count, player.IsSplit);
            card = deck.GetTopCard();
            dealer.IncreaseHand(card);
            displayManager.DrawCards(card, "dealer", dealer.Hand.Count, player.IsSplit);
            card = deck.GetTopCard();
            dealer.IncreaseHand(card);
            displayManager.DrawCards(card, "dealer", dealer.Hand.Count, player.IsSplit);
        }

        public void ManageSplit()
        {
            usingSplit = true;
            player.IsSplit = true;
            displayManager.CleanTable("player");
            player.SplitHand.Push(player.Hand.Pop());
            Card card = player.SplitHand.Peek();
            displayManager.DrawCards(card, "player", player.SplitHand.Count, true);
            card = player.Hand.Peek();
            displayManager.DrawCards(card, "player", player.SplitHand.Count, false);
        }
    }
}
