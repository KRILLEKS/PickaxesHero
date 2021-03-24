using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OresData : MonoBehaviour
{
  public int indexer;
  public List<float> randNumbers;

  public OresData(OreGenerator oreGenerator)
  {
    indexer = oreGenerator.currentLevel;
    randNumbers = oreGenerator.randNumbers;
  }
}
