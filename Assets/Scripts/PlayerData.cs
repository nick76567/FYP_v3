using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class Weapon
{
    public Weapon(WeaponAbility.Weapon _type, double _apRate, double _speedRate)
    {
        type = _type;
        apRate = _apRate;
        speedRate = _speedRate;
    }

    public WeaponAbility.Weapon type;
    public double apRate;
    public double speedRate;
}

[Serializable]
public class Data
{
    public List<Weapon> weaponList;
    //public Weapon[] weaponList;
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
