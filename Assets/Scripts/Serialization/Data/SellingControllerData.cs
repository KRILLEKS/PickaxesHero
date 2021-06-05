using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SellingControllerData
{
    public float sellSpeed;
    public Dictionary<int, int> oresToSell;

    public SellingControllerData()
    {
        sellSpeed = SellingController.sellSpeed;
        oresToSell = SellingController._oresToSell;
    }
}
