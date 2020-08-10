using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWall : MonoBehaviour {

    private static int hp = 3;
    private int counter = 0;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        counter++;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        counter++;
    }



    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (counter == hp)
            Destroy(gameObject);
	}
}
