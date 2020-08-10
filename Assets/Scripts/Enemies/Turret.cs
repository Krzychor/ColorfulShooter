using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : ObjectColor {

    public static Turret FirstTurret;
    public BoxCollider2D Coll;
    public Color TurretColor;
    public int ColorChoice = 2;
    private float cdShoot;
    private static int hp = 5;
    private int counter = 0;


    private void Awake()
    {
        FirstTurret = this;
        Coll = GetComponent<BoxCollider2D>();
    }
    // Use this for initialization
    void Start ()
    {
        TurretColor = Player.Single.color[ColorChoice - 1];
    }
	
	// Update is called once per frame
	void Update ()
    {
        cdShoot += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (cdShoot > 3f)
        { 
            Shoot();
            cdShoot = 0;
        }
        
        if (counter == hp)
            Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet") && collision.gameObject.GetComponent<SpriteRenderer>().color != TurretColor)
            counter++;
    }

    void Shoot()
    {
        Vector3 Target = Player.Single.transform.position;
        Vector3 Dir = (Target - transform.position).normalized;

        GameObject B = Instantiate(Player.Single.BulletPrefab, transform.position + Dir*200, Quaternion.identity);

        B.GetComponent<Bullet>().ColorChange(Player.Single.color[ColorChoice - 1]);
        Rigidbody2D BRigid = B.GetComponent<Rigidbody2D>();

        BRigid.velocity = Dir * B.GetComponent<Bullet>().Bulletspeed;
    }

    public override Color GetColor()
    {
        return TurretColor;
    }
}
