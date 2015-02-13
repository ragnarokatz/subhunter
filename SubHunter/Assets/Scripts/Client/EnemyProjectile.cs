using UnityEngine;

public enum Trajectory
{
    Vertical,
    Quadratic,
}

public class EnemyProjectile : MonoBehaviour
{
    public Ship Ship;
    public float Speed;
    public Trajectory trajectory;

    private Vector3 startPos;
    private Vector3 dir;
    private QuadraticFormula formula;

    private const float TOP_EDGE = 4f;

    void Start()
    {
        if (trajectory == Trajectory.Vertical)
        {
            this.dir = Vector3.up;
            return;
        }

        this.formula = new QuadraticFormula(0.5f);
        this.startPos = this.transform.position;
        if (Ship.transform.position.x < this.transform.position.x)
        {
            this.dir = Vector3.left;
            return;
        }

        this.dir = Vector3.right;
    }

    void Update()
    {
        if (this.transform.position.y > TOP_EDGE)
        {
            Destroy();
            return;
        }

        if (trajectory == Trajectory.Vertical)
        {
            this.transform.position += this.dir * Speed;
            return;
        }

        var x = this.transform.position.x;
        if (this.dir == Vector3.left)
            x -= this.Speed;
        else
            x += this.Speed;

        var deltaX = x - this.startPos.x;
        var deltaY = this.formula.CalculateY(deltaX);

        this.transform.position = new Vector3(x, this.startPos.y + deltaY, 0f);
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}