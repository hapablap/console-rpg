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
        static Direction currentPlayerDirection = Direction.None;

        static List<IDrawable> DrawableEntities = new List<IDrawable>();

        static void Main(string[] args)
        {
            // Cursors ausblenden
            Console.CursorVisible = false;

            // Entitäten erzeugen
            Player player = new Player(1, 1);
            Enemy enemy = new Enemy(2, 2);

            // Entitäten zur Liste hinzufügen, damit sie gezeichnet werden
            DrawableEntities.Add(player);
            DrawableEntities.Add(enemy);

            // Game Loop (Endlosschleife)
            while (true)
            {
                DrawScreen();
                HandleInput();

                player.Move(currentPlayerDirection);
            }
        }

        static void DrawScreen()
        {
            // Bildschirm leeren
            Console.Clear();

            // Entitäten einzelnd zeichnen
            foreach (var drawableEntity in DrawableEntities)
            {
                drawableEntity.Draw();
            }
        }

        static void HandleInput()
        {
            // Tasteneingabe vom Spieler behandeln
            ConsoleKeyInfo key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    currentPlayerDirection = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    currentPlayerDirection = Direction.Right;
                    break;
                case ConsoleKey.UpArrow:
                    currentPlayerDirection = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    currentPlayerDirection = Direction.Down;
                    break;
                default:
                    currentPlayerDirection = Direction.None;
                    break;
            }
        }
    }
}
