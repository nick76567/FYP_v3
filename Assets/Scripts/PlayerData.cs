using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class Weapon
{
    public WeaponAbility.Weapon type;
    public double apRate;
    public double speedRate;

    public Weapon(WeaponAbility.Weapon _type, double _apRate, double _speedRate)
    {
        type = _type;
        apRate = _apRate;
        speedRate = _speedRate;
    }

   
}

[Serializable]
public class Armor
{
    public ArmorAbility.Armor type;
    public double pdpRate;
    public double mdpRate;
    public double speed;

    public Armor(ArmorAbility.Armor _type, double _pdpRate, double _mdpRate, double _speed)
    {
        type = _type;
        pdpRate = _pdpRate;
        mdpRate = _mdpRate;
        speed = _speed;
    }
    
}

[Serializable]
public class Data
{
    public List<Weapon> weaponList;
    public List<Armor> armorList;
    public int win;
    public int lose;
    public int money;

    public Data()
    {
        weaponList = new List<Weapon>();
        weaponList.Add(new Weapon(WeaponAbility.Weapon.AXE, 0, 0));
        weaponList.Add(new Weapon(WeaponAbility.Weapon.BOW, 0, 0));
        weaponList.Add(new Weapon(WeaponAbility.Weapon.STAFF, 0, 0));
        weaponList.Add(new Weapon(WeaponAbility.Weapon.SWORD, 0, 0));

        armorList = new List<Armor>();
        armorList.Add(new Armor(ArmorAbility.Armor.ARMOUR, 0, 0, 0));
        armorList.Add(new Armor(ArmorAbility.Armor.BOOT, 0, 0, 0));
        armorList.Add(new Armor(ArmorAbility.Armor.CLOAK, 0, 0, 0));

        money = lose = win = 0;
    }
}


public class PlayerData : MonoBehaviour {

    private string FilePath;

    public Data data;

    // Use this for initialization
    void Start () {
        data = new Data();    
        FilePath = Path.Combine(Application.dataPath, "save.txt");
        if (File.Exists(FilePath))
        {
            Load();
        }
        Save();
        DontDestroyOnLoad(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BuildWeapon(WeaponAbility.Weapon type, double apRate, double speedRate)
    {
        int index = (int)type;
        Debug.Log("Weapon type " + index);
        data.weaponList[index].apRate = apRate;
        data.weaponList[index].speedRate = speedRate;
    }

    public void BuildArmor(ArmorAbility.Armor type, double pdpRate, double mdpRate, double speed)
    {
        int index = (int)type;
        data.armorList[index].pdpRate = pdpRate;
        data.armorList[index].mdpRate = mdpRate;
        data.armorList[index].speed = speed;
    }

    public Weapon GetWeapon(WeaponAbility.Weapon type)
    {
        return data.weaponList[(int)type];
    }

    public Armor GetArmor(ArmorAbility.Armor type)
    {
        return data.armorList[(int)type];
    }

    public void Save()
    {
        string jsonString = JsonUtility.ToJson(data);
        File.WriteAllText(FilePath, jsonString);
    }

    public void Load()
    {
        string jsonString = File.ReadAllText(FilePath);
        JsonUtility.FromJsonOverwrite(jsonString, data);
    }
}
