using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetValuesToPlayer : MonoBehaviour
{
  [SerializeField]
  UnityEngine.Experimental.Rendering.Universal.Light2D light2D;

  // global variables
  SinglePlayerValues playerValues;

  private void Start()
  {
    playerValues = FindObjectOfType<SinglePlayerValues>();

    FindObjectOfType<Mining>().damage = playerValues.damageValue;
    FindObjectOfType<ChracterAnimatorController>().SetAnimationsSpeed(playerValues.miningSpeedValue);
    light2D.pointLightOuterRadius = playerValues.lightRadiusValue;
  }
}
