using Game.Interfaces;
using System;

namespace Game
{
    public class Map : IDrawable
    {
        public Position Position;
        public int Height;
        public int Width;
        public int MinLeft { get; private set; }
        public int MaxLeft { get; private set; }
        public int MinTop { get; private set; }
        public int MaxTop { get; private set; }

        public Map(int x, int y, int height, int width)
        {
            Position = new Position(x, y);
            Height = height;
            Width = width;

            MinLeft = Position.X;
            MaxLeft = Position.X + Width;
            MinTop = Position.Y;
            MaxTop = Position.Y + Height;
        }

        public void Draw()
        {
            for (int i = MinLeft; i < MaxLeft + 1; i++)
            {
                Console.SetCursorPosition(i, MinTop - 1);
                Console.Write(GetSymbol());
            }

            for (int i = MinTop; i <= MaxTop + 1; i++)
            {
                Console.SetCursorPosition(MinLeft - 1, i - 1);
                Console.Write(GetSymbol());
            }

            for (int i = MinLeft; i < MaxLeft + 2; i++)
            {
                Console.SetCursorPosition(i - 1, MaxTop + 1);
                Console.Write(GetSymbol());
            }

            for (int i = MinTop - 1; i <= MaxTop + 1; i++)
            {
                Console.SetCursorPosition(MaxLeft + 1, i);
                Console.Write(GetSymbol());
            }
        }

        public char GetSymbol()
        {
            return '#';
        }
    }
}
