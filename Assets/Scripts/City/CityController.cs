using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class CityController : MonoBehaviour
{
    [SerializeField] private GameObject filler;

    // public variables
    public UnityEvent onDescending;
    public enum buildings
    {
        descent,
        building
    }

    // local variables
    private CsGlobal _csGlobal;
    private BuildingDefinition _buildingDefinition;
    private GameManager _gameManager;
    private void Awake()
    {
        if (onDescending == null)
            new UnityEvent();
        
        _csGlobal = FindObjectOfType<CsGlobal>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    // invokes ob touch
    public void CheckTarget()
    {
        _buildingDefinition = null;

        var hitInfo = Physics2D.OverlapPoint(_csGlobal.g_mousePosition);
        if (hitInfo != null)
            _buildingDefinition = hitInfo.GetComponent<BuildingDefinition>();
    }

    // invokes on player stop
    public void AtStop()
    {
        if (_buildingDefinition != null)
            switch (_buildingDefinition.building)
            {
                case buildings.descent:
                    _gameManager.LoadMine();
                    onDescending.Invoke();
                    break;
                case buildings.building:
                    _buildingDefinition.menuToOpen.SetActive(true);
                    filler.SetActive(true);
                    break;
                default:
                    Debug.LogError("ERROR");
                    break;
            }
    }
}