namespace Game
{
    public class DemonEnemy : Enemy
    {
        public DemonEnemy(int x, int y) : base(x, y)
        {
            // Jeder Demon hat 80 Health
            Health = 80;
        }

        public override char GetSymbol()
        {
            return '!';
        }
    }
}
