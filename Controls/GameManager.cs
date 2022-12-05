using BlackJack.DataStructures;
using BlackJack.UI;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BlackJack.Controls
{
    public class GameManager
    {
        private int betSize;
        private int betSizeSplit;
        private Deck cardDeck;
        private Card card;
        private Player currentPlayer;
        private Dealer cardDealer;
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
        private static GameManager _instance;

        private GameManager()
        {
            betSize = 0;
            betSizeSplit = 0;
            IsPlayingRound = true;
            isPlaying = true;
            hasDoubled = false;
            canSplit = false;
            canDouble = false;
            splitValue = 0;
            isSecondCheck = false;
        }
        public static GameManager GetGameManager
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }
        public int BetSize 
        { get => betSize; set => betSize = value; }
        public bool IsPlaying 
        { get => isPlaying; set => isPlaying = value; }
        public bool IsPlayingRound 
        { get => isPlayingRound; set => isPlayingRound = value; }
        public int BetSizeSplit 
        { get => betSizeSplit; set => betSizeSplit = value; }
        public DiplsayManager DisplayManager
        {
            get => displayManager;
            set => displayManager = value;
        }
        public Deck CardDeck
        {
            get => cardDeck;
            set => cardDeck = value;
        }
        public Player CurrentPlayer
        {
            get => currentPlayer;
            set => currentPlayer = value;
        }
        public Dealer CardDealer
        {
            get => cardDealer;
            set => cardDealer = value;
        }
        public int InitialBalance
        {
            get => initialBalance;
            set => initialBalance = value;
        }

        internal void Action(string action)
        {
            if (action == "hit")
            {
                ActionHit(true);
            }
            if (action == "stand")
            {
                if (CurrentPlayer.IsSplit)
                {
                    ActionEndSplit();
                }
                else
                {
                    CardDealer.HasRevealed = true;
                    ActionStand(CardDeck);
                    EndGameCheck();
                }

            }
            if (action == "double")
            {
                if (canDouble && !hasDoubled && CurrentPlayer.Chips >= BetSize)
                {
                    ActionDouble();
                }
                else
                {
                    string[] text = { "YOU ARE NOT ABLE TO DOUBLE" };
                    DisplayManager.DrawTextBottom(text);
                }
            }
            if (action == "split")
            {
                if (canSplit && CurrentPlayer.Chips >= BetSize && !hasDoubled)
                {
                    canSplit = false;
                    UpdateChips(BetSize);
                    string[] text = { "Your bet: " + (betSize + betSizeSplit) };
                    DisplayManager.DrawTextCenter(text);
                    ActionSplit();
                }
                else
                {
                    string[] text = { "YOU ARE NOT ABLE TO SPLIT" };
                    DisplayManager.DrawTextBottom(text);
                }
            }
            if (action == "quit" || action == "end")
            {
                ActionExit();
            }
            if (action == "continue")
            {
                ResetPlayer();
            }
        }
        public void ActionDouble()
        {
            UpdateChips(BetSize);
            string[] text = { "Your bet: " + betSize };
            DisplayManager.DrawTextCenter(text);
            hasDoubled = true;
        }
        public void ActionHit(bool isPlayer)
        {
            if (isPlayer)
            {
                card = CardDeck.GetTopCard();
                if (CurrentPlayer.IsSplit)
                {
                    CurrentPlayer.IncreaseSplitHand(card);
                    DisplayManager.DrawPlayerCards(card, CurrentPlayer.SplitHand.Count, true);
                }
                else
                {
                    CurrentPlayer.IncreaseHand(card);
                    DisplayManager.DrawPlayerCards(card, CurrentPlayer.Hand.Count, false);
                }
                DisplayManager.UpdatePlayerValue(CurrentPlayer.HandValue);
            }
            else
            {
                card = CardDeck.GetTopCard();
                CardDealer.IncreaseHand(card);
                DisplayManager.DrawDealerCards(card, CardDealer.Hand.Count, CardDealer.HasRevealed);
                DisplayManager.UpdateDealerValue(CardDealer.HandValue, CardDealer.HasRevealed);
            }

        }
        public void ActionStand(Deck deck)
        {
            RedrawDealerHand();
            CardDealer.HandValue += CardDealer.HiddenValue;
            DisplayManager.UpdateDealerValue(CardDealer.HandValue, CardDealer.HasRevealed);
            while (CardDealer.HandValue < 17)
            {
                ActionHit(false);
            }
        }
        public void ActionSplit()
        {
            usedSplit = true;
            CurrentPlayer.IsSplit = true;
            DisplayManager.CleanTable("player");
            CurrentPlayer.SplitHand.Push(CurrentPlayer.Hand.Pop());
            CurrentPlayer.HandValue = CurrentPlayer.SplitHand.Peek().Value;
            Card card = CurrentPlayer.SplitHand.Peek();
            DisplayManager.DrawPlayerCards(card, CurrentPlayer.SplitHand.Count, true);
            ActionHit(true);
            card = CurrentPlayer.Hand.Peek();
            DisplayManager.DrawPlayerCards(card, CurrentPlayer.Hand.Count, false);
        }
        private void ActionEndSplit()
        {
            CurrentPlayer.IsSplit = false;
            splitValue = CurrentPlayer.HandValue;
            CurrentPlayer.HandValue = CurrentPlayer.Hand.Peek().Value;
            ActionHit(true);
        }
        public void ActionExit()
        {
            IsPlaying = false;
        }
        private void RedrawDealerHand()
        {
            DisplayManager.CleanTable("dealer");
            Card topCard = CardDealer.Hand.Pop();
            DisplayManager.DrawDealerCards(CardDealer.Hand.Peek(), CardDealer.Hand.Count, CardDealer.HasRevealed);
            CardDealer.Hand.Push(topCard);
            DisplayManager.DrawDealerCards(CardDealer.Hand.Peek(), CardDealer.Hand.Count, CardDealer.HasRevealed);
        }
        public void EndGameCheck()
        {
            bool usingSplitCheck = usedSplit && CardDealer.HasRevealed && isSecondCheck;
            string[] text = { "" };
            bool isBust = CurrentPlayer.HandValue > 21;
            bool hasWon = CurrentPlayer.HandValue > CardDealer.HandValue;
            bool hasWonBust = CardDealer.HandValue > 21;
            bool isDraw = CurrentPlayer.HandValue == CardDealer.HandValue;
            bool hasLost = CurrentPlayer.HandValue < CardDealer.HandValue;
            bool hasLostBlackJack = CardDealer.HandValue == 21 && CurrentPlayer.HandValue != CardDealer.HandValue && CardDealer.Hand.Count == 2;
            bool hasWonBlackJack = CurrentPlayer.HandValue == 21 && CurrentPlayer.HandValue != CardDealer.HandValue && CurrentPlayer.Hand.Count == 2;
            bool isDrawBlackJack = CurrentPlayer.HandValue == 21 && CurrentPlayer.HandValue == CardDealer.HandValue;
            if (isBust)
            {
                if (CurrentPlayer.IsSplit)
                {
                    text = new[] { "Your split hand has busted." };
                    DisplayManager.DrawTextCenter(text);
                    ActionEndSplit();
                }
                else
                {
                    if (usedSplit)
                    {
                        text = new[] { "" };
                        EndRound();
                    }
                    else
                    {
                        text = new[] { "It's a bust. Your hand value: " + CurrentPlayer.HandValue };
                        EndRound();
                        DisplayManager.DrawTextCenter(text);
                        return;
                    }
                }
            }
            else if (CardDealer.HasRevealed)
            {
                if (hasWonBust)
                {
                    text = new[] { "You've won! Your hand value: " + CurrentPlayer.HandValue, "Dealer has busted: " + CardDealer.HandValue };
                    AwardPlayer();
                    EndRound();
                }
                else if (isDraw)
                {
                    text = new[] { "It's a draw. Your hand value: " + CurrentPlayer.HandValue, "Dealer's hand value: " + CardDealer.HandValue };
                    UpdateChips(-BetSize);
                    EndRound();
                }
                else if (hasLost)
                {
                    text = new[] { "You've lost. Your hand value: " + CurrentPlayer.HandValue, "Dealer's hand value: " + CardDealer.HandValue };
                    EndRound();
                }
                else if (hasLostBlackJack)
                {
                    text = new[] { "You've lost. Your hand value: " + CurrentPlayer.HandValue, "Dealer has a BlackJack: " + CardDealer.HandValue };
                    EndRound();
                }
                else if (hasWonBlackJack)
                {
                    text = new[] { "You've won! It's a BlackJack: " + CurrentPlayer.HandValue, "Dealer's hand value: " + CardDealer.HandValue };
                    UpdateChips(BetSize / 2);
                    AwardPlayer();
                    EndRound();
                }
                else if (isDrawBlackJack)
                {
                    text = new[] { "It's a draw! you've got a BlackJack:" + CurrentPlayer.HandValue, "Dealer has a BlackJack too:" + CardDealer.HandValue };
                    UpdateChips(-BetSize);
                    EndRound();
                }
                else if (hasWon)
                {
                    text = new[] { "You've won! Your hand value: " + CurrentPlayer.HandValue, "Dealer's hand value: " + CardDealer.HandValue };
                    AwardPlayer();
                    EndRound();
                }
                if (!IsPlayingRound && !CurrentPlayer.IsSplit && !isSecondCheck)
                {
                    DisplayManager.DrawTextCenter(text);
                }
            }
            if (!IsPlayingRound && isSecondCheck)
            {
                text = new[] { "This round chip balance: " + (CurrentPlayer.Chips - InitialBalance) + "       " };
                DisplayManager.DrawTextCenter(text);
            }
            if (usedSplit && CardDealer.HasRevealed && !isSecondCheck)
            {
                CurrentPlayer.HandValue = splitValue;
                isSecondCheck = true;
                EndGameCheck();
            }
        }
        public void EndRound()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            IsPlayingRound = false;
            string[] text = { ">> Do you want to play another round? Y/N <<" };
            DisplayManager.DrawTextBottom(text);
            _instance = null;
        }
        public void ResetPlayer()
        {
            CurrentPlayer.Hand = new Stack<Card>();
            CurrentPlayer.SplitHand = new Stack<Card>();
            CurrentPlayer.IsSplit = false;
            CurrentPlayer.HandValue = 0;
            CurrentPlayer.AceCount = 0;
        }
        public void UpdateChips(int betAmount)
        {

            if (betAmount > CurrentPlayer.Chips)
            {
                BetSize = CurrentPlayer.Chips;
                CurrentPlayer.Chips -= BetSize;
            }
            else
            {
                if (CurrentPlayer.IsSplit)
                {
                    BetSizeSplit += betAmount;
                    CurrentPlayer.Chips -= betAmount;
                }
                else
                {
                    BetSize += betAmount;
                    CurrentPlayer.Chips -= betAmount;
                }
            }
        }
        public void GameOver()
        {
            string[] text = { "   G  A  M  E    O  V  E  R   ", "You've lost all of your chips", ">> Press any key to Exit <<" };
            DisplayManager.DrawTextCenter(text);
            CurrentPlayer.Chips = 100;
            EndRound();
        }
        internal void AwardPlayer()
        {
            if (CurrentPlayer.IsSplit)
            {
                CurrentPlayer.Chips += BetSizeSplit * 2;
            }
            else
            {
                CurrentPlayer.Chips += BetSize * 2;
            }

        }
        public void DealFirstHand()
        {
            ActionHit(true);
            ActionHit(true);
            ActionHit(false);
            ActionHit(false);
            CheckSplit();
            CheckDouble();
            string[] text = { "Your bet: " + betSize };
            DisplayManager.DrawTextCenter(text);
        }
        public void CheckSplit()
        {
            Card[] cards = CurrentPlayer.Hand.ToArray();
            if (cards[0].Value == cards[1].Value)
            {
                canSplit = true;
            }
        }
        public void CheckDouble()
        {
            Card[] cards = CurrentPlayer.Hand.ToArray();
            bool canDouble = cards[0].Value + cards[1].Value >= 9 && cards[0].Value + cards[1].Value <= 11;
            if (canDouble)
            {
                this.canDouble = true;
            }
        }
    }
}
