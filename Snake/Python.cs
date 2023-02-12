using System;
using System.Collections.Generic;

namespace Snake
{
    public static class Python
    {
        private static int headXPosition = SupportClass.DEFAULT_HEAD_X_POS;
        private static int headYPosition = SupportClass.DEFAULT_HEAD_Y_POS;
        private static int tailLength = SupportClass.DEFAULT_TAIL_LENGTH;
        private static string headDirection = "E";
        public static int HeadXPosition { get { return headXPosition; } set { headXPosition = value; } }
        public static int HeadYPosition { get { return headYPosition; } set { headYPosition = value; } }
        public static int TailLength { get { return tailLength; } set { tailLength = value; } }
        public static string HeadDirection { get { return headDirection; } set { headDirection = value; } }
        public static List<int> tailListX = new List<int>();
        public static List<int> tailListY = new List<int>();

        public static void MoveHead()
        {
            if (HeadDirection == "N") HeadYPosition--;
            else if (HeadDirection == "S") HeadYPosition++;
            else if (HeadDirection == "W") HeadXPosition--;
            else if (HeadDirection == "E") HeadXPosition++;
            else if (HeadDirection == "Exit")
            {
                HeadXPosition = SupportClass.DEFAULT_HEAD_X_POS;
                HeadYPosition = SupportClass.DEFAULT_HEAD_Y_POS;
                HeadDirection = "E";
                TailLength = SupportClass.DEFAULT_TAIL_LENGTH;
                Game.Score = 0;
            }
            tailListX.Add(HeadXPosition);
            tailListY.Add(HeadYPosition);
            if (Game.screen[HeadYPosition,HeadXPosition].Equals('*') || Game.screen[HeadYPosition, HeadXPosition].Equals('X'))
            {
                HeadDirection = "Exit";
                string exit = "GAME OVER";
                for (int i = 0; i < exit.ToCharArray().Length; i++) Game.screen[9, 15 + i] = exit.ToCharArray()[i];
            }
            if (Game.screen[HeadYPosition, HeadXPosition].Equals('o'))
            {
                Game.Score++;
                TailLength++;
                Apple.CreateNewApple();
            }
        }
        public static void DrawTail()
        {
            for (int i = 0; i < TailLength; i++) 
            {
                Game.screen[tailListY[tailListY.Count-i-1], tailListX[tailListX.Count-i-1]] = 'X';
            }
        }
    }
}
