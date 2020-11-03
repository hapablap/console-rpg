using Game.Interfaces;
using System;

namespace Game
{
    public class Enemy : IDrawable, IMovable
    {
        public Position Position;
        public Map CurrentMap = null;

        private Direction moveDirection = Direction.None;

        public Enemy(int x, int y)
        {
            Position = new Position(x, y);
        }

        private void GenerateRandomMoveDirection()
        {
            // Random move direction
            // @source: https://stackoverflow.com/questions/3132126/how-do-i-select-a-random-value-from-an-enumeration
            Array values = Enum.GetValues(typeof(Direction));
            Random random = new Random();
            moveDirection = (Direction)values.GetValue(random.Next(values.Length));
        }

        #region IDrawable implementation
        public void Draw()
        {
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write(GetSymbol());
        }

        public char GetSymbol()
        {
            return '$';
        }
        #endregion

        #region IMovable implementation
        public void Move()
        {
            Position.Move(moveDirection);
        }

        public Direction GetMoveDirection()
        {
            return moveDirection;
        }

        public bool CanMove()
        {
            if (CurrentMap == null)
                return false;

            GenerateRandomMoveDirection();

            var tempPosition = new Position(Position.X, Position.Y);
            tempPosition.Move(moveDirection);

            return !(tempPosition.X < CurrentMap.MinLeft ||
                tempPosition.X > CurrentMap.MaxLeft ||
                tempPosition.Y < CurrentMap.MinTop ||
                tempPosition.Y > CurrentMap.MaxTop);
        }
        #endregion
    }
}
