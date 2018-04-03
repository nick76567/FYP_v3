﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntWeaponManager : Photon.PunBehaviour {
    private bool isHitPlayer, isHitPlanet;
    private CharacterAbility characterAbility;
    private Animator animator;
   // private int physicalAp;
    private PunTeams.Team team;


    // Use this for initialization
    void Start () {
        characterAbility = GetComponentInParent<CharacterAbility>();
        //physicalAp = characterAbility.GetPAP();
        team = characterAbility.GetTeam();
        animator = this.GetComponentInParent<Animator>();
        this.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
        
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnTriggerEnter(Collider other)
    {
        if (photonView.isMine)
        {
            
            if (other.tag == "Player" && other.GetComponent<CharacterAbility>().GetTeam() != team && !isHitPlayer)
            {
                isHitPlayer = true;
                Invoke("DisableHitPlayer", 0.5f);

                int otherID = other.gameObject.GetPhotonView().viewID;
                this.photonView.RPC("RPCOnTriggerEnter", PhotonTargets.All, otherID, characterAbility.GetPAP());
            }
            else if (other.tag == "Planet")
            {
                if (other.GetComponent<PlanetAbility>().GetTeam() != team && !isHitPlanet)
                {
                    isHitPlanet = true;
                    Invoke("DisableHitPlanet", 0.5f);
                    string otherName = other.gameObject.name;
                    this.photonView.RPC("RPConTriggerEnter", PhotonTargets.All, other.gameObject.name, team, characterAbility.GetPAP());
                }
            }
        }
        
    }

    [PunRPC]
    private void RPCOnTriggerEnter(int otherID, int physicalAp)
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
