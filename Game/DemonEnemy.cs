namespace Game
{
    public class DemonEnemy : Enemy
    {
        public DemonEnemy(int x, int y) : base(x, y)
        {

        }

        public override char GetSymbol()
        {
            return '!';
        }
    }
}
