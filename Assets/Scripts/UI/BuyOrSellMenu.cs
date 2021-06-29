using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class BuyOrSellMenu : MonoBehaviour
{
    // it`s the name of ore that we work with
    [Space] [SerializeField] private TextMeshProUGUI whatOre;

    // it`s income or cost
    [SerializeField] private TextMeshProUGUI moneyAmount;

    [SerializeField] private GameObject transactionButton;

    // local variables
    private OpenModalMenu _openModalMenu;
    private TMP_InputField inputField;
    private Ore ore;
    private int oreAmount;
    private float totalMoney;
    private SellingController _sellingController;

    private void Awake()
    {
        inputField = transform.GetComponentInChildren<TMP_InputField>();
        _sellingController = FindObjectOfType<SellingController>();
    }

    public void SetUp(OpenModalMenu openModalMenu)
    {
        _openModalMenu = openModalMenu;
        ore = _openModalMenu.ore;

        switch (_openModalMenu._buyOrSell)
        {
            case OpenModalMenu.BuyOrSell.Buy:
                whatOre.text = "Buy " + ore.name;
                break;

            case OpenModalMenu.BuyOrSell.Sell:
                whatOre.text = "Sell " + ore.name;
                break;
        }

        inputField.text = "0";
    }

    public void BuyButton()
    {
        // buy
        if (MoneyController.money >= totalMoney)
        {
            MoneyController.money -= totalMoney;
            SingleExtractedOresCounter.ores[ore.index] += oreAmount;

            // we will also remove ore that we bought from selling
            if (SellingController._oresToSell.ContainsKey(ore.index))
            {
                SellingController._oresToSell.Remove(ore.index);
                _sellingController.textValues[ore.index].text = "0";
            }

            gameObject.SetActive(false);
        }
        else
        {
            InstantiatePopUpTextController.InstantiateText(transactionButton.transform.position,
                                                           "not enough money",
                                                           15);
        }
    }

    public void SellButton()
    {
        // Set amount to sell
        _openModalMenu.textMeshProUGUI.text = oreAmount.ToString();

        if (oreAmount <= 0)
            SellingController._oresToSell.Remove(ore.index);
        else if (SellingController._oresToSell.ContainsKey(ore.index))
            SellingController._oresToSell[ore.index] = oreAmount;
        else
        {
            SellingController._oresToSell.Add(ore.index, oreAmount);
            SellingController._oresToSell.OrderBy(valuePair => valuePair.Key);
            _sellingController.StartSellCoroutine();
        }

        gameObject.SetActive(false);
    }

    public void SetCost()
    {
        oreAmount = int.Parse(inputField.text);

        switch (_openModalMenu._buyOrSell)
        {
            case OpenModalMenu.BuyOrSell.Buy:
                totalMoney = oreAmount * ore.buyPrice;
                break;
            case OpenModalMenu.BuyOrSell.Sell:
                totalMoney = oreAmount * ore.sellPrice;
                break;
        }

        Regex regex = new Regex(@"\d+");
        moneyAmount.text =
            regex.Replace(moneyAmount.text, totalMoney.ToString(), 1);
    }
}