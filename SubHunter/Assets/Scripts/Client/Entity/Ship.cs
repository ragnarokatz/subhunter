using UnityEngine;
using System.IO;
using Foundation;

public class Ship : Entity
{
    public class Data
    {
        public static int   Clips { get; set; }
        public static int   Nukes { get; set; }
        public static float Speed { get; set; }
    }

    private static Ship instance;
    public static Ship I { get { return Ship.instance; } }
    public static bool IsAlive { get { return Ship.instance != null; } }

    public GameObject Weapon;
    public float      FireInterval;

    private float lastFireLeftTime;
    private float lastFireRightTime;
    private float lastFireMiddleTime;

    public void MoveLeft()
    {
        if (this.transform.position.x <= Dimensions.LEFT_EDGE)
            return;

        this.transform.position += Vector3.left * Data.Speed * Time.deltaTime;
    }

    public void MoveRight()
    {
        if (this.transform.position.x >= Dimensions.RIGHT_EDGE)
            return;

        this.transform.position += Vector3.right * Data.Speed * Time.deltaTime;
    }

    public void FireLeft()
    {
        if (Data.Clips <= 0)
            return;
        
        if (Time.time - this.lastFireLeftTime < this.FireInterval)
            return;
        
        Data.Clips--;
        this.lastFireLeftTime = Time.time;

        GameObject.Instantiate(this.Weapon, new Vector3(this.Box.xMin, this.transform.position.y, 0f), Quaternion.identity);
    }
    
    public void FireRight()
    {
        if (Data.Clips <= 0)
            return;
        
        if (Time.time - this.lastFireRightTime < this.FireInterval)
            return;
        
        Data.Clips--;
        this.lastFireRightTime = Time.time;

        GameObject.Instantiate(this.Weapon, new Vector3(this.Box.xMax, this.transform.position.y, 0f), Quaternion.identity);
    }
    
    public void FireMiddle()
    {
        if (Data.Clips <= 0)
            return;
        
        if (Time.time - this.lastFireMiddleTime < this.FireInterval)
            return;
        
        Data.Clips--;
        this.lastFireMiddleTime = Time.time;

        GameObject.Instantiate(this.Weapon, this.transform.position, Quaternion.identity);
    }

    public void UseNuke()
    {
        if (Data.Nukes <= 0)
            return;

        Data.Nukes--;
        var comboIdx = Combo.StartCombo();
        foreach (var enemy in EntityManager.I.Enemies)
            enemy.Explode(comboIdx);
    }

    public override void Destroy ()
    {
        base.Destroy ();

        Ship.instance = null;
        GameObject.Instantiate(Prefabs.Explosion, this.transform.position, Quaternion.identity);
    }

    protected override void Start ()
    {
        System.Diagnostics.Debug.Assert(Ship.instance == null);

        Ship.instance = this;
        this.transform.position = new Vector3(0f, Dimensions.WATER, 0f);
        Data.Speed = UnityEngine.Random.Range(this.SpeedMin, this.SpeedMax);
    }

    protected override void Update()
    {
        if (Input.GetKey(KeyCode.Z))
            FireLeft();

        if (Input.GetKey(KeyCode.X))
            FireRight();

        if (Input.GetKey(KeyCode.Space))
            FireMiddle();

        if (Input.GetKey(KeyCode.LeftArrow))
            MoveLeft();

        if (Input.GetKey(KeyCode.RightArrow))
            MoveRight();
    }
}
