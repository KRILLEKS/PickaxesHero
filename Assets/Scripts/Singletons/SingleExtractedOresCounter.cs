﻿using System;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Linq;

public class SingleExtractedOresCounter : MonoBehaviour
{
    [SerializeField] private static float extraOresPercentage = 0.5f;

    // local variables
    public static int[] ores = new int[Constants.oresAmount]; // all ores

    private static int[] thisLevelOres = new int[Constants.oresAmount]; // ores that were get on current level

    // static variables
    static SingleExtractedOresCounter singleExtractedOresCounter;

    private void Awake()
    {
        if (singleExtractedOresCounter != null)
            Destroy(singleExtractedOresCounter);
        else
        {
            singleExtractedOresCounter = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void GetOre(int index, int amount = 1)
    {
        ores[index] += amount;
        thisLevelOres[index] += amount;
    }

    public static void GetExtraOres()
    {
        ores = thisLevelOres.Select(i => (int) (i * extraOresPercentage))
                                    .Zip(ores, (int i, int j) => i + j).ToArray();

        thisLevelOres = thisLevelOres.Select(ore => ore = 0).ToArray();
    }

    public void LoadOres(ExtractedOresData extractedOresData) =>
        ores = extractedOresData.ores;
}