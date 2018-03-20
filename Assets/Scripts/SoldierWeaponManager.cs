using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierWeaponManager : Photon.PunBehaviour {

    private CharacterAbility characterAbility;
    private Animator animator;
    private int physicalAp;

	// Use this for initialization
	void Start () {
        
        animator = GetComponentInParent<Animator>();
        characterAbility = GetComponentInParent<CharacterAbility>();
        physicalAp = characterAbility.GetPAP();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        int otherID = other.gameObject.GetPhotonView().viewID;
        if (other.tag == "Enemy")
        {
            this.photonView.RPC("RPCOnTriggerEnter", PhotonTargets.All, otherID);
        }
    }

    [PunRPC]
    void RPCOnTriggerEnter(int otherID)
    {
        GameObject other = PhotonView.Find(otherID).gameObject;

        if (animator.GetBool("isShortAttack"))
        {
            Debug.Log("Spear hit");

            other.GetComponent<Rigidbody>().AddForce(transform.root.forward * 500);
            other.GetComponent<CharacterAbility>().PhysicalDamage(physicalAp);
        }
        else if (animator.GetBool("isLongAttack"))
        {
            other.GetComponent<Rigidbody>().AddForce(transform.root.right * 500);
            other.GetComponent<CharacterAbility>().PhysicalDamage(physicalAp);
        }
        
    }
}
