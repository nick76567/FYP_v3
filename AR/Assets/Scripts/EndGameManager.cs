using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour {

    private const int W_COIN = 200, L_COIN = 100;
    private PlayerData player;

    public Text winTeam, reward;
    public Button returnButoon;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetWinTeam(PunTeams.Team team)
    {
        winTeam.text = "Winning Team: " + team;
    }

    public void SetReward(bool isWin)
    {
        player = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        if (isWin)
        {
            reward.text = "Coins: " + W_COIN;
            player.data.coins += W_COIN;
        }
        else
        {
            reward.text = "Coins: " + L_COIN;
            player.data.coins += L_COIN;
        }
    }

    public void ReturnButton()
    {
        SceneManager.LoadScene("Room");
    }
}
