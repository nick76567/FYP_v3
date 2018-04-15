using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetForestManager : Photon.PunBehaviour
{

    private const int HP = 500, PDP = 30, MDP = 10;
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

    }

}
