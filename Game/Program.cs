using Game.Interfaces;
using System;
using System.Collections.Generic;

namespace Game
{
    class Program
    {
        static List<IDrawable> DrawableEntities = new List<IDrawable>();
        static List<IMovable> MovableEntities = new List<IMovable>();
        static List<ICollidable> Collidables = new List<ICollidable>();

        static EnemyFactory EnemyFactory = new EnemyFactory();

        public static bool IsFighting = false;
        public static Enemy CurrentEnemy = null;

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
                if (IsFighting)
                {
                    DrawFightScreen();
                }
                else
                {
                    DrawScreen();
                    HandleInput();
                    MoveEntities();
                    CheckCollisions();
                }
            }
        }

        static void GenerateEnemy(string name, Map map, int x, int y)
        {
            Enemy enemy = EnemyFactory.GetEnemy(name, x, y);
            enemy.CurrentMap = map;
            DrawableEntities.Add(enemy);
            MovableEntities.Add(enemy);
            Collidables.Add(enemy);
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
            foreach (ICollidable collidable in Collidables)
            {
                if (Player.GetInstance().Position.IsEqual(collidable.GetPosition()))
                {
                    collidable.ActionOnCollision();
                }
            }
        }

        static void DrawFightScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Fight started!");
            Console.WriteLine("Choose action:");
            Console.WriteLine("    0) Run away");
            Console.WriteLine("    1) Fight");

            int action = Convert.ToInt32(Console.ReadLine());

            switch (action)
            {
                case 0:
                    // Run away
                    // Tipp: Rückgängig machen von Enemy.ActionOnCollision()
                    break;
                case 1:
                    // Fight
                    // Tipp: Z.B. Random eine Zahl werfen (0-1), 
                    //       bei 0: Gegner trifft Spieler (Player.Health verringern)
                    //       bei 1: Spieler trifft Gegner (CurrentEnemy.Health verringern)
                    // Überprüfen, ob Player oder CurrentEnemy Health <= 0
                    //      bei Player.Health <0 => Game Over
                    //      bei CurrentEnemy.Health => Spiel fortsetzen (vgl. Run Away),
                    //                              => zusätzlich besiegten Gegner entfernen
                    break;
                default:
                    // Invalid input
                    break;
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
