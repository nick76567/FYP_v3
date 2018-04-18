using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetIceManager : Photon.PunBehaviour
{

    private const int HP = 600, PDP = 10, MDP = 30;
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
