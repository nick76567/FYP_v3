using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntWeaponManager : MonoBehaviour {

    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = this.GetComponentInParent<Animator>();
        this.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
        
	}
	
	// Update is called once per frame
	void Update () {
        
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {

            if (animator.GetBool("isLongAttack"))
            {
                other.GetComponent<Rigidbody>().AddForce(transform.root.right * -1000);
                Debug.Log("ontriggerEnter long " + other.name + " " + transform.root.right);
            }
            else if (animator.GetBool("isShortAttack"))
            {
                other.GetComponent<Rigidbody>().AddForce(transform.root.forward * 700);
                Debug.Log("ontriggerEnter short " + other.name);
            }
        }
    }
}
