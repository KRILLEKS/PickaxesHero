using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerValues : MonoBehaviour
{
  // private variables
  public static float damageValue = 20;
  public static float miningSpeedValue = 1;
  public static float lightRadiusValue = 4.5f;

  public static float sellSpeedValue;
  public static float moneyAmount;
  public static Dictionary<int, int> oresToSell;

  // determines in which scene player left the game
  public static int sceneIndex = 1;

  // static variables
  private static SinglePlayerValues singlePlayerValues;

  private void Awake()
  {
    if (singlePlayerValues != null)
      Destroy(singlePlayerValues);
    else
    {
      singlePlayerValues = this;
      DontDestroyOnLoad(singlePlayerValues);
    }
  }

  public static void LoadData(PlayerValuesData data)
  {
    damageValue = data.damageValue;
    miningSpeedValue = data.miningSpeedValue;
    lightRadiusValue = data.lightRadiusValue;

    sellSpeedValue = data.sellSpeedValue;
    oresToSell = data.oresToSell;
    moneyAmount = data.moneyAmount;

    sceneIndex = data.sceneIndex;
  }
}
