using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierWeaponManager : Photon.PunBehaviour {

    private CharacterAbility characterAbility;
    private Animator animator;
    //private int physicalAp;
    private PunTeams.Team team;

	// Use this for initialization
	void Start () {
        team = GetComponentInParent<CharacterAbility>().GetTeam();
        animator = GetComponentInParent<Animator>();
        characterAbility = GetComponentInParent<CharacterAbility>();
        //physicalAp = characterAbility.GetPAP();
      
	}
	

    private void OnTriggerEnter(Collider other)
    {
        if (photonView.isMine)
        {
            if (other.tag == "Player" && other.GetComponent<CharacterAbility>().GetTeam() != team)
            {
                int otherID = other.gameObject.GetPhotonView().viewID;
                this.photonView.RPC("RPCOnTriggerEnter", PhotonTargets.All, otherID, characterAbility.GetPAP());
            }
            else if (other.tag == "Planet")
            {
                if (other.GetComponent<PlanetAbility>().GetTeam() != team)
                {
                    string otherName = other.gameObject.name;
                    this.photonView.RPC("RPConTriggerEnter", PhotonTargets.All, other.gameObject.name, team, characterAbility.GetPAP());
                }
            }
        }
        
    }

    [PunRPC]
    void RPCOnTriggerEnter(int otherID, int physicalAp)
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
    private void RPConTriggerEnter(string otherName, PunTeams.Team _team, int physicalAp)
    {
        if (animator.GetBool("isShortAttack") || animator.GetBool("isLongAttack"))
        {
            Debug.Log("Grunt Planet Hit");
            GameObject other = GameObject.Find(otherName);

            PlanetAbility planetAbility = other.GetComponent<PlanetAbility>();
            planetAbility.PhysicalDamage(physicalAp);
            if (planetAbility.GetHP() <= 0)
            {
                planetAbility.SetTeam(_team);
                Debug.Log("RPCContrig Planet team " + planetAbility.GetTeam());
            }

        }
    }
}
