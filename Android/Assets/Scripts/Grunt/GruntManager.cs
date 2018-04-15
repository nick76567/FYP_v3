using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntManager : Photon.PunBehaviour {

    private const int HP = 1200, MP = 0, PAP = 150, MAP = 0, PDP = 20, MDP = 40;
    private const float WALK = 14, RUN = WALK + 8;

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
            //this.tag = "Enemy";
        }

        GetComponent<CharacterMovement>().SetMovement(WALK, RUN);
    }

    // Update is called once per frame
    void Update()
    {
        //if (characterAbility.GetHP() <= 0)
        //{
        //    Debug.Log("Grunt is dead");
        //}
    }


}
