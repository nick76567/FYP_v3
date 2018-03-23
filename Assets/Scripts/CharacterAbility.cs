using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbility : MonoBehaviour
{

    public enum Team { none, blue, red };

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
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(int _hp, int _mp, int _physicalAp, int _magicalAp, int _physicalDp, int _magicalDp)
    {
        hp = _hp;
        mp = _mp;
        physicalAp = _physicalAp;
        magicalAp = _magicalAp;
        physicalDp = _physicalDp;
        magicalDp = _magicalDp;
    }

    public void PhysicalDamage(int _ap)
    {

        hp = hp - (_ap - physicalDp);
        Debug.Log("Phy damage: " + (_ap - physicalDp));
    }

    public void MagicalDamage(int _ap)
    {

        hp = hp - (_ap - magicalDp);

        Debug.Log("_ap " + _ap);
        Debug.Log("magicalDp" + magicalDp);
        Debug.Log("Mag damage: " + (_ap - magicalDp));
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
