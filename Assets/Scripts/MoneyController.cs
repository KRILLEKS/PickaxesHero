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
    public static TextMeshProUGUI moneyText;

    private static int _money;

    public static int money
    {
        get { return _money; }
        set
        {
            onValueChange();
            _money = value;
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
    }
}