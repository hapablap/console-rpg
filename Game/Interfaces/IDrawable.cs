using System;

namespace Game.Interfaces
{
    public interface IDrawable
    {
        public void Draw();
        public char GetSymbol();
        public ConsoleColor GetColor();
    }
}
