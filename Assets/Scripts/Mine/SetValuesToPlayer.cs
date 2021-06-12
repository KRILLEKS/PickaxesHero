using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetValuesToPlayer : MonoBehaviour
{
    [SerializeField]
    UnityEngine.Experimental.Rendering.Universal.Light2D light2D;

    private void Start()
    {
        FindObjectOfType<Mining>().damage = SinglePlayerValues.damageValue;

        FindObjectOfType<CharacterAnimatorController>().SetAnimationsSpeed(
            SinglePlayerValues
                .miningSpeedValue);

        light2D.pointLightOuterRadius = SinglePlayerValues.lightRadiusValue;
    }
}