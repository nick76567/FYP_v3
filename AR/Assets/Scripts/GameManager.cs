using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private string[] charactersName = { "Golem", "Grunt", "Lich", "Soldier" };
    private enum Planet { Orange, Ice, Forest };
    private float startTime;
    private bool isStartEndGame, isEndGame;
    private List<GameObject> playerList;
    private PlayerData playerData;

    public PlanetAbility[] planets;
    public GameObject endGameCanvas;
    // Use this for initialization
    void Start()
    {
        isEndGame = isStartEndGame = false;
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        GameObject character = PhotonNetwork.Instantiate(charactersName[(int)playerData.GetSelectedCharacter()], new Vector3(Random.Range(0, 50), 0, 0), Quaternion.identity, 0);
        character.GetComponent<CharacterAbility>().EquipWeapon(playerData.GetWeapon());
        character.GetComponent<CharacterAbility>().EquipArmor(playerData.GetArmor());
        endGameCanvas.SetActive(false);
        playerList = new List<GameObject>();

        DisablePlayers();
    }



    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.isMasterClient)
        {
            if (PhotonNetwork.room.PlayerCount != PhotonNetwork.room.MaxPlayers)
            {
                PhotonNetwork.LoadLevel("Room");
                //do sth
            }
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
                if (Time.time - startTime >= 10 && !isEndGame)
                {
                    //GameObject[] playerLists = GameObject.FindGameObjectsWithTag("Player");
                    //foreach(GameObject player in playerLists)
                    //{
                    //    player.GetComponent<CharacterAbility>().Destroy();
                    //}
                    isEndGame = true;
                    foreach (GameObject character in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        character.GetComponent<CharacterAbility>().CharacterEndGame();
                    }
                    endGameCanvas.SetActive(true);
                    endGameCanvas.GetComponent<EndGameManager>().SetWinTeam(planets[(int)Planet.Orange].GetTeam());
                    endGameCanvas.GetComponent<EndGameManager>().SetReward(PhotonNetwork.player.GetTeam() == planets[(int)Planet.Orange].GetTeam());

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

    private void DisablePlayers()
    {
        Debug.Log("DisablePlayers is called");
        GetPlayersRef(GameObject.FindGameObjectsWithTag("Player"));
        if (playerList.Count < PhotonNetwork.room.MaxPlayers)
        {
            Debug.Log("less Players num " + playerList.Count);
            foreach (GameObject player in playerList)
            {
                player.SetActive(false);

            }

            Invoke("DisablePlayers", 0.3f);

        }
        else
        {
            Debug.Log("equal Players num " + playerList.Count);
            EnablePlayers();
        }
    }

    private void EnablePlayers()
    {
        Debug.Log("EnablePlayers");


        foreach (GameObject player in playerList)
        {
            player.SetActive(true);
        }
    }

    private void GetPlayersRef(GameObject[] players)
    {
        foreach (GameObject player in players)
        {
            playerList.Add(player);
        }
    }
}
