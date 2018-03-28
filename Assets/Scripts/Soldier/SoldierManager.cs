using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierManager : Photon.PunBehaviour {

    private const int HP = 1500, MP = 500, PAP = 70, MAP = 0, PDP = 50, MDP = 30;
    private const float WALK = 15, RUN = 24;

    private CharacterAbility characterAbility;
    private AudioListener audioListener;
    private Camera otherCamera;

    // Use this for initialization
    void Start () {
        characterAbility = GetComponent<CharacterAbility>();
        characterAbility.Init(HP, MP, PAP, MAP, PDP, MDP);

        if (photonView.isMine)
        {
        }
        else
        {
            audioListener = GetComponentInChildren<AudioListener>();
            otherCamera = GetComponentInChildren<Camera>();
            audioListener.enabled = false;
            otherCamera.enabled = false;
        }

        GetComponent<CharacterMovement>().SetMovement(WALK, RUN);
    }
	
	// Update is called once per frame
	void Update () {
       // Debug.Log(characterAbility.GetHP());

		if (characterAbility.GetHP() <= 0)
        {
            Debug.Log("Soldire is dead");
        }
	}

}
