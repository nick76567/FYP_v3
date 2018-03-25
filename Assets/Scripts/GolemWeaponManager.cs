using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemWeaponManager : Photon.PunBehaviour {

    private Animator animator;
    private CharacterAbility characterAbility;
    private int physicalAp;
    private PunTeams.Team team;

	// Use this for initialization
	void Start () {
        animator = GetComponentInParent<Animator>();
        characterAbility = GetComponentInParent<CharacterAbility>();
        physicalAp = characterAbility.GetPAP();
        team = characterAbility.GetTeam();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (photonView.isMine)
        {
            if (other.tag == "Player" && other.GetComponent<CharacterAbility>().GetTeam() != team)
            {
                Debug.Log("OnTriggerEnter");
                int otherID = other.gameObject.GetPhotonView().viewID;
                this.photonView.RPC("RPConTriggerEnter", PhotonTargets.All, otherID);
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
        
    }

    [PunRPC]
    private void RPConTriggerEnter(int otherID)
    {
        if (animator.GetBool("isShortAttack"))
        {
            Debug.Log("Golem hit");
            GameObject other = PhotonView.Find(otherID).gameObject;
            other.GetComponent<Rigidbody>().AddForce(transform.root.forward * 700);
            other.GetComponent<CharacterAbility>().PhysicalDamage(physicalAp);
        }
    }

    [PunRPC]
    private void RPConTriggerEnter(string otherName)
    {
        if (animator.GetBool("isShortAttack"))
        {
            Debug.Log("Golem Planet Hit");
            GameObject other = GameObject.Find(otherName);
            if (other == null)
                Debug.Log("Planet Orange not found");


            PlanetAbility planetAbility = other.GetComponent<PlanetAbility>();
            planetAbility.PhysicalDamage(physicalAp);
            if(planetAbility.GetHP() <= 0)
            {
               planetAbility.SetTeam(team);
                Debug.Log("RPCContrig Planet team " + planetAbility.GetTeam());
            }

        }
    }
}
