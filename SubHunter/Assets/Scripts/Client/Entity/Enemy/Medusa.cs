using UnityEngine;

public class Medusa : Enemy
{
    private const float PAUSE_INTERVAL = 2f;
    private const float MOVE_INTERVAL  = 1f;

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
        {
            if (Time.time - this.explodeStartTime < Enemy.EXPLODE_DURATION)
                return;
            
            Destroy();
            return;
        }
        
        StateUpdate();
        MoveUpdate();
    }

    private void StateUpdate()
    {
        if (this.isPaused && Time.time - this.lastStateChangeTime > PAUSE_INTERVAL)
        {
            this.isPaused = false;
            this.lastStateChangeTime = Time.time;

            var targetPos = Ship.IsAlive ? Ship.I.transform.position : new Vector3(Dimensions.SCREEN_LEFT, Dimensions.MEDUSA, 0f);
            this.dir = (targetPos - this.transform.position).normalized;

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
        if (this.transform.position.y > Dimensions.MEDUSA)
            this.transform.position = new Vector3(this.transform.position.x, Dimensions.MEDUSA, this.transform.position.z);
    }
}