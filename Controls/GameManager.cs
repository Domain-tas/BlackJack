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
        private bool usedSplit;
        private bool canSplit;
        private bool hasDoubled;
        private bool canDouble;

        public GameManager(Deck deck, Player player, Dealer dealer, DiplsayManager displayManager)
        {
            this.deck = deck;
            this.player = player;
            betSize = 0;
            this.dealer = dealer;
            IsPlayingRound = true;
            isPlaying = true;
            this.displayManager = displayManager;
            hasDoubled = false;
            canSplit = false;
            canDouble = false;
        }
        public int BetSize { get => betSize; set => betSize = value; }
        public bool IsPlaying { get => isPlaying; set => isPlaying = value; }
        public bool IsPlayingRound { get => isPlayingRound; set => isPlayingRound = value; }

        internal void Action(string action)
        {
            if (action == "hit")
            {
                Deal(true);
            }
            if (action == "stand")
            {
                if (player.IsSplit)
                {
                    EndSplit();
                }
                else
                {
                    dealer.HasRevealed = true;
                    DealerStand(deck);
                    EndGameCheck();
                }

            }
            if (action == "double")
            {
                if (canDouble)
                {
                    UpdateChips(BetSize);
                    string[] text = { "Your bet: " + betSize };
                    displayManager.DrawTextCenter(text);
                }
                else
                {
                    string[] text = { "YOU ARE NOT ABLE TO DOUBLE" };
                    displayManager.DrawTextCenter(text);
                }

            }
            if (action == "split")
            {
                if (canSplit && player.Chips >= BetSize && !hasDoubled)
                {
                    canSplit = false;
                    ManageSplit();
                }
                else
                {
                    string[] text = { "YOU ARE NOT ABLE TO SPLIT" };
                    displayManager.DrawTextBottom(text);
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
            RedrawDealerHand();
            dealer.HandValue += dealer.HiddenValue;
            while (dealer.HandValue <= 17)
            {
                Deal(false);
            }
        }

        private void RedrawDealerHand()
        {
            displayManager.CleanTable("dealer");
            Card topCard = dealer.Hand.Pop();
            displayManager.DrawDealerCards(dealer.Hand.Peek(), dealer.Hand.Count);
            dealer.Hand.Push(topCard);
            displayManager.DrawDealerCards(dealer.Hand.Peek(), dealer.Hand.Count);
        }
        private void DrawSplit()
        {
            displayManager.CleanTable("player");
            Card topCard = dealer.Hand.Pop();
            displayManager.DrawDealerCards(dealer.Hand.Peek(), dealer.Hand.Count);
            dealer.Hand.Push(topCard);
            displayManager.DrawDealerCards(dealer.Hand.Peek(), dealer.Hand.Count);
        }

        public void EndGameCheck()
        {
            string[] text;
            text = new[] { "ERROR" };
            bool isBust = player.HandValue > 21;
            bool hasWon = player.HandValue > dealer.HandValue;
            bool hasWonBust = dealer.HandValue > 21;
            bool isDraw = player.HandValue == dealer.HandValue;
            bool hasLost = player.HandValue < dealer.HandValue;
            bool hasLostBlackJack = dealer.HandValue == 21 && player.HandValue != dealer.HandValue;
            bool hasWonBlackJack = player.HandValue == 21 && player.HandValue != dealer.HandValue;
            bool isDrawBlackJack = player.HandValue == 21 && player.HandValue == dealer.HandValue;
            if (isBust)
            {
                text = new[] { "It's a bust. Your hand value: " + player.HandValue };
                if (player.IsSplit)
                {
                    text = new[] { "Your split hand busted. Your hand value: " + player.HandValue };
                    EndSplit();
                }
                else
                {
                    EndRound();
                }

            }
            if (dealer.HasRevealed)
            {
                if (hasWon)
                {
                    text = new[] { "You've won! Your hand value: " + player.HandValue, "Dealer's hand value: " + dealer.HandValue };
                    AwardPlayer();
                    EndRound();
                }
                else if (hasWonBust)
                {
                    text = new[] { "You've won! Your hand value: " + player.HandValue, "Dealer has busted: " + dealer.HandValue };
                    AwardPlayer();
                    EndRound();
                }
                else if (isDraw)
                {
                    text = new[] { "It's a draw. Your hand value: " + player.HandValue, "Dealer's hand value: " + dealer.HandValue };
                    UpdateChips(-BetSize);
                    EndRound();
                }
                else if (hasLost)
                {
                    text = new[] { "You've lost. Your hand value: " + player.HandValue, "Dealer's hand value: " + dealer.HandValue };
                    UpdateChips(-BetSize);
                    EndRound();
                }
                else if (hasLostBlackJack)
                {
                    text = new[] { "You've lost. Your hand value: " + player.HandValue, "Dealer has a BlackJack: " + dealer.HandValue };
                    EndRound();
                }
                else if (hasWonBlackJack)
                {
                    text = new[] { "You've won! It's a BlackJack: " + player.HandValue, "Dealer's hand value: " + dealer.HandValue };
                    UpdateChips(BetSize / 2);
                    AwardPlayer();
                    EndRound();
                }
                else if (isDrawBlackJack)
                {
                    text = new[] { "It's a draw! you've got a BlackJack:" + player.HandValue, "Dealer has a BlackJack too:" + dealer.HandValue };
                    UpdateChips(-BetSize);
                    EndRound();
                }
            }
            if (!IsPlayingRound)
            {
                displayManager.DrawTextBottom(text);
            }
        }

        private void EndSplit()
        {
            player.IsSplit = false;
            player.HandValue = player.Hand.Peek().Value;
        }

        public void EndRound()
        {
            IsPlayingRound = false;
            string[] text = { ">> Do you want to play another round? Y/N <<" };
            Console.Beep();
            displayManager.DrawTextCenter(text);
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
            CheckDouble();
            string[] text = { "Your bet: " + betSize };
            displayManager.DrawTextCenter(text);
        }

        public void Deal(bool isPlayer)
        {
            if (isPlayer)
            {
                card = deck.GetTopCard();
                if (player.IsSplit)
                {
                    player.IncreaseSplitHand(card);
                    displayManager.DrawPlayerCards(card, player.SplitHand.Count, player.IsSplit);
                }
                else
                {
                    player.IncreaseHand(card);
                    displayManager.DrawPlayerCards(card, player.Hand.Count, player.IsSplit);
                }
                displayManager.UpdatePlayerValue(player.HandValue);
            }
            else
            {
                card = deck.GetTopCard();
                dealer.IncreaseHand(card);
                displayManager.DrawDealerCards(card, dealer.Hand.Count);
                displayManager.UpdateDealerValue(dealer.HandValue);
            }

        }

        public void CheckSplit()
        {
            Card[] cards = player.Hand.ToArray();
            if (cards[0].Value == cards[1].Value)
            {
                canSplit = true;
                string[] text = { "YOU ARE ABLE TO SPLIT >>SPACEBAR<<" };
                displayManager.DrawTextBottom(text);
            }
        }
        public void CheckDouble()
        {
            Card[] cards = player.Hand.ToArray();
            bool canDouble = cards[0].Value + cards[1].Value >= 9 && cards[0].Value + cards[1].Value <= 11;
            if (canDouble)
            {
                this.canDouble = true;
                //string[] text = { "YOU ARE ABLE TO DOUBLE >>RightArrow<<" };
                //displayManager.DrawTextCenter(text);
            }
        }

        public void ManageSplit()
        {
            usedSplit = true;
            player.IsSplit = true;
            //displayManager.CleanTable("player");
            player.SplitHand.Push(player.Hand.Pop());
            player.HandValue = player.SplitHand.Peek().Value;
            Card card = player.SplitHand.Peek();
            displayManager.DrawPlayerCards(card, player.SplitHand.Count, true);
            card = player.Hand.Peek();
            displayManager.DrawPlayerCards(card, player.Hand.Count, false);
        }
    }
}
