using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatisticsData
{
    public int maxLevelReached;
    public bool[] minedOres;

    public StatisticsData()
    {
        maxLevelReached = Statistics.maxLevelReached;
        minedOres = Statistics.minedOres;
    }
}
