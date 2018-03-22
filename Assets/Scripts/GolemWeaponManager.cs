using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemWeaponManager : Photon.PunBehaviour {

    private Animator animator;
    private CharacterAbility characterAbility;
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
        if(other.tag == "Enemy")
        {
            int otherID = other.gameObject.GetPhotonView().viewID;
            this.photonView.RPC("RPCOntriggerEnter", PhotonTargets.All, otherID);
        }
    }

    [PunRPC]
    private void RPCOnTriggerEnter(int otherID)
    {
        if (animator.GetBool("isShortAttack"))
        {
            Debug.Log("Golem hit");
            GameObject other = PhotonView.Find(otherID).gameObject;
            other.GetComponent<Rigidbody>().AddForce(transform.root.forward * 500);
            other.GetComponent<CharacterAbility>().PhysicalDamage(physicalAp);
        }
    }
}
