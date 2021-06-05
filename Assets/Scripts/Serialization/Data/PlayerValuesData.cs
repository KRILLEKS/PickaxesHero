using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerValuesData
{
    public float damageValue;
    public float miningSpeedValue;
    public float lightRadiusValue;

    public float moneyAmount;
    
    public PlayerValuesData()
    {
        damageValue = SinglePlayerValues.damageValue;
        miningSpeedValue = SinglePlayerValues.miningSpeedValue;
        lightRadiusValue = SinglePlayerValues.lightRadiusValue;

        moneyAmount = SinglePlayerValues.moneyAmount;
    }
}
