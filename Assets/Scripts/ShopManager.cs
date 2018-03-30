using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour {

    private PlayerData player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("PlayerData").GetComponent<PlayerData>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BuyAxe()
    {
        Axe axe = new Axe();
        player.BuildWeapon(WeaponAbility.Weapon.AXE, axe.GetPapIncreaseRate(), axe.GetpSpeedIncreaseRate());
        player.Save();
    }

    public void BuyBow()
    {
        Bow bow = new Bow();
        player.BuildWeapon(WeaponAbility.Weapon.BOW, bow.GetMapIncreaseRate(), bow.GetmSpeedIncreaseRate());
        player.Save();
    }

    public void BuyStaff()
    {
        Staff staff = new Staff();
        player.BuildWeapon(WeaponAbility.Weapon.STAFF, staff.GetMapIncreaseRate(), staff.GetmSpeedIncreaseRate());
        player.Save();
    }

    public void BuySword()
    {
        Sword sword = new Sword();
        player.BuildWeapon(WeaponAbility.Weapon.SWORD, sword.GetPapIncreaseRate(), sword.GetpSpeedIncreaseRate());
        player.Save();
    }

    public void Return()
    {
        SceneManager.LoadScene("Lobby");
    }
}
