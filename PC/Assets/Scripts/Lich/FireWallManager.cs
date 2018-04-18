using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallManager : Photon.PunBehaviour
{

    private float initTime;
    private int magicalAp;
    private PunTeams.Team team;
    private bool isHitPlayer, isHitPlanet;
    private int viewID;


    // Use this for initialization
    void Start()
    {
        if (photonView.isMine)
        {
            team = PhotonNetwork.player.GetTeam();
        }

        initTime = Time.timeSinceLevelLoad;
        isHitPlanet = isHitPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.timeSinceLevelLoad - initTime > 2.0f)
        {
            PhotonNetwork.Destroy(gameObject);
        }

    }

    private void DisableHitPlayer()
    {
        isHitPlayer = false;
    }

    private void DisableHitPlanet()
    {
        isHitPlanet = false;
    }

    public void InitFireWall(int _magicalAp, int _viewID)
    {
        viewID = _viewID;
        magicalAp = _magicalAp;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (photonView.isMine)
        {
            if (other.tag == "Player" && other.GetComponent<CharacterAbility>().GetTeam() != team && !isHitPlayer)
            {
                isHitPlayer = true;
                Invoke("DisableHitPlayer", 0.5f);
                Debug.Log("particle hit name " + other.name);
                other.GetComponent<CharacterAbility>().MagicalDamage(magicalAp);

                if (other.GetComponent<CharacterAbility>().GetHP() <= 0)
                {
                    PhotonView.Find(viewID).GetComponent<CharacterAbility>().AddCoins(CharacterAbility.REWARD);
                }
                PhotonNetwork.Destroy(gameObject);
            }
            else if (other.tag == "Planet")
            {
                if (other.GetComponent<PlanetAbility>().GetTeam() != team && !isHitPlanet)
                {
                    isHitPlanet = true;
                    Invoke("DisableHitPlanet", 0.5f);
                    other.GetComponent<PlanetAbility>().MagicalDamage(magicalAp);
                    this.photonView.RPC("RPCOnParticleCollision", PhotonTargets.All, other.name, team);
                    PhotonNetwork.Destroy(gameObject);
                }
            }
        }
    }


    [PunRPC]
    private void RPCOnParticleCollisiion(string otherName, PunTeams.Team _team)
    {
        PlanetAbility other = GameObject.Find(otherName).GetComponent<PlanetAbility>();
        if (other.GetHP() <= 0)
            other.SetTeam(_team);

    }
}
