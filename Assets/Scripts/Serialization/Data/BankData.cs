using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BankData
{
    public float upgradeCost;
    public float sellingSpeedIncrease;
    public float progressAmount;

    public BankData()
    {
        upgradeCost = Bank.upgradeCost;
        sellingSpeedIncrease = Bank.sellingSpeedIncrease;
        progressAmount = Bank.progressAmount;
    }
}