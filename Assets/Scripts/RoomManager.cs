using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomManager : Photon.PunBehaviour {

    public Button[] WeaponsList, ArmorsList, CharactersList, TeamList;
    public GameObject[] allIdleCharactersList;
    public Button readyButton, returnButton;
    public enum CharactersName { Golem, Grunt, Lich, Soldier, CharatersLen };
    
    private CharactersName selectedCharacter;
    private WeaponAbility.Weapon selectedWeapon;
    private ArmorAbility.Armor selectedArmor;
    private PunTeams.Team selectedTeam;
    private int maxPlayerPerTeam;
    private PlayerData playerData;


    // Use this for initialization
    void Start () {
        if (PhotonNetwork.isMasterClient)
        {
            //isBeingMasterClient = true;
            readyButton.GetComponentInChildren<Text>().text = "Start";
        }

        PhotonNetwork.player.SetTeam(PunTeams.Team.none);
        selectedTeam = PunTeams.Team.none;

        for (int i = 1; i < (int)CharactersName.CharatersLen; i++)
            allIdleCharactersList[i].SetActive(false);

        foreach(Button button in WeaponsList)
        {
            button.GetComponent<Image>().color = Color.gray;
        }

        foreach(Button button in ArmorsList)
        {
            button.GetComponent<Image>().color = Color.gray;
        }

        readyButton.GetComponent<Image>().color = Color.gray;
        readyButton.enabled = false;

        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        maxPlayerPerTeam = PhotonNetwork.room.MaxPlayers / 2;

        SelectGolem();
        EquipAxe();
        EquipArmour();
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    private void DisableButtons()
    {
        foreach (Button button in WeaponsList)
        {
            button.enabled = false;
        }

        foreach (Button button in ArmorsList)
        {
            button.enabled = false;
        }

        foreach (Button button in CharactersList)
        {
            button.enabled = false;
        }

        foreach (Button button in TeamList)
        {
            button.enabled = false;
        }

        returnButton.enabled = false;
    }

    private void EnableButtons()
    {
        foreach (Button button in WeaponsList)
        {
            button.enabled = true;
        }

        foreach (Button button in ArmorsList)
        {
            button.enabled = true;
        }

        foreach (Button button in CharactersList)
        {
            button.enabled = true;
        }

        foreach (Button button in TeamList)
        {
            button.enabled = true;
        }

        returnButton.enabled = true;
    }

    private void SelectCharacter(CharactersName name)
    {
        allIdleCharactersList[(int)selectedCharacter].SetActive(false);
        selectedCharacter = name;
        allIdleCharactersList[(int)selectedCharacter].SetActive(true);
    }

    private void SelectWeapon(WeaponAbility.Weapon weapon)
    {
        WeaponsList[(int)selectedWeapon].GetComponent<Image>().color = Color.gray;
        selectedWeapon = weapon;
        WeaponsList[(int)weapon].GetComponent<Image>().color = Color.white;
    }

    private void SelectArmor(ArmorAbility.Armor armor)
    {
        ArmorsList[(int)selectedArmor].GetComponent<Image>().color = Color.gray;
        selectedArmor = armor;
        ArmorsList[(int)armor].GetComponent<Image>().color = Color.white;
    }

    public override void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
    {
        if (PhotonNetwork.isMasterClient)
        {
            
            if(PunTeams.PlayersPerTeam[PunTeams.Team.blue].Count > 0 && PunTeams.PlayersPerTeam[PunTeams.Team.red].Count > 0 &&
                PunTeams.PlayersPerTeam[PunTeams.Team.blue].Count <= maxPlayerPerTeam && PunTeams.PlayersPerTeam[PunTeams.Team.red].Count <= maxPlayerPerTeam &&
               PunTeams.PlayersPerTeam[PunTeams.Team.blue].Count + PunTeams.PlayersPerTeam[PunTeams.Team.red].Count == maxPlayerPerTeam * 2)
            {
                Debug.Log("RoomManager MaxPlayerPerTeam " + maxPlayerPerTeam);

                DisableButtons();
                readyButton.enabled = true;
                readyButton.GetComponent<Image>().color = Color.green;
            }
            else
            {
                EnableButtons();
                readyButton.enabled = false;
                readyButton.GetComponent<Image>().color = Color.gray;
            }
        }

        Debug.Log("RoomManager Red Team " + PunTeams.PlayersPerTeam[PunTeams.Team.red].Count);
        Debug.Log("RoomManager Blue Team " + PunTeams.PlayersPerTeam[PunTeams.Team.blue].Count);
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void ReturnButton()
    {
        PhotonNetwork.LeaveRoom();
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
        Debug.Log("RoomManager changeRedTeam is called");

        if(selectedTeam == PunTeams.Team.none && !PhotonNetwork.isMasterClient)
        {
            readyButton.enabled = true;
            readyButton.GetComponent<Image>().color = Color.white;
        }
        selectedTeam = PunTeams.Team.red;

        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.player.SetTeam(PunTeams.Team.red);
        }

    }

    public void ChangeBlueTeam()
    {
        Debug.Log("RoomManager changeBlueTeam is called");

        if (selectedTeam == PunTeams.Team.none && !PhotonNetwork.isMasterClient)
        {
            readyButton.enabled = true;
            readyButton.GetComponent<Image>().color = Color.white;
        }
        selectedTeam = PunTeams.Team.blue;

        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.player.SetTeam(PunTeams.Team.blue);
        }
    }

    public void EquipAxe()
    {
        SelectWeapon(WeaponAbility.Weapon.AXE);
        playerData.SetSelectedWeapon(WeaponAbility.Weapon.AXE);
    }

    public void EquipBow()
    {
        SelectWeapon(WeaponAbility.Weapon.BOW);
        playerData.SetSelectedWeapon(WeaponAbility.Weapon.BOW);
    }

    public void EquipStaff()
    {
        SelectWeapon(WeaponAbility.Weapon.STAFF);
        playerData.SetSelectedWeapon(WeaponAbility.Weapon.STAFF);
    }

    public void EquipSword()
    {
        SelectWeapon(WeaponAbility.Weapon.SWORD);
        playerData.SetSelectedWeapon(WeaponAbility.Weapon.SWORD);
    }

    public void EquipArmour()
    {
        SelectArmor(ArmorAbility.Armor.ARMOUR);
        playerData.SetSelectedArmor(ArmorAbility.Armor.ARMOUR);
    }

    public void EquipBoot()
    {
        SelectArmor(ArmorAbility.Armor.BOOT);
        playerData.SetSelectedArmor(ArmorAbility.Armor.BOOT);
    }

    public void EquipCloak()
    {
        SelectArmor(ArmorAbility.Armor.CLOAK);
        playerData.SetSelectedArmor(ArmorAbility.Armor.CLOAK);
    }

    public void ReadyButton()
    {
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.LoadLevel("Game");
        }
        else
        {
            //do sth
            if(PunTeams.PlayersPerTeam[selectedTeam].Count < maxPlayerPerTeam)
            {
                PhotonNetwork.player.SetTeam(selectedTeam);
                DisableButtons();
            }
            else
            {
                //Warning
            }
                
        }
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        if (PhotonNetwork.isMasterClient)
        {
            readyButton.GetComponentInChildren<Text>().text = "Start";
            readyButton.enabled = false;
            readyButton.GetComponent<Image>().color = Color.gray;
        }
    }
}
