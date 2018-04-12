using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.EventSystems;

public class SoldierAttack : Photon.PunBehaviour {

    private Animator animator;
    private CharacterAbility characterAbility;
    private double PASpeed, MASpeed;
	private bool isShortAttack, isLongAttack;
    // Use this for initialization
    void Start()
    {	
		isShortAttack = isLongAttack = false;
        animator = GetComponent<Animator>();
        characterAbility = GetComponent<CharacterAbility>();
        PASpeed = characterAbility.GetpSpeed();
    }

	public void DisableShortAttack(){
		animator.SetBool("isShortAttack", false);
		isShortAttack = false;
		Debug.Log ("DisableShortAttack is called");
	}

	public void DisableLongAttack(){
		animator.SetBool("isLongAttack", false);
		isLongAttack = false;
		Debug.Log ("DisableSLongAttack is called");
	}


	//public void OnPointerDown(PointerEventData eventData)
    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            //if (Input.GetKey(KeyCode.J))
			if(isShortAttack)
            {
                if (PASpeed != characterAbility.GetpSpeed())
                {
                    this.photonView.RPC("PRCSetpSpeed", PhotonTargets.All, (float)characterAbility.GetpSpeed());
                }
                animator.SetBool("isShortAttack", true);
            }
            else
            {
                animator.SetBool("isShortAttack", false);
            }

            //if (Input.GetKey(KeyCode.K))
			if(isLongAttack)
            {
                animator.SetBool("isLongAttack", true);
            }
            else
            {
                animator.SetBool("isLongAttack", false);
            }
        }
    }

	public void ShortAttack()
	{
		isShortAttack = true;
		Debug.Log ("isShortAttack is called " + isShortAttack );
	}

	public void LongAttack()
	{
		isLongAttack = true;
		Debug.Log ("isLongAttack is called " + isLongAttack);
	}


    [PunRPC]
    private void PRCSetpSpeed(float _pSpeed)
    {
        PASpeed = _pSpeed;
        animator.SetFloat("SoldierPASpeed", _pSpeed);
    }
}
