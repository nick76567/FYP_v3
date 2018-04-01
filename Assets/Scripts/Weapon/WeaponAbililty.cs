using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAbility{
    public enum Weapon { AXE, BOW, STAFF, SWORD };
    public static double[] increaseRateMain = new double[] { 0.03, 0.03, 0.03, 0.03, 0.03, 0.03, 0.06, 0.06, 0.06, 0.09 };
    public static double[] increaseRateSupport = new double[] { 0.013, 0.013, 0.013, 0.013, 0.013, 0.013, 0.016, 0.016, 0.016, 0.019 };

    private Weapon type;
    private double papIncreaseRate = 0;
    private double mapIncreaseRate = 0;
    private double pSpeedIncreaseRate = 0;
    private double mSpeedIncreaseRate = 0;



    public WeaponAbility(Weapon weapon)
    {
        type = weapon;
        switch (type)
        {
            case Weapon.AXE:
                papIncreaseRate = RandomRateMain();
                pSpeedIncreaseRate = RandomRateSupport();
                break;
            case Weapon.SWORD:
                papIncreaseRate = RandomRateSupport();
                pSpeedIncreaseRate = RandomRateMain();
                break;
            case Weapon.STAFF:
                mapIncreaseRate = RandomRateMain();
                mSpeedIncreaseRate = RandomRateSupport();
                break;
            case Weapon.BOW:
                mapIncreaseRate = RandomRateSupport();
                mSpeedIncreaseRate = RandomRateMain();
                break;

        }
    }

    public double GetPapIncreaseRate()
    {
        return papIncreaseRate;
    }

    public double GetMapIncreaseRate()
    {
        return mapIncreaseRate;
    }

    public double GetpSpeedIncreaseRate()
    {
        return pSpeedIncreaseRate;
    }

    public double GetmSpeedIncreaseRate()
    {
        return mSpeedIncreaseRate;
    }

    private double RandomRateMain()
    {
        return increaseRateMain[Random.Range(0, 10)];
    }

    private double RandomRateSupport()
    {
        return increaseRateSupport[Random.Range(0, 10)];
    }

}
