using System;
using Foundation;

namespace Game
{
    /// <summary>
    /// Base class for all destructible game objects.
    /// </summary>
    public abstract class Entity
    {
        private int maxHP;
        private int hp;
        private int armor;
        private int regen;

        private string name;

        private Vector2 position;
        private Vector2 direction;

        private float acceleration;
        private float velocity;

        // burn, freeze, sludge, shock
        private string state;

        // list of abilities this entity possesses
        private Skill[] skills;

        private Entity()
        {
            // TODO:
        }
        
        public void MoveTo(int x, int y)
        {
            MoveTo(new Vector2(x, y));
        }

        public void MoveTo(Vector2 position)
        {
            
        }

        public void Update()
        {
            if (this.velocity == 0)
                return;

            this.position += this.direction;
        }

        public void SetDirection(Vector2 dir)
        {
            dir.Normalize();
            this.direction = dir;
        }
    }
}