using System;

namespace Game
{
    public class EnemyFactory
    {
        public Enemy GetEnemy(string name, int x, int y)
        {
            switch (name)
            {
                case "orc":
                    return new OrcEnemy(x, y);
            }

            throw new Exception($"Enemy with {name} does not exist");
        }
    }
}
