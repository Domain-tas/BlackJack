using BlackJack.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.UI
{
    class DiplsayManager
    {
        private int playerVerticalDrawOffset;
        private int dealerVerticalDrawOffset;
        private int windowHeight;
        private int windowWidth;
        private int gameHeight;
        private int gameWidth;

        public DiplsayManager()
        {
            this.windowHeight = 52;
            this.windowWidth = 136;
            gameHeight = 40;
            gameWidth = 60;
            this.playerVerticalDrawOffset = 26;
            this.dealerVerticalDrawOffset = 6;
            Console.SetWindowSize(windowWidth, windowHeight);
            Console.BackgroundColor = ConsoleColor.DarkGreen;
        }


        public void DrawAnnotations()
        {
            AnnotationDrawer annotationDrawer = new AnnotationDrawer(dealerVerticalDrawOffset, playerVerticalDrawOffset);
            annotationDrawer.Draw();
        }
        public void DrawCards(Card card, string participant, int currentCardCount, bool isSplit)
        {
            HandDrawer handDrawer;
            CardImageGenerator cardImageGenerator = new CardImageGenerator(card);
            cardImageGenerator.Draw();
            char[,] cardImage = cardImageGenerator.Image;
            if (participant == "player")
            {
                handDrawer = new HandDrawer(playerVerticalDrawOffset, cardImage, currentCardCount, isSplit);
            }
            else
            {
                handDrawer = new HandDrawer(dealerVerticalDrawOffset, cardImage, currentCardCount, isSplit);
            }
            handDrawer.Draw();
        }

        internal void DrawBet(int chips)
        {
            Console.SetCursorPosition(28, 15);
            Console.Write("Your current credits: " + chips);
            Console.SetCursorPosition(30, 17);
            Console.Write("Place your bet: ");
        }
        public void DrawRules()
        {
            Console.SetCursorPosition(56, 5);
        }
        public void CleanTable(string property)
        {
            int cleanableWidth;
            int cleanableHeight;
            int leftPosition;
            int verticalPosition;
            if (property == "player")
            {
                cleanableWidth = windowWidth / 2;
                cleanableHeight = playerVerticalDrawOffset - dealerVerticalDrawOffset;
                leftPosition = 0;
                verticalPosition = playerVerticalDrawOffset;
            }
            else
            {
                cleanableWidth = windowWidth / 3 * 2;
                cleanableHeight = 4;
                leftPosition = windowWidth / 3;
                verticalPosition = windowHeight / 3 * 2;
            }
            TableCleaner table = new TableCleaner(verticalPosition, cleanableWidth, cleanableHeight, leftPosition);
        }

        public void DrawText(string[] text)
        {
            int leftPosition = gameWidth/3;
            int verticalPosition = gameHeight + 1;
            //CleanTable("split");
            TextDrawer textDrawer = new TextDrawer(verticalPosition, leftPosition, text);
            textDrawer.Draw();
        }

        public void DrawOutcome()
        {

        }
        public void DrawHandValue(bool isPlayer, int handValue)
        {
            AnnotationDrawer annotationDrawer = new AnnotationDrawer(dealerVerticalDrawOffset, playerVerticalDrawOffset);
            if (isPlayer)
            {
                annotationDrawer.UpdateHandValue(playerVerticalDrawOffset, handValue, isPlayer);
            }
            else
            {
                annotationDrawer.UpdateHandValue(dealerVerticalDrawOffset, handValue, isPlayer);
            }
            

        }
    }
}
