using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierWeaponManager : Photon.PunBehaviour {

    private CharacterAbility characterAbility;
    private Animator animator;
    private int physicalAp;
    private CharacterAbility.Team team;

	// Use this for initialization
	void Start () {
        team = GetComponentInParent<CharacterAbility>().GetTeam();
        animator = GetComponentInParent<Animator>();
        characterAbility = GetComponentInParent<CharacterAbility>();
        physicalAp = characterAbility.GetPAP();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
/*
    private void OnTriggerEnter(Collider other)
    {
        int otherID = other.gameObject.GetPhotonView().viewID;
        if (other.tag == "Enemy")
        {
            this.photonView.RPC("RPCOnTriggerEnter", PhotonTargets.All, otherID);
        }
    }
*/

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player" && other.GetComponent<CharacterAbility>().GetTeam() != team)
        {
            int otherID = other.gameObject.GetPhotonView().viewID;
            this.photonView.RPC("RPCOnTriggerEnter", PhotonTargets.All, otherID);
        }
        else if (other.tag == "Planet")
        {
            if (other.GetComponent<PlanetAbility>().GetTeam() != team)
            {
                string otherName = other.gameObject.name;
                this.photonView.RPC("RPConTriggerEnter", PhotonTargets.All, other.gameObject.name);
            }
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

    [PunRPC]
    private void RPConTriggerEnter(string otherName)
    {
        if (animator.GetBool("isShortAttack") || animator.GetBool("isLongAttack"))
        {
            Debug.Log("Grunt Planet Hit");
            GameObject other = GameObject.Find(otherName);

            PlanetAbility planetAbility = other.GetComponent<PlanetAbility>();
            planetAbility.PhysicalDamage(physicalAp);
            if (planetAbility.GetHP() <= 0)
            {
                planetAbility.SetTeam(team);
                Debug.Log("RPCContrig Planet team " + planetAbility.GetTeam());
            }

        }
    }
}
