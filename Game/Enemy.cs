using Game.Interfaces;
using System;

namespace Game
{
    public abstract class Enemy : IDrawable, IMovable, ICollidable
    {
        public Position Position;
        public Map CurrentMap = null;
        public int Health = 100;

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
            Console.ForegroundColor = GetColor();
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write(GetSymbol());
        }

        public ConsoleColor GetColor()
        {
            return ConsoleColor.Red;
        }

        public virtual char GetSymbol()
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

        #region ICollidable Implementation
        public Position GetPosition()
        {
            return Position;
        }

        public void ActionOnCollision()
        {
            Program.IsFighting = true;
            Program.CurrentEnemy = this;
        }
        #endregion
    }
}
