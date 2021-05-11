using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositButton : MonoBehaviour
{
    [SerializeField] private int fillerIndex;
    [SerializeField] private GameObject modalMenu;

    // local variables
    private Ore ore;
    private DepositModal _depositModal;

    private void Awake()
    {
        ore = Constants.GetOre(Constants.fillerIndexes[fillerIndex]);
    }

    public void OpenModalMenu()
    {
        transform.parent.gameObject.SetActive(false);
        modalMenu.SetActive(true);

        if (_depositModal == null)
            _depositModal = modalMenu.GetComponent<DepositModal>();

        _depositModal.SetUp(ore, fillerIndex);
    }
}