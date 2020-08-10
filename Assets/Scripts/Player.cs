using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : ObjectColor
{
    //public access point to player object
    public static Player Single;
    private Rigidbody2D Rigid;
    public UIInfoManager UI;
    public BoxCollider2D Coll;
    public CapsuleCollider2D trig;
    public List<Coin> coinArray;

    AnimationController playerAnimationController;

    //moving  
    private bool touch = false;

    public float speed;
    public float jumpforce;
    private static readonly float maxspeed = 2000;

    public float hp = 100;
    public static float maxHp = 200;
    public int coins = 0;

    //shooting
    public GameObject BulletPrefab;
    private static readonly float cooldown = 0.3f;
    private float LastShoot = 0;
    private Vector3 target;

    //color
    public List<Color> color;
    public Color MainColor;


    private void changeHp(float newHp)
    {
        hp = newHp;
        UI.DisplayHp(hp);

        if(hp <= 0)
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    private void counter()
    {
        coins++;
        UI.DisplayCoins(coins);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Platform"))
            Coll.enabled = false;
        

        if (collider.CompareTag("Coin"))
            counter();
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        Coll.enabled = true;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && MainColor != collision.gameObject.GetComponent<SpriteRenderer>().color)
            hp -= 50;
        else if (collision.gameObject.CompareTag("Bullet") && MainColor == collision.gameObject.GetComponent<SpriteRenderer>().color)
        {
            Rigid.AddForce(GetComponent<Rigidbody2D>().velocity.normalized*jumpforce, ForceMode2D.Impulse);
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (0 < collision.contacts[0].normal.y)
            touch = true;
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        touch = false;
    }
    

    public void Shoot()
    {
        Vector3 Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 Dir = Target - transform.position;
        Dir = Dir.normalized;

        GameObject B = Instantiate(BulletPrefab, transform.position, Quaternion.identity);

        B.GetComponent<Bullet>().ColorChange(MainColor);

        Rigidbody2D BRigid = B.GetComponent<Rigidbody2D>();

        BRigid.velocity = Dir * B.GetComponent<Bullet>().Bulletspeed;
    }

    private void Start()
    {
        for (int i = 0; i < coinArray.Count; i++)
        {
            Physics2D.IgnoreCollision(trig, coinArray[i].GetComponent<Collider2D>());
        }


        playerAnimationController = gameObject.AddComponent<AnimationController>();
    }


    void colorChoice()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {

            Single.MainColor = Single.color[0];
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {

            Single.MainColor = Single.color[1];
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {

            Single.MainColor = Single.color[2];
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {

            Single.MainColor = Single.color[3];
        }
    }

    void moving()
    {
        if (Input.GetKeyDown(KeyCode.D) && Rigid.velocity.x == 0)
        {
            Rigid.AddForce(new Vector2(speed * 1000 * 0.15f, 0));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Rigid.AddForce(new Vector2(speed * 1000 * Time.deltaTime, 0));
        }

        if (Input.GetKeyDown(KeyCode.A) && Rigid.velocity.x == 0)
        {
            Rigid.AddForce(new Vector2(-speed * 1000 * 0.15f, 0));
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Rigid.AddForce(new Vector2(-speed * 1000 * Time.deltaTime, 0));
        }
        if (Input.GetKeyDown(KeyCode.Space) && touch)
        {
            Rigid.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
        }
    }

    //antiNull
    private void Awake()
    {

        Single = this;
        MainColor = Single.color[0];
        changeHp(maxHp);
        Coll = GetComponent<BoxCollider2D>();
        trig = GetComponent<CapsuleCollider2D>();
        Rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        colorChoice();

        changeHp(hp);

        target = (Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position).normalized;
        DrawLine(MainColor, transform.position, transform.position+target*300);
        
        //speed control
        if (Rigid.velocity.x > maxspeed)
        {
            Rigid.velocity = new Vector2(maxspeed, Rigid.velocity.y);
        }
        else if (Rigid.velocity.x < -maxspeed)
        {
            Rigid.velocity = new Vector2(-maxspeed, Rigid.velocity.y);
        }

    }


    private void Update()
    {
        //jumping test
        playerAnimationController.Jumping();

        //running test
        playerAnimationController.Running();

        //flip player direction test
        playerAnimationController.FlipPlayerDirection();

        moving();
        

        if (LastShoot > cooldown)
        {
            if (Input.GetMouseButton(0))
            {

                LastShoot = 0;
                Shoot();
            }
        }
        else
            LastShoot += Time.deltaTime;

    }

    void DrawLine(Color color, Vector2 Start, Vector2 End)
    {
        LineRenderer lr = GetComponent<LineRenderer>();
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = 3f;
        lr.endWidth = 3f;
        lr.SetPosition(0, Start);
        lr.SetPosition(1, End);
    }

    public override Color GetColor()
    {
        return MainColor;
    }
}



