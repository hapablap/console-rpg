namespace Game
{
    public class OrcEnemy : Enemy
    {
        public OrcEnemy(int x, int y) : base(x, y)
        {

        }

        public override char GetSymbol()
        {
            return '§';
        }
    }
}
