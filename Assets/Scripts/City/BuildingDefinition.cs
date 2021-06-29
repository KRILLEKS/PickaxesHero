using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingDefinition : MonoBehaviour
{
    [SerializeField] public CityController.buildings building;
    [SerializeField] public GameObject menuToOpen;
    [SerializeField] public UnityEvent clickAction;

    private void Awake()
    {
        if (clickAction == null)
            new UnityEvent();
    }
}
