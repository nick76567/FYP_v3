using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemWeaponManager : Photon.PunBehaviour {

    private bool isHitPlayer, isHitPlanet;
    private Animator animator;
    private CharacterAbility characterAbility;
    //private int physicalAp;
    private PunTeams.Team team;

	// Use this for initialization
	void Start () {
        animator = GetComponentInParent<Animator>();
        characterAbility = GetComponentInParent<CharacterAbility>();
        team = characterAbility.GetTeam();
        isHitPlanet = isHitPlayer = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void DisableHitPlayer()
    {
        isHitPlayer = false;
    }

    private void DisableHitPlanet()
    {
        isHitPlanet = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (photonView.isMine)
        {
            if (animator.GetBool("isShortAttack") && other.tag == "Player" && other.GetComponent<CharacterAbility>().GetTeam() != team && !isHitPlayer)
            {
                isHitPlayer = true;
                Invoke("DisableHitPlayer", 0.5f);
                Debug.Log("OnTriggerEnter");
                int otherID = other.gameObject.GetPhotonView().viewID;
                other.GetComponent<CharacterAbility>().PhysicalDamage(characterAbility.GetPAP());
                this.photonView.RPC("RPCOnTriggerEnter", PhotonTargets.All, otherID);

                if(other.GetComponent<CharacterAbility>().GetHP() <= 0)
                {
                    characterAbility.AddCoins(CharacterAbility.REWARD);
                }

            }
            else if (other.tag == "Planet")
            {
                if (animator.GetBool("isShortAttack") && other.GetComponent<PlanetAbility>().GetTeam() != team && !isHitPlanet)
                {
                    isHitPlanet = true;
                    Invoke("DisableHitPlanet", 0.5f);
                    string otherName = other.gameObject.name;
                    other.GetComponent<PlanetAbility>().PhysicalDamage(characterAbility.GetPAP());
                    this.photonView.RPC("RPCOnTriggerEnter", PhotonTargets.All, other.gameObject.name, team);
                }
            }
        }
        
    }

    [PunRPC]
    private void RPCOnTriggerEnter(int otherID)
    {
        if (animator.GetBool("isShortAttack"))
        {
            Debug.Log("Golem hit");
            GameObject other = PhotonView.Find(otherID).gameObject;
            other.GetComponent<Rigidbody>().AddForce(transform.root.forward * 700);
        }
    }

    [PunRPC]
    private void RPCOnTriggerEnter(string otherName, PunTeams.Team _team)
    {
        if (animator.GetBool("isShortAttack"))
        {
            Debug.Log("Golem Planet Hit");
            GameObject other = GameObject.Find(otherName);
            if (other == null)
                Debug.Log("Planet Orange not found");

            PlanetAbility planetAbility = other.GetComponent<PlanetAbility>();

            if(planetAbility.GetHP() <= 0)
            {
               planetAbility.SetTeam(_team);
                Debug.Log("RPCContrig Planet team " + planetAbility.GetTeam());
            }

        }
    }
}
