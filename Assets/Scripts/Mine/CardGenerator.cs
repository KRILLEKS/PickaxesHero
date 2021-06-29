using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class CardGenerator : MonoBehaviour
{
    [SerializeField] private int cardsAmount = 15;
    [SerializeField] private GameObject content;

    // local variables
    private ChancesGenerator _chancesGenerator;
    private GameObject[] _cards;
    private Random _random;
    private float _randVal;
    private float _totalChance;
    private float _chance;
    private Transform[] _previousTransform;
    private GameObject _currentGO;
    private int amount;

    // chances
    private readonly float[,]
        _chances =
            new float[6, 3]; // first val - chance, second - start amount, third - end amount 

    private int chancesCounter = 0;

    private void Awake()
    {
        _cards = Resources.LoadAll<GameObject>("Cards");
        _chancesGenerator = FindObjectOfType<ChancesGenerator>();
        _random = new Random();

        GetChances();
    }

    private void GetChances()
    {
        // there are chances.GetLength(0) percentages:
        // 1 - very rare chance on extremely low amount - 6.5
        // 2 - rare chance on low amount - 20
        // 3 - average chance on average amount - 40
        // 4 - rare chance on average+ amount - 20
        // 5 - very rare chance on high amount - 10
        // 6 - extremely rare chance on very high amount - 3.5

        SetChance(6.5f, 8);
        SetChance(20f, 20);
        SetChance(40f, 50);
        SetChance(20f, 75);
        SetChance(10f, 100);
        SetChance(3.5f, 250);

        void SetChance(float percentage, int endAmount)
        {
            _chances[chancesCounter, 0] = percentage;
            _chances[chancesCounter, 1] = chancesCounter == 0 ? 0 : _chances[chancesCounter - 1, 2];
            _chances[chancesCounter, 2] = endAmount;

            chancesCounter++;
        }
    }

    public void GenerateCards()
    {
        if (content.transform.childCount > 0)
            ClearContent();

        for (var i = 0; i < cardsAmount; i++)
        {
            _currentGO = Instantiate(GenerateCard(), content.transform);

            amount = GetAmount();
            _currentGO.GetComponentInChildren<TextMeshProUGUI>().text = amount.ToString();
            _currentGO.GetComponent<Card>().amount = amount;
        }

        void ClearContent()
        {
            _previousTransform = content.transform.GetComponentsInChildren<Transform>();

            for (var index = 1;
                 index < _previousTransform.Length;
                 index++) // we start from 1 because we don`t want to interact with parent
            {
                var trans = _previousTransform[index];
                Destroy(trans.gameObject);
            }
        }

        GameObject GenerateCard()
        {
            _totalChance = 0;
            _randVal = (float) (_random.NextDouble() * 100);

            for (var i = 0; i < Constants.oresAmount; i++)
            {
                _chance = _chancesGenerator.currentLevelChances[i];

                if (_chance == 0)
                    continue;

                _totalChance += _chance;
                if (_totalChance > _randVal)
                    return _cards[i];
            }

            Debug.LogError("CARD WASN'T GENERATED");
            return null;
        }

        int GetAmount()
        {
            _totalChance = 0;
            _randVal = (float) (_random.NextDouble() * 100);

            for (var i = 0; i < _chances.GetLength(0); i++)
            {
                _totalChance += _chances[i,0];

                if (_totalChance > _randVal)
                {
                    return _random.Next((int)_chances[i, 1], (int)_chances[i, 2]);
                }
            }

            Debug.LogError("WRONG CARD AMOUNT");
            return -1;
        }
    }
}