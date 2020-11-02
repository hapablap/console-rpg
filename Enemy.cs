using Game.Interfaces;
using System;

namespace Game
{
    public class Enemy : IDrawable
    {
        public Position Position;

        public Enemy(int x, int y)
        {
            Position = new Position(x, y);
        }

        public void Draw()
        {
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write(GetSymbol());
        }

        public char GetSymbol()
        {
            return '$';
        }
    }
}
