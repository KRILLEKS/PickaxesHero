using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChancesData
{
    public float[] chances;

    public ChancesData(ChancesGenerator chancesGenerator)
    {
        chances = chancesGenerator.previousLevelChances;
    }
}
