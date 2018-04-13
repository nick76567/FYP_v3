using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetAbility : Photon.PunBehaviour {

    public Image healthBar;
    public GameObject blueParticleRing;
    public GameObject redParticleRing;

    private float startHP;
    private int hp;
    private int physicalDp;
    private int magicalDp;
    private PunTeams.Team team;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(int _hp, int _physicalDp, int _magicalDp)
    {
        startHP = hp = _hp;
        physicalDp = _physicalDp;
        magicalDp = _magicalDp;
        team = PunTeams.Team.none;
    }

    public void PhysicalDamage(int _ap)
    {

        int damage = (_ap - physicalDp);
        this.photonView.RPC("RPCPhysicalDamage", PhotonTargets.All, ((damage < 0) ? 0 : damage));
        //hp = hp - ((damage < 0) ? 0 : damage);
        //healthBar.fillAmount = hp / startHP;
        //Debug.Log("Phy damage: " + (_ap - physicalDp));
    }

    public void MagicalDamage(int _ap)
    {

        int damage = (_ap - magicalDp);
        this.photonView.RPC("RPCMagicalDamage", PhotonTargets.All, ((damage < 0) ? 0 : damage));
        //hp = hp - ((damage < 0) ? 0 : damage);
        //healthBar.fillAmount = hp / startHP;

        //Debug.Log("_ap " + _ap);
        //Debug.Log("magicalDp" + magicalDp);
        //Debug.Log("Mag damage: " + (_ap - magicalDp));
    }

    public void SetTeam(PunTeams.Team _team)
    {
        team = _team;
        if(_team == PunTeams.Team.red)
        {
            redParticleRing.SetActive(true);
            blueParticleRing.SetActive(false);
        }
        else
        {
            redParticleRing.SetActive(false);
            blueParticleRing.SetActive(true);
        }

        SetHP((int)startHP);
        healthBar.fillAmount = 1;

    }

    public void SetHP(int _hp)
    {
        hp = _hp;
    }

    public int GetHP()
    {
        return hp;
    }

    public int GetPDP()
    {
        return physicalDp;
    }

    public int GetMDP()
    {
        return magicalDp;
    }

    public PunTeams.Team GetTeam()
    {
        return team;
    }

    [PunRPC]
    public void RPCPhysicalDamage(int _ap)
    {
        hp = hp - _ap;
        healthBar.fillAmount = hp / startHP;
        if(photonView.isMine)
            Debug.Log("Phy damage: " + _ap);
    }

    [PunRPC]
    public void RPCMagicalDamage(int _ap)
    {
        hp = hp - _ap;
        healthBar.fillAmount = hp / startHP;

        Debug.Log("_ap " + _ap);
        Debug.Log("magicalDp" + magicalDp);
        Debug.Log("Mag damage: " + (_ap - magicalDp));
    }
}
