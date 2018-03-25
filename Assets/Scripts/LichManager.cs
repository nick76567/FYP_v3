using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichManager : Photon.MonoBehaviour {

    private const int HP = 800, MP = 1000, PAP = 0, MAP = 100, PDP = 20, MDP = 100;

    private CharacterAbility characterAbility;
    private Camera otherCamera;
    private AudioListener audioListener;

    // Use this for initialization
    void Start()
    {
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
            //this.tag = "Enemy";
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (characterAbility.GetHP() <= 0)
        {
            Debug.Log("Lich is dead");
        }
    }
}
