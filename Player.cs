using Game.Interfaces;
using System;

namespace Game
{
    // Aufgabe 2:
    // Implementieren Sie das Singleton Pattern, so dass der Player nur ein Mal erzeugt werden
    // kann. Sie können die Parameter x / y in der Methode GetInstance als Parameter übergeben.
    public class Player : IDrawable
    {
        public Position Position;

        public Player(int x, int y)
        {
            Position = new Position(x, y);
        }

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    Position.Y--;
                    break;
                case Direction.Down:
                    Position.Y++;
                    break;
                case Direction.Left:
                    Position.X--;
                    break;
                case Direction.Right:
                    Position.X++;
                    break;
            }
        }

        public void Draw()
        {
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write(GetSymbol());
        }

        public char GetSymbol()
        {
            return '*';
        }
    }
}
