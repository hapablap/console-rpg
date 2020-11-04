namespace Game.Interfaces
{
    public interface ICollidable
    {
        public Position GetPosition();
        public void ActionOnCollision();
    }
}
