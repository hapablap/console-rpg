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

        static bool IsGameOver = false;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Map map = new Map(1, 1, 20, 50);

            List<EnemySaveData> enemySaveDataList = LoadEnemySaveData();
            foreach (EnemySaveData enemySaveData in enemySaveDataList)
            {
                GenerateEnemy(enemySaveData.EnemyType, map, enemySaveData.X, enemySaveData.Y);
            }

            Player player = Player.GetInstance();
            player.CurrentMap = map;

            DrawableEntities.Add(map);
            DrawableEntities.Add(player);
            MovableEntities.Add(player);

            while (!IsGameOver)
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

                if (IsGameOver)
                    DrawGameOverScreen();
            }
        }

        static List<EnemySaveData> LoadEnemySaveData()
        {
            List<EnemySaveData> enemySaveDataList = new List<EnemySaveData>();

            EnemySaveData enemySaveData1 = new EnemySaveData();
            enemySaveData1.EnemyType = "orc";
            enemySaveData1.X = 2;
            enemySaveData1.Y = 2;

            EnemySaveData enemySaveData2 = new EnemySaveData();
            enemySaveData2.EnemyType = "orc";
            enemySaveData2.X = 4;
            enemySaveData2.Y = 11;

            EnemySaveData enemySaveData3 = new EnemySaveData();
            enemySaveData3.EnemyType = "demon";
            enemySaveData3.X = 4;
            enemySaveData3.Y = 8;

            EnemySaveData enemySaveData4 = new EnemySaveData();
            enemySaveData4.EnemyType = "undead";
            enemySaveData4.X = 12;
            enemySaveData4.Y = 15;

            EnemySaveData enemySaveData5 = new EnemySaveData();
            enemySaveData5.EnemyType = "undead";
            enemySaveData5.X = 24;
            enemySaveData5.Y = 15;

            enemySaveDataList.Add(enemySaveData1);
            enemySaveDataList.Add(enemySaveData2);
            enemySaveDataList.Add(enemySaveData3);
            enemySaveDataList.Add(enemySaveData4);
            enemySaveDataList.Add(enemySaveData5);

            return enemySaveDataList;
        }

        static void GenerateEnemy(string name, Map map, int x, int y)
        {
            Enemy enemy = EnemyFactory.GetEnemy(name, x, y);
            enemy.CurrentMap = map;
            DrawableEntities.Add(enemy);
            MovableEntities.Add(enemy);
            Collidables.Add(enemy);
        }

        static void RemoveEnemy(Enemy enemy)
        {
            DrawableEntities.Remove(enemy);
            MovableEntities.Remove(enemy);
            Collidables.Remove(enemy);
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

        static void DrawGameOverScreen()
        {
            Console.Clear();
            Console.WriteLine("Game Over!");
            Console.ReadLine();
        }

        static void DrawFightScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Fight started!");
            Console.WriteLine($"Your Health: {Player.GetInstance().Health}");
            Console.WriteLine($"Enemy's Health: {CurrentEnemy.Health}");
            Console.WriteLine("Choose action:");
            Console.WriteLine("    0) Run away");
            Console.WriteLine("    1) Fight");

            int action = Convert.ToInt32(Console.ReadLine());

            switch (action)
            {
                case 0:
                    IsFighting = false;
                    CurrentEnemy = null;
                    break;
                case 1:
                    Random random = new Random();
                    int randomNumber = random.Next(0, 100);
                    if(randomNumber <= 30)
                    {
                        Player.GetInstance().Health -= 10;
                    }
                    else 
                    {
                        CurrentEnemy.Health -= 10;
                    }

                    if (Player.GetInstance().Health <= 0)
                        IsGameOver = true;

                    if(CurrentEnemy.Health <= 0)
                    {
                        RemoveEnemy(CurrentEnemy);
                        CurrentEnemy = null;
                        IsFighting = false;
                    }
                    break;
                default:
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
