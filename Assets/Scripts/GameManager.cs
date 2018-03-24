using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private enum Planet { Orange, Ice, Forest};
    private float startTime;
    private bool isStartEndGame;
    //private GameObject[] players;

    public PlanetAbility[] planets;

	// Use this for initialization
	void Start () {
        isStartEndGame = false;
        //players = GameObject.FindGameObjectsWithTag("Player");
        
	}
	
	// Update is called once per frame
	void Update () {
        if (PhotonNetwork.isMasterClient)
        {
            if (PhotonNetwork.room.PlayerCount != PhotonNetwork.room.MaxPlayers)
            {
                //do sth
            }

            if (planets[(int)Planet.Orange].GetTeam() != CharacterAbility.Team.none &&
               planets[(int)Planet.Orange].GetTeam() == planets[(int)Planet.Forest].GetTeam() &&
               planets[(int)Planet.Orange].GetTeam() == planets[(int)Planet.Ice].GetTeam())
            {
                if (!isStartEndGame)
                {
                    isStartEndGame = true;
                    startTime = Time.time;
                }
                else
                {
                    if (Time.time - startTime >= 10)
                    {
                        PhotonNetwork.LoadLevel("Room");
                    }
                }

            }
            else
            {
                if (isStartEndGame)
                {
                    isStartEndGame = false;
                }
            }
        }
	}
}
