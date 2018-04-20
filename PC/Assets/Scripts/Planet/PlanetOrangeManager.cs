using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetOrangeManager : Photon.PunBehaviour {

    private const int HP = 500, PDP = 20, MDP = 20;
    private PlanetAbility planetAbility;

	// Use this for initialization
	void Start () {
        planetAbility = GetComponent<PlanetAbility>();
        planetAbility.Init(HP, PDP, MDP);
	}

    // Update is called once per frame
    void Update()
    {

    }    
	
}
