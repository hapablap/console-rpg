using Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public class Map
    {
        public Position Position;
        public int Height;
        public int Width;

        public Map(int x, int y, int height, int width)
        {
            Position = new Position(x, y);
            Height = height;
            Width = width;
        }

        // Aufgabe 1:
        // Implementieren Sie das Interface IDrawable, so dass die Map
        // als "Mauer" gezeichnet wird. Verwenden Sie als Symbol die Raute '#'
        // Fügen Sie die Map in Program.cs hinzu, so dass diese auch gezeichnet wird.
    }
}
