using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyOrSellMenu : MonoBehaviour
{
    // it`s the name of ore that we work with
    [Space] [SerializeField] private TextMeshProUGUI whatOre;

    // it`s income or cost
    [SerializeField] private TextMeshProUGUI moneyAmount;

    [SerializeField] private GameObject transactionButton;

    // local variables
    private OpenModalMenu _allInfo;
    private TMP_InputField _inputField;
    private Ore _ore;
    private int _oreAmount;
    private float _totalMoney;
    private SellingController _sellingController;
    private Slider _slider;

    private void Awake()
    {
        _inputField = transform.GetComponentInChildren<TMP_InputField>();
        _sellingController = FindObjectOfType<SellingController>();
        _slider = transform.GetComponentInChildren<Slider>();
    }

    public void SetUp(OpenModalMenu openModalMenu)
    {
        _allInfo = openModalMenu;
        _ore = _allInfo.ore;

        switch (_allInfo._buyOrSell)
        {
            case OpenModalMenu.BuyOrSell.Buy:
                whatOre.text = "Buy " + _ore.name;
                _slider.maxValue = (float)(Math.Floor(MoneyController.money / _allInfo.ore.buyPrice));
                break;

            case OpenModalMenu.BuyOrSell.Sell:
                whatOre.text = "Sell " + _ore.name;
                _slider.maxValue = SingleExtractedOresCounter.ores[_ore.index];
                break;
        }

        _inputField.text = "0";
        _slider.value = 0;
    }

    public void BuyButton()
    {
        // buy
        if (MoneyController.money >= _totalMoney)
        {
            MoneyController.money -= _totalMoney;
            SingleExtractedOresCounter.ores[_ore.index] += _oreAmount;

            // we will also remove ore that we bought from selling
            if (SellingController._oresToSell.ContainsKey(_ore.index))
            {
                SellingController._oresToSell.Remove(_ore.index);
                _sellingController.textValues[_ore.index].text = "0";
            }

            gameObject.SetActive(false);
        }
        else
        {
            InstantiatePopUpTextController.InstantiateText(transactionButton.transform.position,
                                                           "not enough money",
                                                           15);
            // TODO: work on it
        }
    }

    public void SellButton()
    {
        // Set amount to sell
        _allInfo.textMeshProUGUI.text = _oreAmount.ToString();

        if (_oreAmount <= 0)
            SellingController._oresToSell.Remove(_ore.index);
        else if (SellingController._oresToSell.ContainsKey(_ore.index))
            SellingController._oresToSell[_ore.index] = _oreAmount;
        else
        {
            SellingController._oresToSell.Add(_ore.index, _oreAmount);
            SellingController._oresToSell.OrderBy(valuePair => valuePair.Key);
            _sellingController.StartSellCoroutine();
        }

        gameObject.SetActive(false);
    }

    public void SetCost()
    {
        _oreAmount = int.Parse(_inputField.text);
        if (_oreAmount > _slider.maxValue)
        {
            _oreAmount = Mathf.FloorToInt(_slider.maxValue);
            _inputField.text = _oreAmount.ToString();
        }

        switch (_allInfo._buyOrSell)
        {
            case OpenModalMenu.BuyOrSell.Buy:
                _totalMoney = _oreAmount * _ore.buyPrice;
                break;
            case OpenModalMenu.BuyOrSell.Sell:
                _totalMoney = _oreAmount * _ore.sellPrice;
                break;
        }

        Regex regex = new Regex(@"\d+");
        moneyAmount.text =
            regex.Replace(moneyAmount.text, _totalMoney.ToString(), 1);
    }

    public void OnSliderValueChange()
    {
        _inputField.text = _slider.value.ToString();
        SetCost();
    }
}