using System;
using System.Collections;
using System.Collections.Generic;
using ICSharpCode.NRefactory.Ast;
using TMPro;
using UnityEngine;

public class OpenModalMenu : MonoBehaviour
{
    [SerializeField] private GameObject modalMenu;

    // local variables
    [SerializeField] public Ore ore;
    private BuyOrSellMenu _buyOrSellMenu;
    [HideInInspector] public TextMeshProUGUI textMeshProUGUI;

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

        Debug.Log(ore.name);
        _buyOrSellMenu.SetUp(this);
    }
}