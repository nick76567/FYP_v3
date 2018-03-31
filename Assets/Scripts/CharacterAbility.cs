﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAbility : Photon.PunBehaviour
{
    public Image healthBar;
    public enum Team { none, blue, red };
    public GameObject[] buttonList;

    private float startHP;
    private int hp;
    private int mp;
    private int physicalAp;
    private int magicalAp;
    private int physicalDp;
    private int magicalDp;
    private double pSpeed;
    private double mSpeed;
    private double speed;

    private Weapon equipWeapon;
    private Armor equipArmor;
    private PunTeams.Team team;
    private CharacterMovement characterMovement;



    // Use this for initialization
    void Start()
    {
        if (photonView.isMine)
        {
            this.photonView.RPC("SetTeam", PhotonTargets.All, PhotonNetwork.player.GetTeam());
        }
        else
        {
            foreach(GameObject button in buttonList)
            {
                button.SetActive(false);
            }
        }
        characterMovement = GetComponent<CharacterMovement>();
        healthBar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(int _hp, int _mp, int _physicalAp, int _magicalAp, int _physicalDp, int _magicalDp, double _speed)
    {
        startHP = hp = _hp;
        mp = _mp;
        physicalAp = _physicalAp;
        magicalAp = _magicalAp;
        physicalDp = _physicalDp;
        magicalDp = _magicalDp;
        mSpeed = pSpeed = 1;
        speed = _speed;
    }

    public void PhysicalDamage(int _ap)
    {
        int damage = (_ap - physicalDp);
        hp = hp - ((damage < 0) ? 0 : damage);
        healthBar.fillAmount = hp / startHP;
        Debug.Log("Phy damage: " + damage);
    }

    public void MagicalDamage(int _ap)
    {
        int damage = (_ap - magicalDp);
        hp = hp - ((damage < 0) ? 0 : damage);
        healthBar.fillAmount = hp / startHP;
        Debug.Log("_ap " + _ap);
        Debug.Log("magicalDp" + magicalDp);
        Debug.Log("Mag damage: " + damage);
    }

    public void SetMP(int _mp)
    {
        mp -= _mp;
    }


    public int GetHP()
    {
        return hp;
    }

    public int GetMP()
    {
        return mp;
    }

    public int GetPAP()
    {
        return physicalAp;
    }

    public int GetMAP()
    {
        return magicalAp;
    }

    public int GetPDP()
    {
        return physicalDp;
    }

    public int GetMDP()
    {
        return magicalDp;
    }

    public double GetpSpeed()
    {
        return pSpeed;
    }

    public double GetmSpeed()
    {
        return mSpeed;
    }

    public void EquipWeapon(Weapon weapon)
    {
        this.photonView.RPC("RPCEquipWeapon", PhotonTargets.All, (int)weapon.type, weapon.apRate, weapon.speedRate);
        this.photonView.RPC("RPCWeaponBuff", PhotonTargets.All, (int)weapon.type, weapon.apRate, weapon.speedRate);

    }


    public void WeaponBuff()
    {
        Debug.Log("equipArmor " + equipArmor.type);
        this.photonView.RPC("RPCWeaponBuff", PhotonTargets.All, (int)equipWeapon.type, equipWeapon.apRate, equipWeapon.speedRate);
      
    }

    public void EquipArmor(Armor armor)
    {
        this.photonView.RPC("RPCEquipArmor", PhotonTargets.All, (int)armor.type, armor.pdpRate, armor.mdpRate, armor.speed);
        this.photonView.RPC("RPCArmorBuff", PhotonTargets.All, (int)armor.type, armor.pdpRate, armor.mdpRate, armor.speed);
    }

    public void ArmorBuff()
    {
        this.photonView.RPC("RPCArmorBuff", PhotonTargets.All, (int)equipArmor.type, equipArmor.pdpRate, equipArmor.mdpRate, equipArmor.speed);
    }

    public PunTeams.Team GetTeam()
    {
        return team;
    }

    [PunRPC]
    private void SetTeam(PunTeams.Team _team)
    {
        team = _team;
    }

    [PunRPC]
    private void RPCEquipWeapon(int type, double apRate, double speedRate)
    {
        Weapon weapon = new Weapon((WeaponAbility.Weapon)type, apRate, speedRate);
        equipWeapon = weapon;

    }

    [PunRPC]
    private void RPCWeaponBuff(int type, double apRate, double speedRate)
    {
        Debug.Log("WeaponBuff " + type);
        
        

        if ((WeaponAbility.Weapon)type == WeaponAbility.Weapon.AXE || (WeaponAbility.Weapon)type == WeaponAbility.Weapon.SWORD)
        {
            physicalAp = (int)(physicalAp + physicalAp * apRate);
            Debug.Log("WeaponBuff " + physicalAp);
        }
        else
        {
            magicalAp = (int)(magicalAp + magicalAp * apRate);
        }
        pSpeed = pSpeed + pSpeed * speedRate;
        Debug.Log("WeaponBuff " + pSpeed);
    }

    [PunRPC]
    private void RPCEquipArmor(int type, double pdpRate, double mdpRate, double speedRate)
    {
        Armor armor = new Armor((ArmorAbility.Armor)type, pdpRate, mdpRate, speedRate);
        equipArmor = armor;
        Debug.Log("equipArmor type " + equipArmor.type);
    }

    [PunRPC]
    private void RPCArmorBuff(int type, double pdpRate, double mdpRate, double speedRate)
    {
        if((ArmorAbility.Armor)type == ArmorAbility.Armor.BOOT)
        {
            speed = speed + speed * speedRate;   
            characterMovement.SetMovement((float)speed, (float)speed + 8);
            Debug.Log("Movement is called " + speed);
        }
        else
        {
            physicalDp = (int)(physicalDp + physicalDp * pdpRate);
            magicalAp = (int)(magicalDp + magicalDp * mdpRate);
        }
        
    }
}
