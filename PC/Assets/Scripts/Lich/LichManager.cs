using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichManager : Photon.MonoBehaviour {

    private const int HP = 1200, MP = 1000, PAP = 0, MAP = 140, PDP = 20, MDP = 100;
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
            //this.tag = "Enemy";
        }

        GetComponent<CharacterMovement>().SetMovement(WALK, RUN);
    }

    // Update is called once per frame
    void Update()
    {
        //if (characterAbility.GetHP() <= 0)
        //{
        //    Debug.Log("Lich is dead");
        //}
    }
}
