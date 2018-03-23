using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetIceManager : Photon.PunBehaviour
{

    private const int HP = 1000, PDP = 10, MDP = 100;
    private PlanetAbility planetAbility;

    // Use this for initialization
    void Start()
    {
        planetAbility = GetComponent<PlanetAbility>();
        planetAbility.Init(HP, PDP, MDP);
    }

    // Update is called once per frame
    void Update()
    {
        if (planetAbility.GetHP() <= 0)
        {
            //planetAbility.SetTeam((planetAbility.GetTeam() == CharacterAbility.Team.blue) ? CharacterAbility.Team.red : CharacterAbility.Team.blue);
            planetAbility.SetHP(HP);
            Debug.Log("PlanetIce isBlueTeam " + planetAbility.GetTeam());
        }
    }

}
