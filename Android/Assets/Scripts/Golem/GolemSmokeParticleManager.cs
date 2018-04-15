using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemSmokeParticleManager : Photon.PunBehaviour {

    private bool isHitPlayer, isHitPlanet;
    private CharacterAbility characterAbility;
    //private int magicalAp;
    private PunTeams.Team team;

    // Use this for initialization
    void Start () {
        characterAbility = GetComponentInParent<CharacterAbility>();
       // magicalAp = characterAbility.GetMAP();
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

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("particle hit name " + other.name + " hit " + characterAbility.GetMAP());
        if (photonView.isMine)
        {
            if (other.tag == "Player" && other.GetComponent<CharacterAbility>().GetTeam() != team && !isHitPlayer)
            {
                isHitPlayer = true;
                Invoke("DisableHitPlayer", 0.5f);
                Debug.Log("particle hit name " + other.name + " hit " + characterAbility.GetMAP());
                other.GetComponent<CharacterAbility>().MagicalDamage(characterAbility.GetMAP());
                //int otherID = other.GetPhotonView().viewID;
                //this.photonView.RPC("RPCOnTriggerEnter", PhotonTargets.All, otherID, characterAbility.GetMAP());

                if (other.GetComponent<CharacterAbility>().GetHP() <= 0)
                {
                    characterAbility.AddCoins(CharacterAbility.REWARD);
                }
            }
            else if (other.tag == "Planet")
            {
                if (other.GetComponent<PlanetAbility>().GetTeam() != team && !isHitPlanet)
                {
                    isHitPlanet = true;
                    Invoke("DisableHitPlanet", 0.5f);
                    other.GetComponent<PlanetAbility>().MagicalDamage(characterAbility.GetMAP());
                    this.photonView.RPC("RPConTriggerEnter", PhotonTargets.All, other.name, team);
                }
            }
        }
        
    }

    //[PunRPC]
    //private void RPCOnTriggerEnter(int otherID, int magicalAp)
    //{
    //    GameObject other = PhotonView.Find(otherID).gameObject;
    //    other.GetComponent<CharacterAbility>().MagicalDamage(magicalAp);
    //}

    [PunRPC]
    private void RPConTriggerEnter(string otherName, PunTeams.Team _team)
    {
        Debug.Log("Golem Planet Hit");
        GameObject other = GameObject.Find(otherName);

        PlanetAbility planetAbility = other.GetComponent<PlanetAbility>();
        //planetAbility.MagicalDamage(magicalAp);
        if (planetAbility.GetHP() <= 0)
        {
            planetAbility.SetTeam(_team);
            Debug.Log("RPCContrig Planet team " + planetAbility.GetTeam());
        }                 
    }
}
