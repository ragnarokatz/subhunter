using UnityEngine;

public enum MotionType
{
    Vertical,
    Quadratic,
}

public class EnemyProjectile : MonoBehaviour
{
    public MotionType Trajectory;
    public float      Speed;

    private Vector3 startPos;
    private Vector3 dir;
    private QuadraticFormula formula;

    private const float TOP_EDGE = 4f;

    void Start()
    {
        // Init for vertical trajectory
        if (Trajectory == MotionType.Vertical)
        {
            this.dir = Vector3.up;
            return;
        }

        // Init for quadratic trajectory
        this.formula = new QuadraticFormula(0.5f);
        this.startPos = this.transform.position;
        if (Init.I.PlayerShip.transform.position.x < this.transform.position.x)
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

        // Update for vertical trajectory
        if (Trajectory == MotionType.Vertical)
        {
            this.transform.position += this.dir * Speed * Time.deltaTime;
            return;
        }

        // Update for quadratic trajectory
        var x = this.transform.position.x;
        if (this.dir == Vector3.left)
            x -= this.Speed * Time.deltaTime;
        else
            x += this.Speed * Time.deltaTime;

        var deltaX = x - this.startPos.x;
        var deltaY = this.formula.CalculateY(deltaX);

        this.transform.position = new Vector3(x, this.startPos.y + deltaY, 0f);
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}