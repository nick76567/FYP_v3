using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetOrangeManager : Photon.PunBehaviour {

    private const int HP = 500, PDP = 10, MDP = 10;
    private PlanetAbility planetAbility;

	// Use this for initialization
	void Start () {
        planetAbility = GetComponent<PlanetAbility>();
        planetAbility.Init(HP, PDP, MDP);
	}

    // Update is called once per frame
    void Update()
    {
        //if (planetAbility.GetHP() <= 0)
        //{

        //    //planetAbility.SetTeam((planetAbility.GetTeam() == CharacterAbility.Team.blue)? CharacterAbility.Team.red : CharacterAbility.Team.blue);
        //    planetAbility.SetHP(HP);
        //    Debug.Log("PlanetOrange Team " + planetAbility.GetTeam());
            
        //}
    }    
	
}
