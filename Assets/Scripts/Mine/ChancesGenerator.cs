using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChancesGenerator : MonoBehaviour
{
  [SerializeField]
  float fillerPercentage = 76f;
  [SerializeField]
  float stoneDecreasePerLvl = 3f; // how many percent will stone give to ore in first chances generation
  [SerializeField]
  float talmallitIncreasePerLvl = 1f;
  [SerializeField]
  float desctroyTailPercentage = 2.5f;
  [SerializeField]
  float tailDecreasePercentage = 2.5f;
  [SerializeField]
  float lastOresValue = 0.1f; // it needs for last ores generation part

  // private variables
  Dictionary<float, float> oresChanceChanges = new Dictionary<float, float>();
  Dictionary<int, float> fillerChanceChanges = new Dictionary<int, float>();

  // local variables
  private float[] previousLevelChances = new float[Constants.oresAmount];
  private int lastOresIndex = 0; // it needs for last ores generation part  
  int[] fillersIndexes = new int[Constants.fillerIndexes.Length];
  int[] oresIndexes = new int[Constants.oresAmount - Constants.fillerIndexes.Length];
  int tailIndex = 0; //use in Delete tails method

  void Start()
  {
    FillDictionaries();
    SeparateOres();

    previousLevelChances[0] = 100f; // first chance
  }
  private void FillDictionaries()
  {
    // TODO: ask on forums
    // TODO: extend comment
    // key - on which chance which change will be. Value - change value
    oresChanceChanges.Add(15f, 2f);
    oresChanceChanges.Add(12f, 1.5f);
    oresChanceChanges.Add(10f, 1.2f);
    oresChanceChanges.Add(7.5f, 1f);
    oresChanceChanges.Add(5f, 0.5f);
    oresChanceChanges.Add(3f, 0.35f);
    oresChanceChanges.Add(1f, 0.2f);
    oresChanceChanges.Add(0.75f, 0.1f);
    oresChanceChanges.Add(0.5f, 0.01f);

    // key it`s index. - it`s before filler. + it`s after filler. Value - change value
    fillerChanceChanges.Add(-2, 0.2f);
    fillerChanceChanges.Add(-1, 1f);
    fillerChanceChanges.Add(1, 2f);
    fillerChanceChanges.Add(2, 3.5f);
    fillerChanceChanges.Add(3, 6.5f);
    fillerChanceChanges.Add(4, 50f);
  }

  #region auxiliary methods

  void SeparateOres()
  {
    for (int i = 0, fillerIndex = 0, oresIndex = 0; i < Constants.oresAmount; i++)
    {
      bool isOre = true;

      foreach (var index in Constants.fillerIndexes) // checks is it ore or filler
        if (index == i)
          isOre = false;

      // TODO: i == index && (isOre = false);     logical expression c#

      if (isOre) // add ores
      {
        oresIndexes[oresIndex++] = i;
      }
      else // add fillers
      {
        fillersIndexes[fillerIndex++] = i;
      }
    }
  }
  void MoveOresChances()
  {
    // we don`t want to interact with ore before tallmalit and with ores after talmallit, because we`ll interact with them throught other percentage generator
    for (int i = oresIndexes.Length - 1 - (Constants.oresAmount - fillersIndexes[fillersIndexes.Length - 1]); i >= 0; i--)
      if ((previousLevelChances[oresIndexes[i]] != 0) && (i != oresIndexes.Length - 1))
        MoveOreChance(i);

    DeleteTails();

    void MoveOreChance(int i)
    {
      for (int j = 0; j < oresChanceChanges.Count; j++)
      {
        if (previousLevelChances[oresIndexes[i]] >= oresChanceChanges.Keys.ElementAt(j))
        {
          previousLevelChances[oresIndexes[i]] -= oresChanceChanges.Values.ElementAt(j);
          previousLevelChances[oresIndexes[i + 1]] += oresChanceChanges.Values.ElementAt(j);
          break;
        }
      }
    }
  }
  void MoveFillerChances()
  {
    // cycle goes throught all fillers except talmallit
    for (int i = 0; i < fillersIndexes.Length - 1; i++) // -2, because we don`t want to do smth with tallmalit
    {
      for (int j = 0; j < fillerChanceChanges.Count - 1; j++)
      {
        // checks need to move chances or no
        if (previousLevelChances[fillersIndexes[i]] > 0
          && (previousLevelChances[fillersIndexes[i + 1] + fillerChanceChanges.Keys.ElementAt(j)] > 0))
        {
          MoveChance(i);
          break;
        }
      }
    }

    void MoveChance(int index)
    {
      // for go via all filler change keys
      for (int j = fillerChanceChanges.Count - 1; j >= 0; j--)
      {
        // if don`t check ores before stone, because there no ores
        if ((index != 0 || fillerChanceChanges.Keys.ElementAt(j) > 0)
        && (previousLevelChances[fillersIndexes[index + 1] + fillerChanceChanges.Keys.ElementAt(j)] > 0)) // checks wich percentage to add
        {
          previousLevelChances[fillersIndexes[index]] -= fillerChanceChanges.Values.ElementAt(j);
          previousLevelChances[fillersIndexes[index + 1]] += fillerChanceChanges.Values.ElementAt(j);

          // it`s delete extra percentage, for example stone == -1, basalt == 101, if something like that happen it will be deleted
          // so after that part stone == 0, basalt = 100
          if (previousLevelChances[fillersIndexes[index]] < 0)
          {
            previousLevelChances[fillersIndexes[index + 1]] += previousLevelChances[fillersIndexes[index]];
            previousLevelChances[fillersIndexes[index]] = 0;
          }

          break;
        }
      }
    }
  }
  void DeleteTails()
  {
    if (previousLevelChances[oresIndexes[tailIndex]] < previousLevelChances[oresIndexes[tailIndex + 1]])
    {
      if (previousLevelChances[oresIndexes[tailIndex]] <= desctroyTailPercentage)
      {
        previousLevelChances[oresIndexes[tailIndex + 1]] += previousLevelChances[oresIndexes[tailIndex]];
        previousLevelChances[oresIndexes[tailIndex]] = 0;
        if (tailIndex != oresIndexes.Length - 2)
          tailIndex++;
      }
      else
      {
        previousLevelChances[oresIndexes[tailIndex]] -= tailDecreasePercentage;
        previousLevelChances[oresIndexes[tailIndex + 1]] += tailDecreasePercentage;
      }
    }
  }

  #endregion

  #region chances generation

  bool GenerateFirstChances()
  {
    if (previousLevelChances[0] > fillerPercentage)
    {
      previousLevelChances[0] -= stoneDecreasePerLvl;
      previousLevelChances[1] += stoneDecreasePerLvl;

      MoveOresChances();

      return true;
    }

    return false;
  }
  bool GenerateSecondChances()
  {
    if (previousLevelChances[fillersIndexes[fillersIndexes.Length - 1]] <= fillerPercentage) // filler before tallamlit
    {
      MoveOresChances();
      MoveFillerChances();
      return true;
    }

    return false;
  }
  bool GenerateThirdChances()
  {
    if (previousLevelChances[fillersIndexes[fillersIndexes.Length - 1]] < 100) // transform ore before talmallit into tallmalit
    {
      previousLevelChances[fillersIndexes[fillersIndexes.Length - 1] - 1] -= talmallitIncreasePerLvl;
      previousLevelChances[fillersIndexes[fillersIndexes.Length - 1]] += talmallitIncreasePerLvl;

      if (previousLevelChances[fillersIndexes[fillersIndexes.Length - 1]] > 100) // delete tail
      {
        previousLevelChances[fillersIndexes[fillersIndexes.Length - 1] - 1] = 0;
        previousLevelChances[fillersIndexes[fillersIndexes.Length - 1]] = 100;
      }

      return true;
    }

    return false;
  }
  void GenerateFourthChances()
  {
    // we have 2 variables. lastOresIndex and lastOresValue
    // every iteration first from last ores gets +lastOresValue
    // every two iterations second from last ores gets +lastOresValue
    // every three iteration third from last ores gets +lastOresValue
    // ....

    // TODO: save index, in GenerationOresIndex.save

    for (int i = 1; i <= previousLevelChances.Length - oresIndexes.Length; i++) // iterate via all last ores
    {
      if (lastOresIndex % i == 0)
      {
        previousLevelChances[fillersIndexes[fillersIndexes.Length - 1]] -= lastOresValue;
        previousLevelChances[fillersIndexes[fillersIndexes.Length - 1] + i] += lastOresValue;
        lastOresIndex++;
      }
    }
  }

  #endregion

  public float[] GetChance()
  {
    // TODO: how to make it scalable
    if (GenerateFirstChances())
      return previousLevelChances;
    else if (GenerateSecondChances())
      return previousLevelChances;
    else if (GenerateThirdChances())
      return previousLevelChances;
    else
      GenerateFourthChances();

    return previousLevelChances;
  }
}
