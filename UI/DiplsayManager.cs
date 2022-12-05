using BlackJack.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Interfaces;

namespace BlackJack.UI
{
    public class DiplsayManager
    {
        private int playerVerticalDrawOffset;
        private int dealerVerticalDrawOffset;
        private int windowHeight;
        private int windowWidth;
        private int gameHeight;
        private int gameWidth;

        public DiplsayManager()
        {
            this.windowHeight = 45;
            this.windowWidth = 136;
            this.playerVerticalDrawOffset = 26;
            this.dealerVerticalDrawOffset = 6;
            gameHeight = playerVerticalDrawOffset + 14;
            gameWidth = 70;
            Console.SetWindowSize(windowWidth, windowHeight);
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void DrawAnnotations()
        {
            AnnotationDrawer annotationDrawer = new AnnotationDrawer(dealerVerticalDrawOffset, playerVerticalDrawOffset);
            annotationDrawer.Draw();
            DrawRules();
        }
        public void DrawPlayerCards(Card card, int currentCardCount, bool isSplit)
        {
            HandDrawer handDrawer;
            CardImageGenerator cardImageGenerator = new CardImageGenerator(card);
            cardImageGenerator.Draw();
            char[,] cardImage = cardImageGenerator.Image;
            handDrawer = new HandDrawer(playerVerticalDrawOffset, cardImage, currentCardCount, isSplit);
            handDrawer.Draw();
        }
        public void DrawDealerCards(Card card, int currentCardCount, bool hasRevealed)
        {
            HandDrawer handDrawer;
            ICardDrawer cardImageGenerator;
            if (currentCardCount == 1 && !hasRevealed)
            {
                cardImageGenerator = new CardImageGeneratorEmpty();
            }
            else
            {
                cardImageGenerator = new CardImageGenerator(card);
            }
            cardImageGenerator.Draw();
            char[,] cardImage = cardImageGenerator.Image;
            handDrawer = new HandDrawer(dealerVerticalDrawOffset, cardImage, currentCardCount, false);
            handDrawer.Draw();
        }
        internal void DrawInitialScreen(int chips)
        {
            DrawRules();
            string[] text = { "B  L  A  C  K  J  A  C  K " };
            DrawTextTop(text);
            text = new[] { "Your current credits: " + chips, "Place your bet: " };
            DrawTextCenter(text);
        }
        public void DrawRules()
        {
            string[] text;
            text = new[]
            {
                "BLACKJACK",
                "The aim of this game is to get a hand, which value "+
                "is higher than the dealer's, however not surpassing 21. "+
                "A deck of 52 cards is used for playing this game.",
                "RULES",
                "At the beginning of a round, both player and the dealer "+
                "get dealt two cards. One of the dealer's cards remain "+
                "hidden. Aces <A> count as either 1 or 11. If player's "+
                "initial hand value is 9-11, he can choose to "+
                "\"double\" to double the bet. Then, if player wants, "+
                "he/her can make a \"hit\" to get an additional card. If "+
                "player's hand value at any point of the game exceeds 21 - "+
                "player looses. Player can choose to \"stand\", to keep his/her "+
                "hand. After choosing to stand, dealer draws additional cards until his hand "+
                "value is greater than 16. Additionally, if player's initial hand consists of "+
                "cards of the same value, player can choose to \"split\", which "+
                "splits his hand into two hands which player can play separately. "+
                "Player then has to place the same initial bet on his/her split hand.",
                "HOUSE RULES",
                "* Player cannot split more than once. ",
                "* Player starts with 100 chips. ",
                "CONTROLS",
                "<UpArrow> - HIT",
                "<DownArrow> - STAND",
                "<RightArrow> - DOUBLE",
                "<SPACEBAR> - SPLIT",
                "<Q> - QUIT GAME"
            };
            DrawTextRight(text);
        }
        public void CleanTable(string property)
        {
            int cleanableWidth = gameWidth;
            int cleanableHeight=15;
            int leftPosition=0;
            int verticalPosition;
            if (property == "player")
            {
                verticalPosition = playerVerticalDrawOffset;
            }
            else if (property == "dealer")
            {
                verticalPosition = dealerVerticalDrawOffset;
            }
            else
            {
                return;
            }
            TableCleaner table = new TableCleaner(verticalPosition, cleanableWidth, cleanableHeight, leftPosition);
            table.Clean();
        }
        public void DrawTextBottom(string[] text)
        {
            int leftPosition = gameWidth / 3;
            int verticalPosition = gameHeight + 1;
            int drawableWidth = gameWidth;
            TextDrawer textDrawer = new TextDrawer(verticalPosition, leftPosition, drawableWidth, text);
            textDrawer.Draw();
        }
        public void DrawTextTop(string[] text)
        {
            int leftPosition = gameWidth / 3;
            int verticalPosition = 9;
            int drawableWidth = gameWidth;
            TextDrawer textDrawer = new TextDrawer(verticalPosition, leftPosition, drawableWidth, text);
            textDrawer.Draw();
        }
        public void DrawTextCenter(string[] text)
        {
            int leftPosition = gameWidth / 3;
            int verticalPosition = playerVerticalDrawOffset-2;
            int drawableWidth = gameWidth;
            TextDrawer textDrawer = new TextDrawer(verticalPosition, leftPosition, drawableWidth, text);
            textDrawer.Draw();
        }
        public void DrawTextRight(string[] text)
        {
            int leftPosition = gameWidth + 3;
            int verticalPosition = 2;
            int drawableWidth = windowWidth;
            TextDrawer textDrawer = new TextDrawer(verticalPosition, leftPosition, drawableWidth, text);
            textDrawer.Draw();
        }
        public void UpdateDealerValue(int handValue, bool hasRevealed)
        {
            ValueDrawerDealer valueDrawerDealer = new ValueDrawerDealer(dealerVerticalDrawOffset, handValue, hasRevealed);
            valueDrawerDealer.Draw();
        }
        public void UpdatePlayerValue(int handValue)
        {
            ValueDrawerPlayer valueDrawerPlayer = new ValueDrawerPlayer(playerVerticalDrawOffset, handValue);
            valueDrawerPlayer.Draw();
        }
    }
}
