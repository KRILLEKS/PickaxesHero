using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    // values
    public static int maxLevelReached;
    public static bool[] minedOres = new bool[Constants.oresAmount];

    // local values
    private static Statistics _statistics;

    private void Awake()
    {
        if (_statistics != null)
        {
            Destroy(_statistics);
            _statistics = this;
        }
        
        DontDestroyOnLoad(this);
    }

    public static void SetMaxLevelReached(int value)
    {
        if (maxLevelReached > value == false)
            maxLevelReached = value;
    }

    public static void GetNewMinedOre(int index)
    {
        minedOres[index] = true;
    }

    public static void LoadData(StatisticsData data)
    {
        maxLevelReached = data.maxLevelReached;
        minedOres = data.minedOres;
    }
}