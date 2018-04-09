using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopManager : Photon.PunBehaviour {
    private const int WEAPON_PRICE = 3000, ARMOR_PRICE = 2000;
    private PlayerData player;
    private Data data;
    public Text[] weaponList;
    public Text[] armorList;
    public Text coins;



	// Use this for initialization
	void Start () {
        Debug.Log("ShopManager is created");
        player = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        Debug.Log("After find playerData");
        data = player.data;


        foreach(WeaponAbility.Weapon weapon in Enum.GetValues(typeof(WeaponAbility.Weapon)))
        {
            UpdateWeaponText(weapon);
        }

        foreach(ArmorAbility.Armor armor in Enum.GetValues(typeof(ArmorAbility.Armor)))
        {
            UpdateArmorText(armor);
        }

        UpdateCoinsText();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void UpdateWeaponText(WeaponAbility.Weapon weapon)
    {
        switch (weapon)
        {
            case WeaponAbility.Weapon.AXE:
                weaponList[(int)WeaponAbility.Weapon.AXE].text = "PAP increase: " + data.weaponList[(int)WeaponAbility.Weapon.AXE].apRate + "\n" +
                                                            "PAS increase: " + data.weaponList[(int)WeaponAbility.Weapon.AXE].speedRate + "\n" +
                                                            "Price: " + WEAPON_PRICE;
                break;
            case WeaponAbility.Weapon.BOW:
                weaponList[(int)WeaponAbility.Weapon.BOW].text = "MAP increase: " + data.weaponList[(int)WeaponAbility.Weapon.BOW].apRate + "\n" +
                                                            "MAS increase: " + data.weaponList[(int)WeaponAbility.Weapon.BOW].speedRate + "\n" +
                                                            "Price: " + WEAPON_PRICE;
                break;
            case WeaponAbility.Weapon.STAFF:
                weaponList[(int)WeaponAbility.Weapon.STAFF].text = "MAP increase: " + data.weaponList[(int)WeaponAbility.Weapon.STAFF].apRate + "\n" +
                                                            "MAS increase: " + data.weaponList[(int)WeaponAbility.Weapon.STAFF].speedRate + "\n" +
                                                            "Price: " + WEAPON_PRICE;
                break;
            case WeaponAbility.Weapon.SWORD:
                weaponList[(int)WeaponAbility.Weapon.SWORD].text = "PAP increase: " + data.weaponList[(int)WeaponAbility.Weapon.SWORD].apRate + "\n" +
                                                            "PAS increase: " + data.weaponList[(int)WeaponAbility.Weapon.SWORD].speedRate + "\n" +
                                                            "Price: " + WEAPON_PRICE;
                break;
        }
    }

    private void UpdateArmorText(ArmorAbility.Armor armor)
    {
        switch (armor)
        {
            case ArmorAbility.Armor.ARMOUR:
                armorList[(int)ArmorAbility.Armor.ARMOUR].text = "PDP increase: " + data.armorList[(int)ArmorAbility.Armor.ARMOUR].pdpRate + "\n" +
                                                                 "MDP increase: " + data.armorList[(int)ArmorAbility.Armor.ARMOUR].mdpRate + "\n" +
                                                                 "Speed: " + data.armorList[(int)ArmorAbility.Armor.ARMOUR].speed + "\n" +
                                                                 "Price: " + ARMOR_PRICE;
                break;
            case ArmorAbility.Armor.BOOT:
                armorList[(int)ArmorAbility.Armor.BOOT].text = "PDP increase: " + data.armorList[(int)ArmorAbility.Armor.BOOT].pdpRate + "\n" +
                                                                 "MDP increase: " + data.armorList[(int)ArmorAbility.Armor.BOOT].mdpRate + "\n" +
                                                                 "Speed: " + data.armorList[(int)ArmorAbility.Armor.BOOT].speed + "\n" +
                                                                 "Price: " + ARMOR_PRICE;
                break;
            case ArmorAbility.Armor.CLOAK:
                armorList[(int)ArmorAbility.Armor.CLOAK].text = "PDP increase: " + data.armorList[(int)ArmorAbility.Armor.CLOAK].pdpRate + "\n" +
                                                                 "MDP increase: " + data.armorList[(int)ArmorAbility.Armor.CLOAK].mdpRate + "\n" +
                                                                 "Speed: " + data.armorList[(int)ArmorAbility.Armor.CLOAK].speed + "\n" +
                                                                 "Price: " + ARMOR_PRICE;
                break;
        }
    }

    private void UpdateCoinsText()
    {
        coins.text = "Coins: " + data.coins;
    }

    public void BuyAxe()
    {
        if (data.coins < WEAPON_PRICE)
            return;
        data.coins -= WEAPON_PRICE;
        UpdateCoinsText();
        Axe axe = new Axe();
        player.BuildWeapon(WeaponAbility.Weapon.AXE, axe.GetPapIncreaseRate(), axe.GetpSpeedIncreaseRate());
        UpdateWeaponText(WeaponAbility.Weapon.AXE);
        player.Save();
    }

    public void BuyBow()
    {
        if (data.coins < WEAPON_PRICE)
            return;
        data.coins -= WEAPON_PRICE;
        UpdateCoinsText();
        Bow bow = new Bow();
        player.BuildWeapon(WeaponAbility.Weapon.BOW, bow.GetMapIncreaseRate(), bow.GetmSpeedIncreaseRate());
        UpdateWeaponText(WeaponAbility.Weapon.BOW);
        player.Save();
    }

    public void BuyStaff()
    {
        if (data.coins < WEAPON_PRICE)
            return;
        data.coins -= WEAPON_PRICE;
        UpdateCoinsText();
        Staff staff = new Staff();
        player.BuildWeapon(WeaponAbility.Weapon.STAFF, staff.GetMapIncreaseRate(), staff.GetmSpeedIncreaseRate());
        UpdateWeaponText(WeaponAbility.Weapon.STAFF);
        player.Save();
    }

    public void BuySword()
    {
        if (data.coins < WEAPON_PRICE)
            return;
        data.coins -= WEAPON_PRICE;
        UpdateCoinsText();
        Sword sword = new Sword();
        player.BuildWeapon(WeaponAbility.Weapon.SWORD, sword.GetPapIncreaseRate(), sword.GetpSpeedIncreaseRate());
        UpdateWeaponText(WeaponAbility.Weapon.SWORD);
        player.Save();
    }

    public void BuyArmour()
    {
        if (data.coins < ARMOR_PRICE)
            return;
        data.coins -= ARMOR_PRICE;
        UpdateCoinsText();
        Armour armour = new Armour();
        player.BuildArmor(ArmorAbility.Armor.ARMOUR, armour.GetpdpIncreaseRate(), armour.GetmdpIncreaseRate(), 0);
        UpdateArmorText(ArmorAbility.Armor.ARMOUR);
        player.Save();
    }

    public void BuyBoot()
    {
        if (data.coins < ARMOR_PRICE)
            return;
        data.coins -= ARMOR_PRICE;
        UpdateCoinsText();
        Boot boot = new Boot();
        player.BuildArmor(ArmorAbility.Armor.BOOT, 0, 0, boot.GetSpeedIncreaseRate());
        UpdateArmorText(ArmorAbility.Armor.BOOT);
        player.Save();
    }

    public void BuyCloak()
    {
        if (data.coins < ARMOR_PRICE)
            return;
        data.coins -= ARMOR_PRICE;
        UpdateCoinsText();
        Cloak cloak = new Cloak();
        player.BuildArmor(ArmorAbility.Armor.CLOAK, cloak.GetpdpIncreaseRate(), cloak.GetmdpIncreaseRate(), 0);
        UpdateArmorText(ArmorAbility.Armor.CLOAK);
        player.Save();
    }

    public void Return()
    {
        SceneManager.LoadScene("New Lobby", LoadSceneMode.Single);
    }
}
