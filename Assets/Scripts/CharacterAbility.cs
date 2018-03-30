using System.Collections;
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

    private Weapon equipWeapon;
    private PunTeams.Team team;




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
        healthBar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(int _hp, int _mp, int _physicalAp, int _magicalAp, int _physicalDp, int _magicalDp)
    {
        startHP = hp = _hp;
        mp = _mp;
        physicalAp = _physicalAp;
        magicalAp = _magicalAp;
        physicalDp = _physicalDp;
        magicalDp = _magicalDp;
        mSpeed = pSpeed = 1;

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
        //equipWeapon = weapon;
        //WeaponBuff();
    }


    public void WeaponBuff()
    {
        this.photonView.RPC("RPCWeaponBuff", PhotonTargets.All, (int)equipWeapon.type, equipWeapon.apRate, equipWeapon.speedRate);
        //Debug.Log("WeaponBuff " + equipWeapon.type);
        //Debug.Log("WeaponBuff " + physicalAp);
        //Debug.Log("WeaponBuff " + pSpeed);

        //if (equipWeapon.type == WeaponAbility.Weapon.AXE || equipWeapon.type == WeaponAbility.Weapon.SWORD)
        //{
        //    physicalAp = (int)(physicalAp + physicalAp * equipWeapon.apRate);
        //}
        //else
        //{
        //    magicalAp = (int)(magicalAp + magicalAp * equipWeapon.apRate);
        //}
        //pSpeed = pSpeed + pSpeed * equipWeapon.speedRate;
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
}
