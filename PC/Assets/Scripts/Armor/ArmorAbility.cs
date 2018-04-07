using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorAbility{

    public enum Armor { ARMOUR,BOOT, CLOAK };
    public static double[] increaseRateMain = new double[] { 0.03, 0.03, 0.03, 0.03, 0.03, 0.03, 0.06, 0.06, 0.06, 0.09 };
    public static double[] increaseRateSupport = new double[] { 0.013, 0.013, 0.013, 0.013, 0.013, 0.013, 0.016, 0.016, 0.016, 0.019 };

    private Armor type;
    private double pdpIncreaseRate = 0;
    private double mdpIncreaseRate = 0;
    private double speedIncreaseRate = 0;




    public ArmorAbility(Armor armor)
    {
        type = armor;
        switch (type)
        {
            case Armor.ARMOUR:
                pdpIncreaseRate = RandomRateMain();
                mdpIncreaseRate = RandomRateSupport();
                break;
            case Armor.CLOAK:
                pdpIncreaseRate = RandomRateSupport();
                mdpIncreaseRate = RandomRateMain();
                break;
            case Armor.BOOT:
                speedIncreaseRate = RandomRateSupport();
                break;


        }
    }

    public double GetpdpIncreaseRate()
    {
        return pdpIncreaseRate;
    }


    public double GetmdpIncreaseRate()
    {
        return mdpIncreaseRate;
    }

    public double GetSpeedIncreaseRate()
    {
        return speedIncreaseRate;
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
