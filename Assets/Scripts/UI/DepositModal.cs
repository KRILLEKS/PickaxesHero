using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class DepositModal : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI oreName;
    [SerializeField] private GameObject bankMenuGO;

    [SerializeField] private float[] progressReceivedPerOre =
        new float[Constants.fillerIndexes.Length];

    // local variables
    private Bank _bank;
    private Ore _ore;
    private int _fillerIndex;
    private TMP_InputField _inputField;
    private int amountToDeposit;
    private float progressToAdd;

    private void Awake()
    {
        _bank = FindObjectOfType<Bank>();
        _inputField = gameObject.GetComponentInChildren<TMP_InputField>();
    }

    public void SetUp(Ore ore, int fillerIndex)
    {
        _ore = ore;
        _fillerIndex = fillerIndex;
        oreName.text = _ore.name;
        _inputField.text = "0";
    }

    public void DepositButton()
    {
        amountToDeposit = int.Parse(_inputField.text);

        if (SingleExtractedOresCounter.ores[_ore.index] >= amountToDeposit)
        {
            // transaction
            SingleExtractedOresCounter.ores[_ore.index] -= amountToDeposit;
            
            progressToAdd =
                progressReceivedPerOre[_fillerIndex] * amountToDeposit;

            _bank.AddProgress(progressToAdd);
            
            // close menu
            bankMenuGO.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            // TODO: popup menu "not enough resources"
        }
    }
}