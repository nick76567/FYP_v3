using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemSmokeParticleManager : Photon.PunBehaviour {

    private CharacterAbility characterAbility;
    private int magicalAp;
    private PunTeams.Team team;

    // Use this for initialization
    void Start () {
        characterAbility = GetComponentInParent<CharacterAbility>();
        magicalAp = characterAbility.GetMAP();
        team = characterAbility.GetTeam();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnParticleCollision(GameObject other)
    {
        if (photonView.isMine)
        {
            if (other.tag == "Player" && other.GetComponent<CharacterAbility>().GetTeam() != team)
            {
                Debug.Log("particle hit name " + other.name);
                int otherID = other.GetPhotonView().viewID;
                this.photonView.RPC("RPCOnTriggerEnter", PhotonTargets.All, otherID);
            }
            else if (other.tag == "Planet")
            {
                if (other.GetComponent<PlanetAbility>().GetTeam() != team)
                {
                    this.photonView.RPC("RPConTriggerEnter", PhotonTargets.All, other.name);
                }
            }
        }
        
    }

    [PunRPC]
    private void RPCOnTriggerEnter(int otherID)
    {
        GameObject other = PhotonView.Find(otherID).gameObject;
        other.GetComponent<CharacterAbility>().MagicalDamage(magicalAp);
    }

    [PunRPC]
    private void RPConTriggerEnter(string otherName)
    {
        Debug.Log("Golem Planet Hit");
        GameObject other = GameObject.Find(otherName);

        PlanetAbility planetAbility = other.GetComponent<PlanetAbility>();
        planetAbility.MagicalDamage(magicalAp);
        if (planetAbility.GetHP() <= 0)
        {
            planetAbility.SetTeam(team);
            Debug.Log("RPCContrig Planet team " + planetAbility.GetTeam());
        }                 
    }
}
