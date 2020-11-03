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

        static EnemyFactory EnemyFactory = new EnemyFactory();

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Map map = new Map(1, 1, 20, 50);
            Enemy orcEnemy = EnemyFactory.GetEnemy("orc", 2, 2);
            Enemy demonEnemy = EnemyFactory.GetEnemy("demon", 5, 8);
            Enemy undeadEnemy = EnemyFactory.GetEnemy("undead", 12, 15);
            Player player = Player.GetInstance();
            player.CurrentMap = map;
            orcEnemy.CurrentMap = map;
            demonEnemy.CurrentMap = map;
            undeadEnemy.CurrentMap = map;

            DrawableEntities.Add(map);
            DrawableEntities.Add(player);
            DrawableEntities.Add(orcEnemy);
            DrawableEntities.Add(demonEnemy);
            DrawableEntities.Add(undeadEnemy);

            MovableEntities.Add(player);
            MovableEntities.Add(orcEnemy);
            MovableEntities.Add(demonEnemy);
            MovableEntities.Add(undeadEnemy);

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
