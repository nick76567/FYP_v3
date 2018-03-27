using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallManager : Photon.PunBehaviour {

    private float initTime;
    private int magicalAp;
    private PunTeams.Team team;
    

    // Use this for initialization
    void Start () {

        if (photonView.isMine)
        {
            Debug.Log("fire ball team " + PhotonNetwork.player.GetTeam());
            team = PhotonNetwork.player.GetTeam();
        }

        initTime = Time.timeSinceLevelLoad;     
        magicalAp = 100;
    }
	
	// Update is called once per frame
	void Update () {
        
        
		if(Time.timeSinceLevelLoad - initTime >= 2f)
        {        
            PhotonNetwork.Destroy(gameObject);
        }
        
	}


    private void OnParticleCollision(GameObject other)
    {
        if (photonView.isMine)
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
                    this.photonView.RPC("RPCOnParticleCollision", PhotonTargets.All, other.name, team);
                    PhotonNetwork.Destroy(gameObject);
                }
            }
            else
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
        

    }

    [PunRPC]
    private void RPCOnParticleCollision(int otherID)
    {
        GameObject other = PhotonView.Find(otherID).gameObject;
        other.GetComponent<CharacterAbility>().MagicalDamage(magicalAp);
        other.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
        

    }

    [PunRPC]
    private void RPCOnParticleCollision(string otherName, PunTeams.Team _team)
    {
        PlanetAbility other = GameObject.Find(otherName).GetComponent<PlanetAbility>();
        other.MagicalDamage(magicalAp);
        if (other.GetHP() <= 0)
            other.SetTeam(_team);
    }

}
