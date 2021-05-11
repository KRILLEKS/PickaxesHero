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

    public float sellSpeedValue;
    public float moneyAmount;
    public Dictionary<int, int> oresToSell;

    public int sceneIndex;
    
    public PlayerValuesData()
    {
        damageValue = SinglePlayerValues.damageValue;
        miningSpeedValue = SinglePlayerValues.miningSpeedValue;
        lightRadiusValue = SinglePlayerValues.lightRadiusValue;

        sellSpeedValue = SinglePlayerValues.sellSpeedValue;
        oresToSell = SinglePlayerValues.oresToSell;
        moneyAmount = SinglePlayerValues.moneyAmount;

        sceneIndex = SinglePlayerValues.sceneIndex;
    }
}
