using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : Photon.PunBehaviour {

    public InputField cName;
    public InputField jName;
    public GameObject createPanel;
    public GameObject joinPanel;
    public Dropdown maxPlayers;
    public Text createFail;
    public Text joinFail;

    private byte[] maxPlayersList = { 2, 4 };
    private string currentPanel = "";
    private const string ROOM_SCENE = "Room";

	// Use this for initialization
	void Start () {
        DisableUI("All");
        SceneManager.sceneLoaded += this.OnLoadCallBack;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void DisableUI(string ui)
    {
        switch (ui)
        {
            case "CreatePanel":
                createPanel.SetActive(false);
                break;
            case "JoinPanel":
                joinPanel.SetActive(false);
                break;
            case "All":
                createPanel.SetActive(false);
                joinPanel.SetActive(false);
                createFail.enabled = false;
                joinFail.enabled = false;
                break;
            case "":
                break;
        }

        currentPanel = "";
    }

    public void EnableCreatePanel()
    {
        DisableUI(currentPanel);
        currentPanel = "CreatePanel";
        createPanel.SetActive(true);
    }

    public void EnableJoinPanel()
    {
        DisableUI(currentPanel);
        currentPanel = "JoinPanel";
        joinPanel.SetActive(true);
    }

    public void DisablePanel()
    {
        DisableUI(currentPanel);
        currentPanel = "";
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(cName.text,
                                 new RoomOptions() { MaxPlayers = maxPlayersList[maxPlayers.value] },
                                 null);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(jName.text);
    }

    private void OnLoadCallBack(Scene scene, LoadSceneMode sceneMode)
    {
        Debug.Log("LEVEL " + scene.name);

        if(scene.name == "Room")
        {
            PhotonNetwork.Instantiate("Player", new Vector3(), Quaternion.identity, 0);
        }
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Craete Room");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Join Room");
        SceneManager.LoadScene(ROOM_SCENE);
        
    }

    public override void OnPhotonCreateRoomFailed(object[] codeAndMsg)
    {
        Debug.Log("Create Room Fail");
        createFail.enabled = true;
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
        Debug.Log("Join Room Fail");
        joinFail.enabled = true;
    }
}
