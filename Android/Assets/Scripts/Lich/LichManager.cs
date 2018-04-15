using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichManager : Photon.MonoBehaviour {

    private const int HP = 1000, MP = 1000, PAP = 0, MAP = 150, PDP = 10, MDP = 60;
    private const float WALK = 12, RUN = WALK + 8;

    private CharacterAbility characterAbility;
    private Camera otherCamera;
    private AudioListener audioListener;

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
        }

        GetComponent<CharacterMovement>().SetMovement(WALK, RUN);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
