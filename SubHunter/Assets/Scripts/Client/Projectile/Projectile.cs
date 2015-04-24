using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Collision dimensions of the projectile
    public float Width;
    public float Height;

    // Speed of the projectile
    public float Speed;

    // Direction and destroy boundary
    protected Vector3 dir;
    protected float destroyBoundary;

    // Collision box of the projectile
    public Rect Box
    {
        get
        {
            return new Rect(
                this.transform.position.x - this.Width / 2,
                this.transform.position.y - this.Height / 2,
                this.Width,
                this.Height);
        }
    }

    public virtual void Start()
    {
    }

    public virtual void Update()
    {
        if (IsCollidingWithShip())
        {
            // If the projectile has collided with player ship, end game
            Game.EndGame();
            Destroy();
            return;
        }

        this.transform.position += this.dir * this.Speed * Time.deltaTime;
    }

    protected bool IsCollidingWithShip()
    {
        if (this.Box.Overlaps(Game.PlayerShip.Box))
            return true;

        return false;
    }

    protected void Destroy()
    {
        Destroy(this.gameObject);
    }
}