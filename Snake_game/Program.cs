using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Snake_game
{
    public class Program
    {

        static void Main(string[] args)
        {
            int screenwidth = 50;
            int screenheight = 50;
            bool appleEaten = false;
            
            Console.SetWindowSize(screenwidth, screenheight);
          Game game = new Game(50,50);
            
            while (game.GameStatus)
            {
                Thread.Sleep(Convert.ToInt32(50*game.GameSpeed));
                int newX = game.SnakeBody[0].X;
                int newY = game.SnakeBody[0].Y;
                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.DownArrow:
                            {
                                game.Direction = Direction.Down;
                                break;
                            }
                        case ConsoleKey.UpArrow:
                            {
                                game.Direction = Direction.Up;
                                break;
                            }
                        case ConsoleKey.LeftArrow:
                            {
                                game.Direction = Direction.Left;
                                break;
                            }
                        case ConsoleKey.RightArrow:
                            {
                                game.Direction = Direction.Right;
                                break;
                            }
                    }
                }
                    switch (game.Direction)
                    {
                        case Direction.Left:
                            {
                                newX--;
                                break;
                            }
                        case Direction.Right:
                            {
                                newX++;
                                break;
                            }
                        case Direction.Up:
                            {
                                newY--;
                                break;
                            }
                        case Direction.Down:
                            {
                                newY++;
                                break;
                            }
                    }

                appleEaten = false;
                game.SnakeBody.Insert(0, new Snake(newX,newY));
                    for (int j = 0; j < game.AppleList.Count; j++)
                    {
                        if (game.SnakeBody[0].X == game.AppleList[j].X && game.SnakeBody[0].Y == game.AppleList[j].Y) {
                            game.GameSpeed = 1.0 /game.SnakeBody.Count;
                            game.AppleList.RemoveAt(j);
                            appleEaten = true;
                            game.AppleList.Add(new Apple(game));
                        }
                    }
                    for (int i = 0;  i < game.SnakeBody.Count; i++)
                {
                    for (int j = 0; i < game.SnakeBody.Count; j++)
                    {
                        if (j == i)
                        {
                            break;
                        }
                        if (game.SnakeBody[i].X == game.SnakeBody[j].X && game.SnakeBody[i].Y == game.SnakeBody[j].Y)
                        {
                            game.GameStatus = false;
                        }

                    }
                    if (game.SnakeBody[i].X > game.ScreenWidth || newX < 0 || game.SnakeBody[i].Y > game.ScreenHeight || newY < 0) {
                        game.GameStatus = false;
                    }
                }
                if (!appleEaten) {
                    game.SnakeBody.RemoveAt(game.SnakeBody.Count - 1);                    
                }
                if (game.GameStatus)
                {
                    Render(game);
                }
                

            }
            Console.WriteLine("\nYOU LOST! :(\n Your final score was: " + game.SnakeBody.Count);
        }
        static void Render(Game game)
        {
            Console.Clear();
            for (int i = 0; i < game.SnakeBody.Count; i++)
            {
                Console.SetCursorPosition(game.SnakeBody[i].X, game.SnakeBody[i].Y);
                Console.Write("■");

            }
            for (int i = 0; i < game.AppleList.Count; i++)
            {
                Console.SetCursorPosition(game.AppleList[i].X, game.AppleList[i].Y);
                Console.Write("\x1B[31m■\x1B[0m");
            }
        }
    }

    public class Game
    {
        public int ScreenWidth {  get; private set; }
        public int ScreenHeight { get; private set; }
        public bool GameStatus { get; set; }
        public double GameSpeed { get; set; }
        public List<Snake> SnakeBody { get; private set; }
        public Direction Direction { get; set; }
        public List<Apple> AppleList { get; private set; }


        public Game(int screenX, int ScreenY)
        {
            ScreenWidth = screenX;
            ScreenHeight = ScreenY;
            GameSpeed = 1.0;
            SnakeBody = new List<Snake>();
            AppleList = new List<Apple>();
            Direction = Direction.Up;
            GameStatus = true;
            AppleList.Add(new Apple(this));
            SnakeBody.Add(new Snake(ScreenWidth / 2, ScreenHeight / 2));

        }
    }
    public class Apple
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Apple(Game game)
        {
            var random = new Random();

            X = random.Next(0,game.ScreenWidth);
            Y = random.Next(0, game.ScreenHeight);
        }
    }
    public class Snake
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Snake(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}

