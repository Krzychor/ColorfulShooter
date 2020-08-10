using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Player"){
 
            var scene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(scene.name);
       }
    }
}
