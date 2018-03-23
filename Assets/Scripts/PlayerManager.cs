using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : Photon.PunBehaviour, IPunObservable{

    private const int READY_BUTTON = 6;
    private const string SCENE = "Game";

    public GameObject[] allButtonsList;
    public GameObject[] allIdleCharactersList;
    public Text readyButton;

    private string[] charactersName = { "Golem", "Grunt", "Lich", "Soldier" };
    private enum CharactersName { Golem, Grunt, Lich, Soldier, CharatersLen};
    private CharactersName currentCharacter;
    private bool isReady , isBeingMasterClient;
    private static int totalIsReady, maxPlayerPerTeam;


    // Use this for initialization
    void Start () {
        if (photonView.isMine)
        {
            PhotonNetwork.player.SetTeam(PunTeams.Team.none);
            currentCharacter = CharactersName.Golem;

            for (int i = 1; i < (int)CharactersName.CharatersLen; i++)
                allIdleCharactersList[i].SetActive(false);

            isBeingMasterClient = false;
            isReady = false;
            totalIsReady = 0;
            
            if (PhotonNetwork.isMasterClient)
            {
                isBeingMasterClient = true;
                allButtonsList[READY_BUTTON].GetComponent<Button>().enabled = false;
                readyButton.text = "Start";
            }

            SceneManager.sceneLoaded += this.OnLoadCallBack;

        }
        else
        {
            foreach(GameObject button in allButtonsList)
            {
                button.SetActive(false);
            }

            foreach(GameObject idleCharacter in allIdleCharactersList)
            {
                idleCharacter.SetActive(false);
            }

        }
        maxPlayerPerTeam = PhotonNetwork.room.MaxPlayers / 2;

        //DontDestroyOnLoad(this);


        Debug.Log("photon maxplayer " + PhotonNetwork.room.MaxPlayers);
        Debug.Log("MaxPlayerPerTeam " + maxPlayerPerTeam);
	}
	
	// Update is called once per frame
	void Update () {
        if (PhotonNetwork.isMasterClient && photonView.isMine)
        {
            if (!isBeingMasterClient)
            {
                if (isReady)
                {
                    this.photonView.RPC("RPCReady", PhotonTargets.All, null);
                }
                allButtonsList[READY_BUTTON].GetComponent<Button>().enabled = false;
                readyButton.text = "Start";
                isBeingMasterClient = true;
            }
/*
            Debug.Log("Red Team " + PunTeams.PlayersPerTeam[PunTeams.Team.red].Count);
            Debug.Log("Blue Team " + PunTeams.PlayersPerTeam[PunTeams.Team.blue].Count);
            Debug.Log("totalIsReady " + totalIsReady);
*/
            if (PunTeams.PlayersPerTeam[PunTeams.Team.blue].Count == maxPlayerPerTeam &&
                PunTeams.PlayersPerTeam[PunTeams.Team.red].Count == maxPlayerPerTeam &&
                totalIsReady == PhotonNetwork.room.MaxPlayers - 1)
            {
                allButtonsList[READY_BUTTON].GetComponent<Button>().enabled = true;
            }
            
        }


    }

    private void OnLoadCallBack(Scene scene, LoadSceneMode sceneMode)
    {
        if(scene.name == "Game")
        {
            PhotonNetwork.Instantiate(charactersName[(int)currentCharacter], new Vector3(Random.Range(0, 50), 0, 0), Quaternion.identity, 0);
            
        }
    }
    
    private void SelectCharacter(CharactersName name)
    {
        allIdleCharactersList[(int)currentCharacter].SetActive(false);
        currentCharacter = name;
        allIdleCharactersList[(int)currentCharacter].SetActive(true);
    }

    public void SelectGolem()
    {
        SelectCharacter(CharactersName.Golem);
    }

    public void SelectGrunt()
    {
        SelectCharacter(CharactersName.Grunt);
    }

    public void SelectLich()
    {
        SelectCharacter(CharactersName.Lich);
    }

    public void SelectSoldier()
    {
        SelectCharacter(CharactersName.Soldier);
    }

    public void ChangeRedTeam()
    {
        PhotonNetwork.player.SetTeam(PunTeams.Team.red);
        
    }

    public void ChangeBlueTeam()
    {
        PhotonNetwork.player.SetTeam(PunTeams.Team.blue);
        
    }

    public void Ready()
    {
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.LoadLevel(SCENE);
        }
        else
        {
            this.photonView.RPC("RPCReady", PhotonTargets.All, null);
        }
        
    }

    [PunRPC]
    public void RPCReady()
    {
            if (isReady)
            {
                isReady = false;
                totalIsReady--;
                Debug.Log("RPCREady in isReady " + totalIsReady);
            }
            else
            {

                isReady = true;
                totalIsReady++;
                Debug.Log("RPCREady in !isReady " + totalIsReady);
            }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
