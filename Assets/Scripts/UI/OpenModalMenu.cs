using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenModalMenu : MonoBehaviour
{
    [SerializeField] public BuyOrSell _buyOrSell;
    [SerializeField] private GameObject modalMenu;

    // local variables
    [SerializeField] public Ore ore;
    private BuyOrSellMenu _buyOrSellMenu;
    [HideInInspector] public TextMeshProUGUI textMeshProUGUI;

    public enum BuyOrSell
    {
        Buy,
        Sell
    }

    private void Awake()
    {
        textMeshProUGUI = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        _buyOrSellMenu = modalMenu.GetComponent<BuyOrSellMenu>();
    }

    public void SetOre(Ore ore)
    {
        this.ore = ore;
    }

    public void OpenMenu()
    {
        modalMenu.SetActive(true);

        _buyOrSellMenu.SetUp(this);
    }
}