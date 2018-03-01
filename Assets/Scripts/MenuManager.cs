using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : Photon.PunBehaviour {

    public Button play;

    private const string _GAME_VERSION = "1";
    private const string LOBBY_SCENE = "Lobby";

    private void Awake()
    {
        PhotonNetwork.autoJoinLobby = false;
        PhotonNetwork.automaticallySyncScene = true;

        play.enabled = false;
    }

    // Use this for initialization
    void Start () {
        if (PhotonNetwork.ConnectUsingSettings(_GAME_VERSION))
        {
            play.enabled = true;
        }
	}
	
    public void Play()
    {
        SceneManager.LoadScene(LOBBY_SCENE);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connet to master");
    }
}
