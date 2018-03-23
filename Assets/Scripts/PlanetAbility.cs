using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetAbility : MonoBehaviour {



    private int hp;
    private int physicalDp;
    private int magicalDp;
    private CharacterAbility.Team team;


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
        hp = _hp;
        physicalDp = _physicalDp;
        magicalDp = _magicalDp;
        team = CharacterAbility.Team.none;
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

    public void SetTeam(CharacterAbility.Team _team)
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

    public CharacterAbility.Team GetTeam()
    {
        return team;
    }
}
