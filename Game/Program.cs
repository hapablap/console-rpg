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
        static List<Enemy> enemies = new List<Enemy>();

        static EnemyFactory EnemyFactory = new EnemyFactory();

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Map map = new Map(1, 1, 20, 50);

            GenerateEnemy("orc", map, 2, 2);
            GenerateEnemy("orc", map, 4, 11);
            GenerateEnemy("demon", map, 5, 8);
            GenerateEnemy("orc", map, 12, 15);

            Player player = Player.GetInstance();
            player.CurrentMap = map;

            DrawableEntities.Add(map);
            DrawableEntities.Add(player);
            MovableEntities.Add(player);

            while (true)
            {
                DrawScreen();
                HandleInput();
                MoveEntities();
                CheckCollisions();
            }
        }

        static void GenerateEnemy(string name, Map map, int x, int y)
        {
            Enemy enemy = EnemyFactory.GetEnemy(name, x, y);
            enemy.CurrentMap = map;
            DrawableEntities.Add(enemy);
            MovableEntities.Add(enemy);
            enemies.Add(enemy);
        }

        static void MoveEntities()
        {
            foreach (var movableEntity in MovableEntities)
            {
                if (movableEntity.CanMove())
                    movableEntity.Move();
            }
        }

        static void CheckCollisions()
        {
            foreach (Enemy enemy in enemies)
            {
                // ICollidable.GetPosition() vergleichen mit Player Position
                if (Player.GetInstance().Position.X == enemy.Position.X && Player.GetInstance().Position.Y == enemy.Position.Y)
                {
                    // Enemy: Kampf starten
                    // (Item: Gegenstand benutzen)
                    // (NPC: Sprechen)
                    // => ICollidable.ActionOnCollision()
                }
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
