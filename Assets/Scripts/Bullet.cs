using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : ObjectColor {


    public int Bulletspeed = 1500;
    private static float maxlifetime = 5;
    private float lifetime = 0;
    private static int maxBulletSpeed;


    public void ColorChange(Color BulletColor)
    {

        GetComponent<SpriteRenderer>().color = BulletColor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player P = collision.gameObject.GetComponent<Player>();

        if (!collision.gameObject.CompareTag("Coin"))
            if (collision.gameObject.GetComponent<ObjectColor>() == null || collision.gameObject.GetComponent<ObjectColor>().GetColor() != GetComponent<SpriteRenderer>().color)
            {
                Destroy(gameObject);
            }
    }


    // Use this for initialization
    void Start ()
    {
        maxBulletSpeed = Bulletspeed;

        Physics2D.IgnoreCollision(Player.Single.Coll, GetComponent<BoxCollider2D>());
    }

    private void FixedUpdate()
    {
        if(GetComponent<Rigidbody2D>().velocity.magnitude != maxBulletSpeed)
        {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * maxBulletSpeed;
        }

    }

    // Update is called once per frame
    void Update ()
    {

        lifetime += Time.deltaTime;
        
        Physics2D.IgnoreCollision(Player.Single.Coll, GetComponent<BoxCollider2D>(), false);
        

		if(lifetime > maxlifetime)
        {

            Destroy(gameObject);
        }
	}

    public override Color GetColor()
    {
        return GetComponent<SpriteRenderer>().color;
    }
}
