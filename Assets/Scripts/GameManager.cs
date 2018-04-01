using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private string[] charactersName = { "Golem", "Grunt", "Lich", "Soldier" };
    private enum Planet { Orange, Ice, Forest};
    private float startTime;
    private bool isStartEndGame;
    //private GameObject[] players;
    private PlayerData playerData;

    public PlanetAbility[] planets;

	// Use this for initialization
	void Start () {
        isStartEndGame = false;
        //players = GameObject.FindGameObjectsWithTag("Player");
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        GameObject character = PhotonNetwork.Instantiate(charactersName[(int)playerData.GetSelectedCharacter()], new Vector3(Random.Range(0, 50), 0, 0), Quaternion.identity, 0);
        character.GetComponent<CharacterAbility>().EquipWeapon(playerData.GetWeapon(playerData.GetSelectedWeapon()));
        character.GetComponent<CharacterAbility>().EquipArmor(playerData.GetArmor(playerData.GetSelectedArmor()));
    }
	
	// Update is called once per frame
	void Update () {
        if (PhotonNetwork.isMasterClient)
        {
            if (PhotonNetwork.room.PlayerCount != PhotonNetwork.room.MaxPlayers)
            {
                //do sth
            }

            if (planets[(int)Planet.Orange].GetTeam() != PunTeams.Team.none &&
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
                        PhotonNetwork.LoadLevel("Lobby");
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
