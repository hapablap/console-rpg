using System;

namespace Game
{
    public class OrcEnemy : Enemy
    {
        public OrcEnemy(int x, int y) : base(x, y)
        {
            // Random health von 100 bis 200 bei Generierung von einem Orc
            Random random = new Random();
            Health = random.Next(100, 200);
        }

        public override char GetSymbol()
        {
            return '§';
        }
    }
}
