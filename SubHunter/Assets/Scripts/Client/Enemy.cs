using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float SpeedMin;
    public float SpeedMax;

    public float SpawnCeiling;
    public float SpawnFloor;

    private float velocity;
    private float acceleration;

    private Vector3 dir;

    void Start()
    {
        this.velocity = Random.Range(SpeedMin, SpeedMax);
        var height = Random.Range(SpawnFloor, SpawnCeiling);

        var dirRandom = Random.Range(0, 2);
        if (dirRandom == 0)
        {
            this.dir = Vector3.right;
            this.transform.position = new Vector3(-4f, height, this.transform.position.z);
            return;
        }

        this.dir = Vector3.left;
        this.transform.position = new Vector3(4f, height, this.transform.position.z);
    }

    void Update()
    {
        this.transform.localPosition += this.dir * this.velocity;
    }
}