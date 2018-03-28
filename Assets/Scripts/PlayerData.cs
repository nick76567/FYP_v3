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
    public int win;
    public int lose;
    public int money;

    public Data()
    {
        weaponList = new List<Weapon>();
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
        Load();
        DontDestroyOnLoad(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddWeapon(WeaponAbility.Weapon type, double apRate, double speedRate)
    {
        data.weaponList.Add(new Weapon(type, apRate, speedRate));
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
