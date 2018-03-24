using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichAttack : Photon.PunBehaviour{

    private CharacterAbility.Team team;
    private Animator animator;
    private GameObject fireBall, fireWall;
    private bool isLaunchFireBall, isLaunchFireWall;
    private bool isStopLaunchFireBall, isStopLaunchFireWall;

    // Use this for initialization
    void Start()
    {
        team = GetComponent<CharacterAbility>().GetTeam();
        animator = GetComponent<Animator>();
        fireBall = Resources.Load("FireBall", typeof(GameObject)) as GameObject;
        fireWall = Resources.Load("FireWall", typeof(GameObject)) as GameObject;
        
        if(fireBall == null || fireWall == null)
        {
            Debug.LogError("particle not found");
        }



        isLaunchFireBall = false;
        isLaunchFireWall = false;
        isStopLaunchFireBall = true;
        isStopLaunchFireWall = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            if (Input.GetKey(KeyCode.J) && !isLaunchFireBall && !isLaunchFireWall)
            {
                Debug.Log("Lich fire ball");
                isLaunchFireBall = true;
                /*
                animator.SetBool("isShortAttack", true);
                Invoke("LaunchFireBall", 0.5f);
                */
                int id = PhotonNetwork.Instantiate("FireBall", transform.position + (transform.forward * 2) + new Vector3(0, 2, 0), transform.rotation, 0).GetPhotonView().viewID;
                this.photonView.RPC("RPCLaunchFireBall", PhotonTargets.All, team, id);
                isStopLaunchFireBall = false;
                
            }
            else if( !isStopLaunchFireBall &&  !isLaunchFireBall)
            {
                //animator.SetBool("isShortAttack", false);
                this.photonView.RPC("RPCStopLaunchFireBall", PhotonTargets.All);
                isStopLaunchFireBall = true;
                
            }

            if (Input.GetKey(KeyCode.K) && !isLaunchFireWall && !isLaunchFireBall)
            {
                Debug.Log("Lich fire wall");
                isLaunchFireWall = true;
                /*
                animator.SetBool("isLongAttack", true);
                Invoke("LaunchFireWall", 0.5f);
                */
                int id = PhotonNetwork.Instantiate("FireWall", transform.position + (transform.forward * 2), transform.rotation, 0).GetPhotonView().viewID;
                this.photonView.RPC("RPCLaunchFireWall", PhotonTargets.All, team, id);
                isStopLaunchFireWall = false;
                
            }
            else if( !isStopLaunchFireWall &&  !isLaunchFireWall)
            {
                //animator.SetBool("isLongAttack", false);
                this.photonView.RPC("RPCStopLaunchFireWall", PhotonTargets.All);
                isStopLaunchFireWall = true;
                
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

/*    
    private void LaunchFireBall()
    {
        //isLaunchFireBall = true;
        animator.SetBool("isShortAttack", true);

        Instantiate(fireBall, transform.position + new Vector3(0, 2, 0), transform.rotation).GetComponent<Rigidbody>().AddForce(transform.forward * 1000);

        Invoke("ChangeLaunchFireBallState", 1.0f);
    }

 
    private void LaunchFireWall()
    {
        //isLaunchFireWall = true;
        animator.SetBool("isLongAttack", true);
       
        Instantiate(fireWall, transform.position + (transform.forward * 2), transform.rotation) ;
        //fireWall2.Launch();
        Invoke("ChangeLaunchFireWallState", 2.0f);
    }
*/
    [PunRPC]
    private void RPCLaunchFireBall(CharacterAbility.Team team, int id)
    {
        //isLaunchFireBall = true;
        animator.SetBool("isShortAttack", true);

        GameObject tmp = PhotonView.Find(id).gameObject;
        tmp.GetComponent<Rigidbody>().AddForce(transform.forward * 700);
        tmp.GetComponent<FireBallManager>().SetTeam(team);

        Invoke("ChangeLaunchFireBallState", 1.0f);
    }

    [PunRPC]
    private void RPCLaunchFireWall(CharacterAbility.Team team, int id)
    {
        //isLaunchFireWall = true;
        animator.SetBool("isLongAttack", true);

        GameObject tmp = PhotonView.Find(id).gameObject;
        tmp.GetComponent<FireWallManager>().SetTeam(team);
       
        

        //fireWall2.Launch();
        Invoke("ChangeLaunchFireWallState", 2.0f);
    }

    [PunRPC]
    private void RPCStopLaunchFireBall()
    {
        animator.SetBool("isShortAttack", false);
    }

    [PunRPC]
    private void RPCStopLaunchFireWall()
    {
        animator.SetBool("isLongAttack", false);
    }

}
