using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SellingController : MonoBehaviour
{
    // determines how frequently sell coroutine will be invoked
    [SerializeField] private float waitTime = 0.1f;

    // it`s content in sell menu
    [SerializeField] private GameObject sellMenuContent;
    [SerializeField] private GameObject oresMenuContent;
    [SerializeField] private GameObject listOresMenuContent;

    // public variables
    // it determines how much gold u can potentially earn per second
    public float sellSpeed;

    // private variables
    [HideInInspector]
    public List<TextMeshProUGUI> textValues = new List<TextMeshProUGUI>();

    private UpdateOresInfo _updateOresInfo;
    private UpdateListOresInfo _updateListOresInfo;
    private Ore[] ores;

    private IEnumerator coroutine;
    private int keyIndex;
    private bool canISellOre;
    private float moneyOnIteration;
    private float moneyInThePool; // extra money, that player didn't receive yet

    // static variables
    // key - index, amount
    public static Dictionary<int, int>
        _oresToSell = new Dictionary<int, int>();

    private void Awake()
    {
        SinglePlayerValues.oresToSell = _oresToSell;
        ores = Constants.GetOres();
        _updateOresInfo = oresMenuContent.GetComponent<UpdateOresInfo>();
        _updateListOresInfo =
            listOresMenuContent.GetComponent<UpdateListOresInfo>();

        FindTextValues();

        void FindTextValues()
        {
            var children =
                sellMenuContent.transform
                               .GetComponentsInChildren<Transform>();

            foreach (var child in children)
            {
                if (child.gameObject.CompareTag("Value"))
                    textValues.Add(child.gameObject
                                        .GetComponent<TextMeshProUGUI>());
            }

            if (textValues.Count != Constants.oresAmount)
                Debug.LogError("ERROR!");
        }
    }

    public void StartSellCoroutine()
    {
        if (coroutine == null)
        {
            coroutine = sellCoroutine();
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator sellCoroutine()
    {
        while (_oresToSell.Count > 0)
        {
            for (int i = 0; i < _oresToSell.Count; i++)
            {
                if (SingleExtractedOresCounter.ores
                        [_oresToSell.ElementAt(i).Key] >
                    0)
                {
                    canISellOre = true;
                    keyIndex = _oresToSell.ElementAt(i).Key;
                    break;
                }
                else
                {
                    keyIndex = -1;
                }
            }

            // it`s in if clause, because we don`t want to get pool money if player can`t sell ore
            if (canISellOre)
            {
                // how much money will be distributed in this iteration
                moneyOnIteration = sellSpeed * waitTime;
                moneyInThePool += moneyOnIteration;
            }

            if (keyIndex != -1 &&
                ores[keyIndex].sellPrice <=
                moneyInThePool &&
                SingleExtractedOresCounter.ores[keyIndex] != 0)
            {
                moneyInThePool -= ores[keyIndex].sellPrice;
                MoneyController.money += ores[keyIndex].sellPrice;

                SingleExtractedOresCounter.ores[keyIndex]--;

                _oresToSell[keyIndex]--;

                ChangeTextValues(keyIndex);

                // check if we sell all ore of one type
                if (_oresToSell[keyIndex] <= 0)
                {
                    _oresToSell.Remove(keyIndex);
                }
            }

            canISellOre = false;
            yield return new WaitForSeconds(waitTime);
        }

        coroutine = null;
    }

    public void ChangeTextValues(int index)
    {
        if (sellMenuContent.activeSelf)
        {
            textValues[index].text = _oresToSell[index].ToString();
        }

        if (oresMenuContent.activeSelf)
        {
            _updateOresInfo.SetContentInfo();
        }

        if (listOresMenuContent.activeSelf)
        {
            _updateListOresInfo.SetContentInfo();
        }
    }
}