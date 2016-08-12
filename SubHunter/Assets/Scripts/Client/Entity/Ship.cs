using UnityEngine;
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
            Data.speed = 2f;

            EventManager.UpdateAttribs("clip", false);
        }

        public static void UseClip()
        {
            Data.clip--;
            EventManager.UpdateAttribs("clip", false);
        }

        public static void AddClip()
        {
            Data.clip++;
            EventManager.UpdateAttribs("clip", true, "add");
        }

        public static void RestoreClip()
        {
            Data.clip++;
            EventManager.UpdateAttribs("clip", true, "restore");
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
            Data.speed = 5f;
        }

        public static void RestoreSpeed()
        {
            Data.speed = 2f;
        }
    }

    private static Ship instance;
    public static Ship I { get { return Ship.instance; } }
    public static bool IsAlive { get { return Ship.instance != null; } }

    public GameObject Weapon;
    public float FireInterval;

    private float lastFireTime;
    // private float lastFireLeftTime;
    // private float lastFireRightTime;
    // private float lastFireMiddleTime;

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
        
        if (Time.time - this.lastFireTime < this.FireInterval)
            return;

        Data.UseClip();
        this.lastFireTime = Time.time;
        AudioManager.I.AudioSources[0].Play();

        GameObject.Instantiate(this.Weapon, new Vector3(this.Box.xMin - 0.1f, Dimensions.WATER_SURFACE, 0f), Quaternion.identity);
    }
    
    public void FireRight()
    {
        if (Data.Clip <= 0)
            return;
        
        if (Time.time - this.lastFireTime < this.FireInterval)
            return;

        Data.UseClip();
        this.lastFireTime = Time.time;
        AudioManager.I.AudioSources[0].Play();

        GameObject.Instantiate(this.Weapon, new Vector3(this.Box.xMax + 0.2f, Dimensions.WATER_SURFACE, 0f), Quaternion.identity);
    }
    
    public void FireMiddle()
    {
        if (Data.Clip <= 0)
            return;
        
        if (Time.time - this.lastFireTime < this.FireInterval)
            return;

        Data.UseClip();
        this.lastFireTime = Time.time;
        AudioManager.I.AudioSources[0].Play();

        GameObject.Instantiate(this.Weapon, new Vector3(this.transform.position.x, Dimensions.WATER_SURFACE, 0f), Quaternion.identity);
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

    public void Explode()
    {
        GameObject.Instantiate(Prefabs.Explosion, this.transform.position, Quaternion.identity);
        Destroy ();
    }

    // Destroy without explosion
    public override void Destroy ()
    {
        base.Destroy ();

        Ship.instance = null;
    }

    protected void Awake ()
    {
        Log.Assert(Ship.instance == null);

        Ship.instance = this;

        this.transform.SetParent(EntityManager.I.ShipParent, true);
        this.transform.localPosition = Vector3.zero;
        BuffManager.I.AddStartBuff();
    }

    protected override void Start()
    {

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
