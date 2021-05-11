using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bank : MonoBehaviour
{
    [SerializeField] private float firstUpgradeCost = 100;
    [SerializeField] private Slider progressSlider;
    
    // public variables
    public static float upgradeCost = 0;
    
    // static variables
    private static SellingController _sellingController;
    public static float sellingSpeedIncrease = 2;
    public static float progressAmount = 0f;

    private void Awake()
    {
        _sellingController = FindObjectOfType<SellingController>();
        
        SinglePlayerValues.sellSpeedValue = _sellingController.sellSpeed;

        if (upgradeCost == 0)
            upgradeCost = firstUpgradeCost;
    }

    public void AddProgress(float progress)
    {
        progressAmount += progress;

        if (progressAmount >= upgradeCost)
        {
            progressAmount -= upgradeCost;
            
            Upgrade();
        }
        
        UpdateSliderValue();
    }
    
    private static void Upgrade()
    {
        // TODO: make formula
        upgradeCost += 100;

        _sellingController.sellSpeed += sellingSpeedIncrease;
        SinglePlayerValues.sellSpeedValue += sellingSpeedIncrease;

        // TODO: make formula
        sellingSpeedIncrease += 2;
    }

    public void UpdateSliderValue()
    {
        progressSlider.value = progressAmount / upgradeCost;
    }

    public void LoadData(BankData bankData)
    {
        upgradeCost = bankData.upgradeCost;
        sellingSpeedIncrease = bankData.sellingSpeedIncrease;
        progressAmount = bankData.progressAmount;
    }
}
