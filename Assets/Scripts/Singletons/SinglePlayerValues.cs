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

  public static float moneyAmount;

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

    moneyAmount = data.moneyAmount;
  }
}
