using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OresData
{
    public int indexer;
    public List<float> randNumbers;

    public OresData(OreGenerator oreGenerator)
    {
        indexer = oreGenerator.indexer;
        randNumbers = oreGenerator.randNumbers;
    }
}
