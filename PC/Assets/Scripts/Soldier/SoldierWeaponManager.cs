using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierWeaponManager : Photon.PunBehaviour {

    private bool isHitPlayer, isHitPlanet;
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
        isHitPlanet = isHitPlayer = false;
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
            if ((animator.GetBool("isShortAttack") || animator.GetBool("isLongAttack")) && other.tag == "Player" && other.GetComponent<CharacterAbility>().GetTeam() != team && !isHitPlayer)
            {
                isHitPlayer = true;
                Invoke("DisableHitPlayer", 0.5f);
                int otherID = other.gameObject.GetPhotonView().viewID;
                other.GetComponent<CharacterAbility>().PhysicalDamage(characterAbility.GetPAP());
                this.photonView.RPC("RPCOnTriggerEnter", PhotonTargets.All, otherID);

                if (other.GetComponent<CharacterAbility>().GetHP() <= 0)
                {
                    characterAbility.AddCoins(CharacterAbility.REWARD);
                }

            }
            else if (other.tag == "Planet")
            {
                if ((animator.GetBool("isShortAttack") || animator.GetBool("isLongAttack")) && other.GetComponent<PlanetAbility>().GetTeam() != team && !isHitPlanet)
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
    void RPCOnTriggerEnter(int otherID)
    {
        GameObject other = PhotonView.Find(otherID).gameObject;

        if (animator.GetBool("isShortAttack"))
        {
            //Debug.Log("Spear hit");

            other.GetComponent<Rigidbody>().AddForce(transform.root.forward * 500);
            //other.GetComponent<CharacterAbility>().PhysicalDamage(physicalAp);
        }
        else if (animator.GetBool("isLongAttack"))
        {
            other.GetComponent<Rigidbody>().AddForce(transform.root.right * 500);
            //other.GetComponent<CharacterAbility>().PhysicalDamage(physicalAp);
        }

    }

    [PunRPC]
    private void RPCOnTriggerEnter(string otherName, PunTeams.Team _team)
    {
        if (animator.GetBool("isShortAttack") || animator.GetBool("isLongAttack"))
        {
            //Debug.Log("Soldier Planet Hit");
            GameObject other = GameObject.Find(otherName);

            PlanetAbility planetAbility = other.GetComponent<PlanetAbility>();
            //planetAbility.PhysicalDamage(physicalAp);
            if (planetAbility.GetHP() <= 0)
            {
                planetAbility.SetTeam(_team);
                Debug.Log("RPCContrig Planet team " + planetAbility.GetTeam());
            }

        }
    }
}
