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

    private static float _money;

    public static float money
    {
        get { return _money; }
        set
        {
            _money = value;
            onValueChange();
        }
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
        SinglePlayerValues.moneyAmount = money;
    }
}