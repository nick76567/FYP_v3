using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichAttack : MonoBehaviour {

    private Animator animator;
    private GameObject fireBall, fireWall;
    private bool isLaunchFireBall, isLaunchFireWall;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        fireBall = Resources.Load("FireBall", typeof(GameObject)) as GameObject;
        fireWall = Resources.Load("FireWall", typeof(GameObject)) as GameObject;

        if(fireBall == null || fireWall == null)
        {
            Debug.LogError("particle not found");
        }

        isLaunchFireBall = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.J) && !isLaunchFireBall)
        {
            isLaunchFireBall = true;
            animator.SetBool("isShortAttack", true);
            Invoke("LaunchFireBall", 0.5f);
        }
        else
        {
            animator.SetBool("isShortAttack", false);
        }

        if (Input.GetKey(KeyCode.K) && !isLaunchFireWall)
        {
            isLaunchFireWall = true;
            animator.SetBool("isLongAttack", true);
            Invoke("LaunchFireWall", 0.5f);
        }
        else
        {
            animator.SetBool("isLongAttack", false);
        }
    }


    private void ChangeLaunchFireBallState()
    {
        isLaunchFireBall = !isLaunchFireBall;
    }

    private void ChangeLaunchFireWallState()
    {
        isLaunchFireWall = !isLaunchFireWall;
    }

    private void LaunchFireBall()
    {
        Instantiate(fireBall, transform.position + new Vector3(0, 2, 0), Quaternion.identity).GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
        Invoke("ChangeLaunchFireBallState", 2.0f);
    }

    private void LaunchFireWall()
    {
        Instantiate(fireWall, transform.position + new Vector3(-2, 0, 1), Quaternion.identity);
        Invoke("ChangeLaunchFireWallState", 2.0f);
    }
}
