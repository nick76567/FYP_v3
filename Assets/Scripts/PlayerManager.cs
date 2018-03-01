using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : Photon.PunBehaviour, IPunObservable {

    public GameObject[] allButtonsList;
    public GameObject[] allIdleCharactersList;

    private string[] charactersName = { "Golem", "Grunt", "Lich", "Soldier" };
    private enum CharactersName { Golem, Grunt, Lich, Soldier, CharatersLen};
    private CharactersName currentCharacter;
    // Use this for initialization
    void Start () {
        if (photonView.isMine)
        {
            PhotonNetwork.player.SetTeam(PunTeams.Team.none);
            currentCharacter = CharactersName.Golem;

            for (int i = 1; i < (int)CharactersName.CharatersLen; i++)
                allIdleCharactersList[i].SetActive(false);
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

        DontDestroyOnLoad(this);

        Debug.Log("Player is created");
	}
	
	// Update is called once per frame
	void Update () {
		
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

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {

        }
        else
        {

        }
    }

    [PunRPC]
    public void Ready()
    {

    }
}
