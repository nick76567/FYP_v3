using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAbility : MonoBehaviour
{
    public Image healthBar;
    public enum Team { none, blue, red };

    private float startHP;
    private int hp;
    private int mp;
    private int physicalAp;
    private int magicalAp;
    private int physicalDp;
    private int magicalDp;
    private Team team;




    // Use this for initialization
    void Start()
    {
        healthBar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(int _hp, int _mp, int _physicalAp, int _magicalAp, int _physicalDp, int _magicalDp)
    {
        startHP = hp = _hp;
        mp = _mp;
        physicalAp = _physicalAp;
        magicalAp = _magicalAp;
        physicalDp = _physicalDp;
        magicalDp = _magicalDp;
    }

    public void PhysicalDamage(int _ap)
    {
        int damage = (_ap - physicalDp);
        hp = hp - ((damage < 0) ? 0 : damage);
        healthBar.fillAmount = hp / startHP;
        Debug.Log("Phy damage: " + damage);
    }

    public void MagicalDamage(int _ap)
    {
        int damage = (_ap - magicalDp);
        hp = hp - ((damage < 0) ? 0 : damage);
        healthBar.fillAmount = hp / startHP;
        Debug.Log("_ap " + _ap);
        Debug.Log("magicalDp" + magicalDp);
        Debug.Log("Mag damage: " + damage);
    }

    public void SetMP(int _mp)
    {
        mp -= _mp;
    }

    public void SetTeam(Team _team)
    {
        team = _team;
    }

    public int GetHP()
    {
        return hp;
    }

    public int GetMP()
    {
        return mp;
    }

    public int GetPAP()
    {
        return physicalAp;
    }

    public int GetMAP()
    {
        return magicalAp;
    }

    public int GetPDP()
    {
        return physicalDp;
    }

    public int GetMDP()
    {
        return magicalDp;
    }

    public Team GetTeam()
    {
        return team;
    }
}
