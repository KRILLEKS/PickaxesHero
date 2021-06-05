using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BankData
{
    public int upgradeIndex;
    public float progressAmount;

    public BankData()
    {
        upgradeIndex = Bank.upgradeIndex;
        progressAmount = Bank.progressAmount;
    }
}