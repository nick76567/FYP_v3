using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntWeaponManager : Photon.PunBehaviour {

    private CharacterAbility characterAbility;
    private Animator animator;
    private int physicalAp;


    // Use this for initialization
    void Start () {
        characterAbility = GetComponentInParent<CharacterAbility>();
        physicalAp = characterAbility.GetPAP();
        animator = this.GetComponentInParent<Animator>();
        this.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
        
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
    private void RPCOnTriggerEnter(int otherID)
    {
        GameObject other = PhotonView.Find(otherID).gameObject;
        if (animator.GetBool("isLongAttack"))
        {
            other.GetComponent<Rigidbody>().AddForce(transform.root.right * -1000);
            Debug.Log("ontriggerEnter long " + other.name + " " + transform.root.right);
            other.GetComponent<CharacterAbility>().PhysicalDamage(physicalAp);
        }
        else if (animator.GetBool("isShortAttack"))
        {
            other.GetComponent<Rigidbody>().AddForce(transform.root.forward * 700);
            Debug.Log("ontriggerEnter short " + other.name);
            other.GetComponent<CharacterAbility>().PhysicalDamage(physicalAp);
        }  
    }
}
