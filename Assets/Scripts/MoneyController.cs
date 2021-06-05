using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MoneyController : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField] private TextMeshProUGUI moneyTextSerializable;

    // static variable
    private static TextMeshProUGUI moneyText;

    public static float money
    {
        get { return SinglePlayerValues.moneyAmount; }
        set
        {
            SinglePlayerValues.moneyAmount = value;
            onValueChange();
        }
    }

    private void Awake()
    {
        onValueChange();
    }

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        moneyText = moneyTextSerializable;
    }

    private static void onValueChange()
    {
        moneyText.text = money.ToString();
    }
}