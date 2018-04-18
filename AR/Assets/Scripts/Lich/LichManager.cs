using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichManager : Photon.MonoBehaviour {

    private const int HP = 1000, MP = 1000, PAP = 0, MAP = 150, PDP = 10, MDP = 60;
    private const float WALK = 120, RUN = WALK + 30;

    private CharacterAbility characterAbility;
    private Camera otherCamera;
    private AudioListener audioListener;

    private void Awake()
    {
        characterAbility = GetComponent<CharacterAbility>();
        characterAbility.Init(HP, MP, PAP, MAP, PDP, MDP, WALK);
        //this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
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

    //private void OnEnable()
    //{
    //    this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    //}

    
}
