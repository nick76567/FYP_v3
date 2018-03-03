using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour {

    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.J))
        {
            animator.SetBool("isShortAttack", true);
        }
        else
        {
            animator.SetBool("isShortAttack", false); 
        }

        if (Input.GetKey(KeyCode.K))
        {
            animator.SetBool("isLongAttack", true);
        }
        else
        {
            animator.SetBool("isLongAttack", false);
        }
	}
}
