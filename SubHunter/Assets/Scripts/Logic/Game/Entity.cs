using System;
using Foundation;

namespace Game
{
    /// <summary>
    /// Base class for all destructible game objects.
    /// </summary>
    public class Entity
    {
        private string name;

        private Vector2 position;
        private Vector2 direction;

        protected float velocity;

        private Skill skill;

        private bool isDead;
        public  bool IsDead { get { return this.isDead; } }

        protected Entity()
        {
            // TODO:
        }

        protected void MoveTo(int x, int y, float velocity)
        {
            var dir = new Vector2(x, y) - this.position;
            MoveTo(dir, velocity);
        }

        protected void MoveTo(Vector2 direction, float velocity)
        {
            SetDirection(direction);
            this.velocity = velocity;
        }

        public void Update()
        {
            if (this.isDead)
                return;

            if (this.velocity.Equals(0f))
                return;

            if (this.direction.Equals(Vector2.Zero))
                return;

            this.position += this.direction * velocity;
        }

        private void SetDirection(Vector2 dir)
        {
            this.direction = dir.Normalized;
        }
    }
}