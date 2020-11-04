using Game.Interfaces;
using System;

namespace Game
{
    public class Player : IDrawable, IMovable
    {
        public Position Position;
        public Direction MoveDirection;
        public Map CurrentMap = null;

        private static Player instance = null;

        public static Player GetInstance()
        {
            if (instance == null)
                instance = new Player();
            return instance;
        }

        private Player()
        {
            Position = new Position(1, 1);
        }

        #region IDrawable implementation
        public void Draw()
        {
            SetColor();
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write(GetSymbol());
        }

        public void SetColor()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public char GetSymbol()
        {
            return '*';
        }
        #endregion

        #region IMovable implementation
        public Direction GetMoveDirection()
        {
            return MoveDirection;
        }

        public bool CanMove()
        {
            if (CurrentMap == null)
                return false;

            var tempPosition = new Position(Position.X, Position.Y);
            tempPosition.Move(GetMoveDirection());

            return !(tempPosition.X < CurrentMap.MinLeft ||
                tempPosition.X > CurrentMap.MaxLeft ||
                tempPosition.Y < CurrentMap.MinTop ||
                tempPosition.Y > CurrentMap.MaxTop);
        }

        public void Move()
        {
            Position.Move(GetMoveDirection());
        }
        #endregion
    }
}
