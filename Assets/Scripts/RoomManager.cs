using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomManager : Photon.PunBehaviour {

    public GameObject[] allIdleCharactersList;
    public Button readyButton;
    public enum CharactersName { Golem, Grunt, Lich, Soldier, CharatersLen };

    
    private CharactersName currentCharacter;
    private bool isReady;
    private int maxPlayerPerTeam;
    private PlayerData playerData;


    // Use this for initialization
    void Start () {
        if (PhotonNetwork.isMasterClient)
        {
            //isBeingMasterClient = true;
            readyButton.enabled = false;
            readyButton.GetComponentInChildren<Text>().text = "Start";
        }

        PhotonNetwork.player.SetTeam(PunTeams.Team.none);

        for (int i = 1; i < (int)CharactersName.CharatersLen; i++)
            allIdleCharactersList[i].SetActive(false);

        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        maxPlayerPerTeam = PhotonNetwork.room.MaxPlayers / 2;
        isReady = false;
    }
	
	// Update is called once per frame
	void Update () {
       // Debug.Log("Blue team " + PunTeams.PlayersPerTeam[PunTeams.Team.blue].Count);
    }

    private void SelectCharacter(CharactersName name)
    {
        allIdleCharactersList[(int)currentCharacter].SetActive(false);
        currentCharacter = name;
        allIdleCharactersList[(int)currentCharacter].SetActive(true);
    }

    public override void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
    {
        if (PhotonNetwork.isMasterClient)
        {
            if(PunTeams.PlayersPerTeam[PunTeams.Team.blue].Count == maxPlayerPerTeam &&
               PunTeams.PlayersPerTeam[PunTeams.Team.red].Count == maxPlayerPerTeam)
            {
                readyButton.enabled = true;
                readyButton.GetComponent<Image>().color = Color.green;
            }
            else
            {
                readyButton.enabled = false;
                readyButton.GetComponent<Image>().color = Color.gray;
            }
        }
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void ReturnButton()
    {
        PhotonNetwork.LeaveRoom();
        //PhotonNetwork.Disconnect();
    }

    public void SelectGolem()
    {
        SelectCharacter(CharactersName.Golem);
        playerData.SetSelectedCharacter(CharactersName.Golem);
    }

    public void SelectGrunt()
    {
        SelectCharacter(CharactersName.Grunt);
        playerData.SetSelectedCharacter(CharactersName.Grunt);
    }

    public void SelectLich()
    {
        SelectCharacter(CharactersName.Lich);
        playerData.SetSelectedCharacter(CharactersName.Lich);
    }

    public void SelectSoldier()
    {
        SelectCharacter(CharactersName.Soldier);
        playerData.SetSelectedCharacter(CharactersName.Soldier);
    }

    public void ChangeRedTeam()
    {
        PhotonNetwork.player.SetTeam(PunTeams.Team.red);

    }

    public void ChangeBlueTeam()
    {
        PhotonNetwork.player.SetTeam(PunTeams.Team.blue);
    }

    public void EquipAxe()
    {
        playerData.SetSelectedWeapon(WeaponAbility.Weapon.AXE);
    }

    public void EquipBow()
    {
        playerData.SetSelectedWeapon(WeaponAbility.Weapon.BOW);
    }

    public void EquipStaff()
    {
        playerData.SetSelectedWeapon(WeaponAbility.Weapon.STAFF);
    }

    public void EquipSword()
    {
        playerData.SetSelectedWeapon(WeaponAbility.Weapon.SWORD);
    }

    public void EquipArmour()
    {
        playerData.SetSelectedArmor(ArmorAbility.Armor.ARMOUR);
    }

    public void EquipBoot()
    {
        playerData.SetSelectedArmor(ArmorAbility.Armor.BOOT);
    }

    public void EquipCloak()
    {
        playerData.SetSelectedArmor(ArmorAbility.Armor.CLOAK);
    }
}
