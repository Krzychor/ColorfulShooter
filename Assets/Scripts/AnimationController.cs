using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

    private Animator animator;
    private bool faceOnRight;

    void Start () {
        animator = gameObject.GetComponent<Animator>();
        faceOnRight = true;
    }

    public void FlipPlayerDirection () {
        float movement = Input.GetAxis("Horizontal");
        if (movement > 0 && !faceOnRight || movement < 0 && faceOnRight)
        {
            faceOnRight = !faceOnRight;
            Vector3 flipFace = transform.localScale;
            flipFace.x *= -1;
            transform.localScale = flipFace;
        }
    }

    public void Jumping () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Jumping", true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("Jumping", false);
        }
    }
    
    public void Running () {
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
		{
			animator.SetBool("Running", true);
		}
		if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
		{
			animator.SetBool("Running", false);
		}
    }
    
}
