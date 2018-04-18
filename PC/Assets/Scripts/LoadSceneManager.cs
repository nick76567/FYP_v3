using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : Photon.PunBehaviour {

	public Button play;

	private const string _GAME_VERSION = "1";
	private const string LOBBY_SCENE = "New Lobby";

	public void Awake()
	{
		PhotonNetwork.autoJoinLobby = false;
		PhotonNetwork.automaticallySyncScene = true;

		play.enabled = false;


	}


	// Use this for initialization
	public void Start () {

		if (PhotonNetwork.ConnectUsingSettings (_GAME_VERSION)) {
			play.enabled = true;

		}
	}

	public void Play()
	{
		SceneManager.LoadScene (LOBBY_SCENE);
	}

	public override void OnConnectedToMaster()
	{
		Debug.Log ("connect to master");

	}
}
