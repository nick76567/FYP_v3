using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GolemManager : Photon.PunBehaviour {

    private const int HP = 2000, MP = 0, PAP = 150, MAP = 20, PDP = 50, MDP = 5;
    private const float WALK = 10, RUN = WALK + 8;

    private CharacterAbility characterAbility;
    private AudioListener audioListener;
    private Camera otherCamera;

    private void Awake()
    {
        characterAbility = GetComponent<CharacterAbility>();
        characterAbility.Init(HP, MP, PAP, MAP, PDP, MDP, WALK);
    }

    // Use this for initialization
    void Start()
    {
        if (photonView.isMine)
        {

        
        }
        else
        {
            audioListener = GetComponentInChildren<AudioListener>();
            otherCamera = GetComponentInChildren<Camera>();
            audioListener.enabled = false;
            otherCamera.enabled = false;
            //this.tag = "Player";
        }

        GetComponent<CharacterMovement>().SetMovement(WALK, RUN);
    }

    // Update is called once per frame
    void Update()
    {
        if (characterAbility.GetHP() <= 0)
        {
            Debug.Log("Grunt is dead");
        }
    }

}
