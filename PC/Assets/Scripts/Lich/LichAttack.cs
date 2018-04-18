using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichAttack : Photon.PunBehaviour{

    private Animator animator;
    private GameObject fireBall, fireWall;
    private bool isLaunchFireBall, isLaunchFireWall;
    private bool isStopLaunchFireBall, isStopLaunchFireWall;
    private CharacterAbility characterAbility;

    // Use this for initialization
    void Start()
    {	
        animator = GetComponent<Animator>();
        characterAbility = GetComponent<CharacterAbility>();
        fireBall = Resources.Load("FireBall", typeof(GameObject)) as GameObject;
        fireWall = Resources.Load("FireWall", typeof(GameObject)) as GameObject;
        
        if(fireBall == null || fireWall == null)
        {
            Debug.LogError("particle not found");
        }

        isLaunchFireBall = isLaunchFireWall = false;
        isStopLaunchFireBall = isStopLaunchFireWall = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            if (Input.GetKey(KeyCode.J) && !isLaunchFireBall && !isLaunchFireWall)
            {
                animator.SetBool("isShortAttack", true);
                isLaunchFireBall = true;

                Invoke("LaunchFireBall", 0.4f);
                Invoke("ChangeStopFireBallState", 0.7f);
                
            }
            else if( !isStopLaunchFireBall)
            {
                animator.SetBool("isShortAttack", false);
                ChangeStopFireBallState();
            }

            if (Input.GetKey(KeyCode.K) && !isLaunchFireWall && !isLaunchFireBall)
            {
                animator.SetBool("isLongAttack", true);
                isLaunchFireWall = true;

                Invoke("LaunchFireWall", 0.4f);
                Invoke("ChangeStopFireWallState", 1f);
            }
            else if( !isStopLaunchFireWall)
            {
                animator.SetBool("isLongAttack", false);
                ChangeStopFireWallState();
            }
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

    private void ChangeStopFireBallState()
    {
        isStopLaunchFireBall = !isStopLaunchFireBall;
    }

    private void ChangeStopFireWallState()
    {
        isStopLaunchFireWall = !isStopLaunchFireWall;
    }

    private void LaunchFireBall()
    {
        GameObject lauchFireBall = PhotonNetwork.Instantiate("FireBall", transform.position + transform.forward + new Vector3(0, 2, 0), transform.rotation, 0);
        int id = lauchFireBall.GetPhotonView().viewID;
        lauchFireBall.GetComponent<FireBallManager>().InitFireBall(characterAbility.GetMAP(), this.photonView.viewID);
        this.photonView.RPC("RPCLaunchFireBall", PhotonTargets.All, id);
    }

    private void LaunchFireWall()
    {
        GameObject lauchFireWall = PhotonNetwork.Instantiate("FireWall", transform.position + (transform.forward * 2), transform.rotation, 0);
        int id = lauchFireWall.GetPhotonView().viewID;
        lauchFireWall.GetComponent<FireWallManager>().InitFireWall(characterAbility.GetMAP(), this.photonView.viewID);
        this.photonView.RPC("RPCLaunchFireWall", PhotonTargets.All, id);
    }


    [PunRPC]
    private void RPCLaunchFireBall( int id)
    {
        GameObject tmp = PhotonView.Find(id).gameObject;
        tmp.GetComponent<Rigidbody>().AddForce(transform.forward * 700);

        Invoke("ChangeLaunchFireBallState", 2f);
    }

    [PunRPC]
    private void RPCLaunchFireWall(int id)
    {
        GameObject tmp = PhotonView.Find(id).gameObject;

        Invoke("ChangeLaunchFireWallState", 2.0f);
    }
}
