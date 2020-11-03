using Game.Interfaces;
using System;
using System.Collections.Generic;

namespace Game
{
    // 3. Aufgabe:
    // Implementieren Sie objektorientiert die Möglichkeit, zu verhindern,
    // dass der Spieler durch die Wand der Map läuft
    class Program
    {
        static List<IDrawable> DrawableEntities = new List<IDrawable>();
        static List<IMovable> MovableEntities = new List<IMovable>();

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Map map = new Map(1, 1, 20, 50);
            Enemy enemy = new Enemy(2, 2);
            Player player = Player.GetInstance();
            player.CurrentMap = map;
            enemy.CurrentMap = map;

            DrawableEntities.Add(map);
            DrawableEntities.Add(player);
            DrawableEntities.Add(enemy);

            MovableEntities.Add(player);
            MovableEntities.Add(enemy);

            while (true)
            {
                DrawScreen();
                HandleInput();
                MoveEntities();
            }
        }

        static void MoveEntities()
        {
            foreach (var movableEntity in MovableEntities)
            {
                if (movableEntity.CanMove())
                    movableEntity.Move();
            }
        }

        static void DrawScreen()
        {
            Console.Clear();

            foreach (var drawableEntity in DrawableEntities)
            {
                drawableEntity.Draw();
            }
        }

        static void HandleInput()
        {
            ConsoleKeyInfo key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    Player.GetInstance().MoveDirection = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    Player.GetInstance().MoveDirection = Direction.Right;
                    break;
                case ConsoleKey.UpArrow:
                    Player.GetInstance().MoveDirection = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    Player.GetInstance().MoveDirection = Direction.Down;
                    break;
                default:
                    Player.GetInstance().MoveDirection = Direction.None;
                    break;
            }
        }
    }
}
