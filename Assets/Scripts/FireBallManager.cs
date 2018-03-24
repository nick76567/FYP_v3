using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallManager : Photon.PunBehaviour {

    private float initTime;
    private int magicalAp;
    private CharacterAbility.Team team;
    

    // Use this for initialization
    void Start () {
        
        initTime = Time.timeSinceLevelLoad;
        
        magicalAp = 100;
    }
	
	// Update is called once per frame
	void Update () {
        
        
		if(Time.timeSinceLevelLoad - initTime >= 2f)
        {
            
            Destroy(gameObject);
        }
        
	}


    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player" && other.GetComponent<CharacterAbility>().GetTeam() != team)
        {
            Debug.Log("particle hit name " + other.name);
            int otherID = other.GetPhotonView().viewID;
            this.photonView.RPC("RPCOnParticleCollision", PhotonTargets.All, otherID);
            PhotonNetwork.Destroy(gameObject);
        }
        else if (other.tag == "Planet" && other.GetComponent<PlanetAbility>().GetTeam() != team)
        {
            if (other.GetComponent<PlanetAbility>().GetTeam() != team)
            {
                this.photonView.RPC("RPCOnParticleCollision", PhotonTargets.All, other.name);
                PhotonNetwork.Destroy(gameObject);
            }
        }
        else
        {
            PhotonNetwork.Destroy(gameObject);
        }

    }

    public void SetTeam(CharacterAbility.Team _team)
    {
        team = _team;
    }

    [PunRPC]
    private void RPCOnParticleCollision(int otherID)
    {
        GameObject other = PhotonView.Find(otherID).gameObject;
        other.GetComponent<CharacterAbility>().MagicalDamage(magicalAp);
        other.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
        
    }

    [PunRPC]
    private void RPCOnParticleCollision(string otherName)
    {
        GameObject.Find(otherName).GetComponent<PlanetAbility>().MagicalDamage(magicalAp);
    }


}
