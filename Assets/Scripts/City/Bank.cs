using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bank : MonoBehaviour
{
    [SerializeField] private Slider progressSlider;

    // static variables
    public static float progressAmount;
    private static float upgradeCost;
    public static int upgradeIndex = 1;

    private void Awake()
    {
        if (upgradeIndex == 1)
        {
            SetUpgradeValues();
            upgradeCost = 1; // because it can`t be equal 0
        }

        UpdateSliderValue();
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
        upgradeIndex++;
        SetUpgradeValues();
    }

    private static void SetUpgradeValues()
    {
        upgradeCost = Mathf.Pow(upgradeIndex, 3) - Mathf.Pow(upgradeIndex, 2);
        SellingController.sellSpeed = Mathf.Pow(upgradeIndex, 3) / 10;
    }

    private void UpdateSliderValue()
    {
        progressSlider.value = progressAmount / upgradeCost;
    }

    public static void LoadData(BankData bankData)
    {
        upgradeIndex = bankData.upgradeIndex;
        SetUpgradeValues();
        progressAmount = bankData.progressAmount;
    }
}