namespace Game.Interfaces
{
    public interface IMovable
    {
        public Direction GetMoveDirection();
        public bool CanMove();
        public void Move();
    }
}
