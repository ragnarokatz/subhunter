using UnityEngine;

public class Medusa : Enemy
{
    private const float PAUSE_INTERVAL = 2f;
    private const float MOVE_INTERVAL = 1f;

    private float lastStateChangeTime;
    private bool  isPaused;

    protected override void Start ()
    {
        base.Start();

        var spawnPos = Random.Range(SpawnFloor, SpawnCeiling);
        this.transform.position = new Vector3(spawnPos, Dimensions.BOT_EDGE, this.transform.position.z);
    }

    protected override void Update ()
    {
        if (this.isExploding)
            return;
        
        StateUpdate();
        MoveUpdate();
    }

    private void StateUpdate()
    {
        if (this.isPaused && Time.time - this.lastStateChangeTime > PAUSE_INTERVAL)
        {
            this.isPaused = false;
            this.lastStateChangeTime = Time.time;

            this.dir = (GameObserver.PlayerShip.transform.position - this.transform.position).normalized;

            return;
        }

        if (this.isPaused)
            return;

        if (Time.time - this.lastStateChangeTime < MOVE_INTERVAL)
            return;

        this.isPaused = true;
        this.lastStateChangeTime = Time.time;
    }

    private void MoveUpdate()
    {
        if (this.isPaused)
            return;

        this.transform.position += this.dir * this.speed * Time.deltaTime;
    }
}