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
            
            Console.SetWindowSize(screenwidth, screenheight);
          Game game = new Game(50,50);
            
            while (game.GameStatus)
            {
                Thread.Sleep(Convert.ToInt32(50*game.GameSpeed));
                int newX = game.SnakeBody[0].X;
                int newY = game.SnakeBody[0].Y;
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.DownArrow: { 
                            game.Direction = Direction.Down;
                            break;
                        }
                    case ConsoleKey.UpArrow: { 
                            game.Direction = Direction.Up;
                            break;
                        }
                    case ConsoleKey.LeftArrow: { 
                            game.Direction = Direction.Left;
                            break;
                        }
                    case ConsoleKey.RightArrow: { 
                            game.Direction = Direction.Right;
                            break;
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

                Render(game);
                game.SnakeBody.Insert(0, new Snake(newX,newY));
                game.SnakeBody.RemoveAt(game.SnakeBody.Count - 1);

            }
        }
        static void Render(Game game)
        {
            Console.Clear();
            for (int i = 0; i < game.SnakeBody.Count; i++) {
                Console.SetCursorPosition(game.SnakeBody[i].X, game.SnakeBody[i].Y);
                Console.Write("■");
            }
        }
    }

    public class Game
    {
        public int ScreenWidth {  get; private set; }
        public int ScreenHeight { get; private set; }
        public bool GameStatus { get; private set; }
        public double GameSpeed { get; private set; }
        public List<Snake> SnakeBody { get; private set; }
        public Direction Direction { get; set; }


        public Game(int screenX, int ScreenY)
        {
            ScreenWidth = screenX;
            ScreenHeight = ScreenY;
            GameSpeed = 1.0;
            SnakeBody = new List<Snake>();
            Direction = Direction.Up;
            GameStatus = true;

            SnakeBody.Add(new Snake(ScreenWidth / 2, ScreenHeight / 2));

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

