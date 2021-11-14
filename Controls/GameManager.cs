using BlackJack.DataStructures;
using BlackJack.UI;
using System;
using System.Collections.Generic;

namespace BlackJack.Controls
{
    class GameManager
    {
        private int betSize;
        private int betSizeSplit;
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
        private int splitValue;
        private bool isSecondCheck;
        private int initialBalance;

        public GameManager(Deck deck, Player player, Dealer dealer, DiplsayManager displayManager)
        {
            this.deck = deck;
            this.player = player;
            betSize = 0;
            betSizeSplit = 0;
            this.dealer = dealer;
            IsPlayingRound = true;
            isPlaying = true;
            this.displayManager = displayManager;
            hasDoubled = false;
            canSplit = false;
            canDouble = false;
            splitValue = 0;
            isSecondCheck = false;
            initialBalance = player.Chips;
        }
        public int BetSize { get => betSize; set => betSize = value; }
        public bool IsPlaying { get => isPlaying; set => isPlaying = value; }
        public bool IsPlayingRound { get => isPlayingRound; set => isPlayingRound = value; }
        public int BetSizeSplit { get => betSizeSplit; set => betSizeSplit = value; }

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
                if (canDouble&&!hasDoubled&& player.Chips >= BetSize)
                {
                    UpdateChips(BetSize);
                    string[] text = { "Your bet: " + betSize };
                    displayManager.DrawTextCenter(text);
                    hasDoubled = true;
                }
                else
                {
                    string[] text = { "YOU ARE NOT ABLE TO DOUBLE" };
                    displayManager.DrawTextBottom(text);
                }
            }
            if (action == "split")
            {
                if (canSplit && player.Chips >= BetSize && !hasDoubled)
                {
                    canSplit = false;
                    UpdateChips(BetSize);
                    ManageSplit();
                }
                else
                {
                    string[] text = { "YOU ARE NOT ABLE TO SPLIT" };
                    displayManager.DrawTextBottom(text);
                }
            }
            if (action == "quit" || action == "end")
            {
                EndApplication();
            }
            if (action == "continue")
            {
                ResetPlayer();
            }
        }

        public void DealerStand(Deck deck)
        {
            RedrawDealerHand();
            dealer.HandValue += dealer.HiddenValue;
            while (dealer.HandValue < 17)
            {
                Deal(false);
            }
        }
        private void RedrawDealerHand()
        {
            displayManager.CleanTable("dealer");
            Card topCard = dealer.Hand.Pop();
            displayManager.DrawDealerCards(dealer.Hand.Peek(), dealer.Hand.Count, dealer.HasRevealed);
            dealer.Hand.Push(topCard);
            displayManager.DrawDealerCards(dealer.Hand.Peek(), dealer.Hand.Count, dealer.HasRevealed);
        }
        public void EndGameCheck()
        {
            bool usingSplitCheck = usedSplit && dealer.HasRevealed&&isSecondCheck;
            string[] text = { "" };
            bool isBust = player.HandValue > 21;
            bool hasWon = player.HandValue > dealer.HandValue;
            bool hasWonBust = dealer.HandValue > 21;
            bool isDraw = player.HandValue == dealer.HandValue;
            bool hasLost = player.HandValue < dealer.HandValue;
            bool hasLostBlackJack = dealer.HandValue == 21 && player.HandValue != dealer.HandValue && dealer.Hand.Count==2;
            bool hasWonBlackJack = player.HandValue == 21 && player.HandValue != dealer.HandValue && player.Hand.Count==2;
            bool isDrawBlackJack = player.HandValue == 21 && player.HandValue == dealer.HandValue;
            if (isBust)
            {
                if (player.IsSplit)
                {
                    text = new[] { "Your split hand has busted."};
                    EndSplit();
                }
                else
                {

                    if (usedSplit)
                    {
                        text = new[] {""};
                        EndRound();
                    }
                    else
                    {
                        text = new[] { "It's a bust. Your hand value: " + player.HandValue };
                        EndRound();
                        displayManager.DrawTextCenter(text);
                        return;
                    }
                    
                }
                
                return;
            }
            if (dealer.HasRevealed)
            {
                if (hasWonBust)
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
                    UpdateChips(BetSize /2);
                    AwardPlayer();
                    EndRound();
                    hasWon = false;
                }
                else if (isDrawBlackJack)
                {
                    text = new[] { "It's a draw! you've got a BlackJack:" + player.HandValue, "Dealer has a BlackJack too:" + dealer.HandValue };
                    UpdateChips(-BetSize);
                    EndRound();
                }
                if (hasWon)
                {
                    text = new[] { "You've won! Your hand value: " + player.HandValue, "Dealer's hand value: " + dealer.HandValue };
                    AwardPlayer();
                    EndRound();
                }
                if (!IsPlayingRound&&!player.IsSplit && !isSecondCheck)
                {
                    displayManager.DrawTextCenter(text);
                }
            }
            if (!IsPlayingRound && isSecondCheck)
            {
                text = new[] { "This round chip balance: "+ (player.Chips- initialBalance)+"       "};
                displayManager.DrawTextCenter(text);
            }
            if (usedSplit && dealer.HasRevealed && !isSecondCheck)
            {
                player.HandValue = splitValue;
                isSecondCheck = true;
                EndGameCheck();
            }
        }
        private void EndSplit()
        {
            player.IsSplit = false;
            splitValue = player.HandValue;
            player.HandValue = player.Hand.Peek().Value;
            Deal(true);
        }

        public void EndRound()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            IsPlayingRound = false;
            string[] text = { ">> Do you want to play another round? Y/N <<" };
            displayManager.DrawTextBottom(text);
        }

        public void EndApplication()
        {
            IsPlaying = false;
        }

        private void ResetPlayer()
        {
            player.Hand = new Stack<Card>();
            player.SplitHand = new Stack<Card>();
            player.IsSplit = false;
            player.HandValue = 0;
            player.AceCount = 0;
        }

        internal void UpdateChips(int betAmount)
        {

            if (betAmount > player.Chips)
            {
                BetSize = player.Chips;
                player.Chips -= BetSize;
            }
            else
            {
                if (player.IsSplit)
                {
                    BetSizeSplit += betAmount;
                    player.Chips -= betAmount;
                }
                else
                {
                    BetSize += betAmount;
                    player.Chips -= betAmount;
                }
            }
        }

        public void GameOver()
        {
            string[] text = { "   G A M E  O V E R   ", "You've lost all of your chips", ">> Press any key to Exit <<" };
            displayManager.DrawTextCenter(text);
            player.Chips = 100;
            EndRound();
        }

        internal void AwardPlayer()
        {
            if (player.IsSplit)
            {
                player.Chips += BetSizeSplit * 2;
            }
            else
            {
                player.Chips += BetSize * 2;
            }

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
                    displayManager.DrawPlayerCards(card, player.SplitHand.Count, true);
                }
                else
                {
                    player.IncreaseHand(card);
                    displayManager.DrawPlayerCards(card, player.Hand.Count, false);
                }
                displayManager.UpdatePlayerValue(player.HandValue);
            }
            else
            {
                card = deck.GetTopCard();
                dealer.IncreaseHand(card);
                displayManager.DrawDealerCards(card, dealer.Hand.Count, dealer.HasRevealed);
                displayManager.UpdateDealerValue(dealer.HandValue, dealer.HasRevealed);
            }

        }

        public void CheckSplit()
        {
            Card[] cards = player.Hand.ToArray();
            if (cards[0].Value == cards[1].Value)
            {
                canSplit = true;
            }
        }
        public void CheckDouble()
        {
            Card[] cards = player.Hand.ToArray();
            bool canDouble = cards[0].Value + cards[1].Value >= 9 && cards[0].Value + cards[1].Value <= 11;
            if (canDouble)
            {
                this.canDouble = true;
            }
        }

        public void ManageSplit()
        {
            usedSplit = true;
            player.IsSplit = true;
            displayManager.CleanTable("player");
            player.SplitHand.Push(player.Hand.Pop());
            player.HandValue = player.SplitHand.Peek().Value;
            Card card = player.SplitHand.Peek();
            displayManager.DrawPlayerCards(card, player.SplitHand.Count, true);
            Deal(true);
            card = player.Hand.Peek();
            displayManager.DrawPlayerCards(card, player.Hand.Count, false);
        }
    }
}
