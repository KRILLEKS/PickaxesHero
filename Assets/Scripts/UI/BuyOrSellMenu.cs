using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class BuyOrSellMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI whatOreToBuy;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private TextMeshProUGUI money;


    // local variables
    private TMP_InputField inputField;
    private OpenModalMenu openModalMenu;
    private Ore ore;
    private float totalCost;

    private void Awake()
    {
        inputField = transform.GetComponentInChildren<TMP_InputField>();
    }

    public void SetUp(OpenModalMenu openModalMenu)
    {
        this.openModalMenu = openModalMenu;
        ore = openModalMenu.ore;

        whatOreToBuy.text = "buy " + ore.name;
        inputField.text = "0";
    }

    public void BuyButton()
    {
        openModalMenu.textMeshProUGUI.text = inputField.text;

        gameObject.SetActive(false);
    }

    public void SetCost()
    {
        totalCost = int.Parse(inputField.text) * ore.buyPrice;

        Regex regex = new Regex(@"\d+");
        cost.text = regex.Replace(cost.text, totalCost.ToString(), 1);
    }
}