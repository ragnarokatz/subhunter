using UnityEngine;
using System.IO;
using Foundation;

public class Ship : Entity
{
    public class Data
    {
        private static int   clip;
        private static int   nuke;
        private static float speed;

        public static int   Clip  { get { return Data.clip; } }
        public static int   Nuke  { get { return Data.nuke; } }
        public static float Speed { get { return Data.speed; } }

        public static void Init()
        {
            Data.clip  = 5;
            Data.nuke  = 0;
            Data.speed = 3.5f;

            EventManager.UpdateAttrib("clip");
        }

        public static void UseClip()
        {
            Data.clip--;
            EventManager.UpdateAttrib("clip");
        }

        public static void AddClip()
        {
            Data.clip++;
            EventManager.UpdateAttrib("clip");
        }

        public static void UseNuke()
        {
            Data.nuke--;
        }

        public static void AddNuke()
        {
            Data.nuke++;
        }

        public static void Speedup()
        {
            Data.speed = 7f;
        }

        public static void RestoreSpeed()
        {
            Data.speed = 3.5f;
        }
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
        if (Data.Clip <= 0)
            return;
        
        if (Time.time - this.lastFireLeftTime < this.FireInterval)
            return;

        Log.Trace("Fire left.");

        Data.UseClip();
        this.lastFireLeftTime = Time.time;

        GameObject.Instantiate(this.Weapon, new Vector3(this.Box.xMin, Dimensions.WATER, 0f), Quaternion.identity);
    }
    
    public void FireRight()
    {
        if (Data.Clip <= 0)
            return;
        
        if (Time.time - this.lastFireRightTime < this.FireInterval)
            return;

        Log.Trace("Fire right.");

        Data.UseClip();
        this.lastFireRightTime = Time.time;

        GameObject.Instantiate(this.Weapon, new Vector3(this.Box.xMax, Dimensions.WATER, 0f), Quaternion.identity);
    }
    
    public void FireMiddle()
    {
        if (Data.Clip <= 0)
            return;
        
        if (Time.time - this.lastFireMiddleTime < this.FireInterval)
            return;

        Log.Trace("Fire middle.");

        Data.UseClip();
        this.lastFireMiddleTime = Time.time;

        GameObject.Instantiate(this.Weapon, new Vector3(this.transform.position.x, Dimensions.WATER, 0f), Quaternion.identity);
    }

    public void UseNuke()
    {
        if (Data.Nuke <= 0)
            return;

        Data.UseNuke();
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
        Log.Assert(Ship.instance == null);

        Ship.instance = this;
        this.transform.position = new Vector3(0f, Dimensions.SHIP, 0f);
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
