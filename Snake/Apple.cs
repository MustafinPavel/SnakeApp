using System;
namespace Snake
{
    public abstract class Apple
    {
        public static int AppleX { get; private set; }
        public static int AppleY { get; private set; }
        private static Random rnd = new Random();

        public static void CreateNewApple()
        {
            while (!Game.screen[AppleY, AppleX].Equals(' ')) 
            {
                AppleX = rnd.Next(3, SupportClass.SCREEN_WIDTH - 4);
                AppleY = rnd.Next(3, SupportClass.SCREEN_HEIGHT - 4);
            }
        }
        public static void DrawApple()
        {
            Game.screen[AppleY, AppleX] = 'o';
        }
    }
}
