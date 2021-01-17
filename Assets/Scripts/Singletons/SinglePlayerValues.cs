using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerValues : MonoBehaviour
{
  // private variables
  public float damageValue = 20;
  public float miningSpeedValue = 1;
  public float lightRadiusValue = 4.5f;

  // static variables
  static public SinglePlayerValues pointLightValue;

  private void Awake()
  {
    if (pointLightValue != null)
      Destroy(pointLightValue);
    else
    {
      pointLightValue = this;
      DontDestroyOnLoad(pointLightValue);
    }
  }
}
