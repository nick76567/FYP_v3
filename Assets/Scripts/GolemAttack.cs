using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAttack : Photon.PunBehaviour {

    private GameObject dustRing;
    private Animator animator;
    private bool isLaunchDustRing;
    private GolemSmokeRingManager smokeRing;

	// Use this for initialization
	void Start () {
        isLaunchDustRing = false;

        animator = GetComponent<Animator>();
        //dustRing = Resources.Load("GolemSmokeRing", typeof(GameObject)) as GameObject;
        smokeRing = GetComponentInChildren<GolemSmokeRingManager>();
        
	}
	
	// Update is called once per frame
	void Update () {
        if (photonView.isMine)
        {
            if (Input.GetKey(KeyCode.J))
            {
                animator.SetBool("isShortAttack", true);
            }
            else
            {
                animator.SetBool("isShortAttack", false);
            }

            if (Input.GetKey(KeyCode.K) && !isLaunchDustRing)
            {
                isLaunchDustRing = true;

                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

                animator.SetBool("isLongAttack", true);
                Invoke("LaunchDustRing", 0.9f);

            }
            else
            {
                animator.SetBool("isLongAttack", false);
            }
        }
    }

    private void LaunchDustRing()
    {
        //Instantiate(dustRing, transform.position, Quaternion.identity);
        smokeRing.Launch();
        Invoke("ChangeIsLaunchDustRingState", 2f);
    }

    private void ChangeIsLaunchDustRingState()
    {
        isLaunchDustRing = false;

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
    }
}
