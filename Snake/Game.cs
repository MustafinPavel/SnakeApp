using System;
using System.Threading;


namespace Snake
{
    public class Game
    {
        public static int Score { get; set; }
        private static int screenHeight = SupportClass.SCREEN_HEIGHT;
        private static int screenWidth = SupportClass.SCREEN_WIDTH;
        public static char[,] screen;
        //Regex regexName = new Regex(@"(?<name>^[^\s]+)", RegexOptions.Multiline);
        //Regex regexScore = new Regex(@"(?<score>\b\d+$)", RegexOptions.Multiline);

        public static void ShowMainMenu()
        {
            screen = new char[screenHeight, screenWidth];
            ScreenClear();
            for (int row = 0; row < screenHeight; row++)
            {
                string newGame = "1.Новая игра";
                for (int i = 0; i < newGame.ToCharArray().Length; i++) screen[7, 14 + i] = newGame.ToCharArray()[i];
                string rules = "2.Правила игры";
                for (int i = 0; i < rules.ToCharArray().Length; i++) screen[8, 13 + i] = rules.ToCharArray()[i];
                string scores = "3.Рекорды";
                for (int i = 0; i < scores.ToCharArray().Length; i++) screen[9, 15 + i] = scores.ToCharArray()[i];
                string exit = "4.Выход";
                for (int i = 0; i < exit.ToCharArray().Length; i++) screen[10, 16 + i] = exit.ToCharArray()[i];
            }
            ScreenDraw();
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.D1) StartGame();
                else if (key.Key == ConsoleKey.D2) ShowRules();
                else if (key.Key == ConsoleKey.D3) ShowHighScores();
                else if (key.Key == ConsoleKey.D4) Environment.Exit(0);
            } while (key.Key != ConsoleKey.D1 || key.Key != ConsoleKey.D2 || key.Key != ConsoleKey.D3 || key.Key != ConsoleKey.D4);
        }


        public static void ShowRules()
        {
            ScreenClear();
            string rules = "Ползай, жри 'o' и";
            for (int i = 0; i < rules.ToCharArray().Length; i++) screen[8, 11 + i] = rules.ToCharArray()[i];
            string rules2 = "не врезайся в стены и хвост";
            for (int i = 0; i < rules2.ToCharArray().Length; i++) screen[9, 6 + i] = rules2.ToCharArray()[i];
            string back = "Нажми любую клавишу,";
            for (int i = 0; i < back.ToCharArray().Length; i++) screen[15, 10 + i] = back.ToCharArray()[i];
            string back2 = "чтобы вернуться в меню";
            for (int i = 0; i < back2.ToCharArray().Length; i++) screen[16, 9 + i] = back2.ToCharArray()[i];
            ScreenDraw();
            Console.ReadKey(true);
            ShowMainMenu();
        }

        public static void ShowHighScores()
        {                                               // Добавить меню рекордов.
            ScreenClear();                              // Ключ - разобраться в работе с xml файлами.
            string scores = "В разработке";
            for (int i = 0; i < scores.ToCharArray().Length; i++) screen[8, 13 + i] = scores.ToCharArray()[i];
            string back = "Нажми любую клавишу,";
            for (int i = 0; i < back.ToCharArray().Length; i++) screen[15, 10 + i] = back.ToCharArray()[i];
            string back2 = "чтобы вернуться в меню";
            for (int i = 0; i < back2.ToCharArray().Length; i++) screen[16, 9 + i] = back2.ToCharArray()[i];
            ScreenDraw();
            Console.ReadKey(true);
            ShowMainMenu();
        }

        private static void ScreenDraw()
        {
            for (int row = 0; row < screenHeight; row++)
            {
                for (int column = 0; column < screenWidth; column++) Console.Write(screen[row, column]);
                Console.WriteLine();
            }
        }
        private static void ScreenClear()
        {
            Console.Clear();
            for (int row = 0; row < screenHeight; row++)
            {
                for (int column = 0; column < screenWidth; column++)
                {
                    if (row < 3 || row > screenHeight - 4 || column < 3 || column > screenWidth - 4) screen[row, column] = '*';
                    else screen[row, column] = ' ';
                }
            }
        }

        private static void ShowScoreAndExit()
        {
            string score = $" Score: {Game.Score} ";
            for (int i = 0; i < score.ToCharArray().Length; i++) screen[1, 22 + i] = score.ToCharArray()[i];
            string exit = " Нажми L, чтобы выйти ";
            for (int i = 0; i < exit.ToCharArray().Length; i++) screen[SupportClass.SCREEN_HEIGHT - 2, 9 + i] = exit.ToCharArray()[i];
        }

        public static void StartGame()
        {
            Thread timer = new Thread(TimerThread);
            timer.Start();
            Apple.CreateNewApple();
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);        // Заблокировать возможность разворота на 180 градусов
                if (key.Key == ConsoleKey.W)        // Ключ - последняя запись о положении хвоста в Python
                {
                    if (Python.HeadDirection == "S") Python.HeadDirection = "S";
                    else Python.HeadDirection = "N";
                }
                else if (key.Key == ConsoleKey.S)
                {
                    if (Python.HeadDirection == "N") Python.HeadDirection = "N";
                    else Python.HeadDirection = "S";
                }
                else if (key.Key == ConsoleKey.A)
                {
                    if (Python.HeadDirection == "E") Python.HeadDirection = "E";
                    else Python.HeadDirection = "W";
                }
                else if (key.Key == ConsoleKey.D)
                {
                    if (Python.HeadDirection == "W") Python.HeadDirection = "W";
                    else Python.HeadDirection = "E";
                }

            } while (key.Key != ConsoleKey.L);
            Python.HeadDirection = "Exit";
            ShowMainMenu();
        }
        public static void TimerThread()
        {
            do
            {
                ScreenClear();
                ShowScoreAndExit();
                Apple.DrawApple();
                Python.DrawTail();
                Python.MoveHead();
                screen[Python.HeadYPosition, Python.HeadXPosition] = '%';
                ScreenDraw();
                Thread.Sleep(SupportClass.TICK_SPEED);

            } while (!Python.HeadDirection.Equals("Exit"));
        }
    }
}
