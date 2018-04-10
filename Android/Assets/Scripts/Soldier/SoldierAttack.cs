using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.EventSystems;

public class SoldierAttack : Photon.PunBehaviour {

    private Animator animator;
    private CharacterAbility characterAbility;
    private double PASpeed, MASpeed;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        characterAbility = GetComponent<CharacterAbility>();
        PASpeed = characterAbility.GetpSpeed();
    }

	//public void OnPointerDown(PointerEventData eventData)
    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            if (Input.GetKey(KeyCode.J))
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

    [PunRPC]
    private void PRCSetpSpeed(float _pSpeed)
    {
        PASpeed = _pSpeed;
        animator.SetFloat("SoldierPASpeed", _pSpeed);
    }
}
