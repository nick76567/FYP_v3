using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetAbility : MonoBehaviour {

    public Image healthBar;

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
        hp = hp - ((damage < 0) ? 0 : damage);
        healthBar.fillAmount = hp / startHP;
        Debug.Log("Phy damage: " + (_ap - physicalDp));
    }

    public void MagicalDamage(int _ap)
    {

        int damage = (_ap - magicalDp);
        hp = hp - ((damage < 0) ? 0 : damage);
        healthBar.fillAmount = hp / startHP;

        Debug.Log("_ap " + _ap);
        Debug.Log("magicalDp" + magicalDp);
        Debug.Log("Mag damage: " + (_ap - magicalDp));
    }

    public void SetTeam(PunTeams.Team _team)
    {
        team = _team;
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
}
