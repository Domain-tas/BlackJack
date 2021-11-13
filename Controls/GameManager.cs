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
        private bool canSplit;

        public GameManager(Deck deck, Player player, Dealer dealer, DiplsayManager displayManager)
        {
            this.deck = deck;
            this.player = player;
            betSize = 0;
            this.dealer = dealer;
            IsPlayingRound = true;
            isPlaying = true;
            this.displayManager = displayManager;
            usingSplit = false;
            canSplit = false;
        }
        public int BetSize { get => betSize; set => betSize = value; }
        public bool IsPlaying { get => isPlaying; set => isPlaying = value; }
        public bool IsPlayingRound { get => isPlayingRound; set => isPlayingRound = value; }

        internal void Action(string action)
        {
            if (action == "hit")
            {
                Deal(true);
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
                if (canSplit)
                {
                    canSplit = false;
                    ManageSplit();
                }
                else
                {
                    string[] text = { "YOU ARE NOT ABLE TO SPLIT" };
                    displayManager.DrawText(text);
                }
                
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
                Deal(false);
            }
        }

        public void EndGameCheck()
        {
            string[] text;
            text = new string[] {"ERROR"};
            bool isBust = player.HandValue > 21;
            bool hasWon = dealer.HasRevealed && player.HandValue > dealer.HandValue;
            bool hasWonBust = dealer.HasRevealed && dealer.HandValue > 21;
            bool isDraw = dealer.HasRevealed && player.HandValue == dealer.HandValue;
            bool hasLost = dealer.HasRevealed && player.HandValue < dealer.HandValue;
            bool hasLostBlackJack = !dealer.HasRevealed && dealer.HandValue == 21 && player.HandValue != dealer.HandValue;
            bool hasWonBlackJack = !dealer.HasRevealed && player.HandValue == 21 && player.HandValue != dealer.HandValue;
            bool isDrawBlackJack = !dealer.HasRevealed && player.HandValue == 21 && player.HandValue == dealer.HandValue;
            if (isBust)
            {
                //displayManager.CleanTable();
                text = new []{"It's a bust. Your hand value: " + player.HandValue};
                EndRound();
            }
            if (hasWon)
            {
                text = new[]{ "You've won! Your hand value: " + player.HandValue, "Dealer's hand value: " + dealer.HandValue };
                AwardPlayer();
                EndRound();
            }
            if (hasWonBust)
            {
                text = new [] { "You've won! Your hand value: " + player.HandValue, "Dealer has busted: " + dealer.HandValue };
                AwardPlayer();
                EndRound();
            }
            if (isDraw)
            {
                text = new [] { "It's a draw. Your hand value: " + player.HandValue, "Dealer's hand value: " + dealer.HandValue };
                UpdateChips(-BetSize);
                EndRound();
            }
            if (hasLost)
            {
                text = new [] { "You've lost. Your hand value: " + player.HandValue, "Dealer's hand value: " + dealer.HandValue };
                UpdateChips(-BetSize);
                EndRound();
            }
            if (hasLostBlackJack)
            {
                text = new [] { "You've lost. Your hand value: " + player.HandValue, "Dealer has a BlackJack: " + dealer.HandValue };
                EndRound();
            }
            if (hasWonBlackJack)
            {
                text = new [] { "You've won! It's a BlackJack: " + player.HandValue, "Dealer's hand value: " + dealer.HandValue };
                UpdateChips(BetSize / 2);
                AwardPlayer();
                EndRound();
            }
            if (isDrawBlackJack)
            {
                text = new [] { "It's a draw! you've got a BlackJack:" + player.HandValue, "Dealer has a BlackJack too:" + dealer.HandValue };
                UpdateChips(-BetSize);
                EndRound();
            }

            if (!IsPlayingRound)
            {
                displayManager.DrawText(text);
            }
            //Console.WriteLine("Error");
        }

        public void EndRound()
        {
            IsPlayingRound = false;
            string[] text = {"Do you want to play another round? Y/N"};
            displayManager.DrawText(text);
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
            Deal(true);
            Deal(true);
            Deal(false);
            Deal(false);
            CheckSplit();
        }

        public void Deal(bool isPlayer)
        {
            if (isPlayer)
            {
                card = deck.GetTopCard();
                player.IncreaseHand(card);
                displayManager.DrawCards(card, "player", player.Hand.Count, player.IsSplit);
                displayManager.DrawHandValue(isPlayer, player.HandValue);
            }
            else
            {
                card = deck.GetTopCard();
                dealer.IncreaseHand(card);
                displayManager.DrawCards(card, "dealer", dealer.Hand.Count, player.IsSplit);
                displayManager.DrawHandValue(isPlayer, dealer.HandValue);
            }

        }

        public void CheckSplit()
        {
            Card[] cards = player.Hand.ToArray();
            if (cards[0].Value == cards[1].Value)
            {
                canSplit = true;
                string[] text = { "YOU ARE ABLE TO SPLIT" };
                displayManager.DrawText(text);
            }
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
