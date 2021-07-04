using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OresHider : MonoBehaviour
{
    [SerializeField] private GameObject content;

    // local variables
    private int elementsInChild;
    private readonly bool[] isOreUnlocked = new bool[Constants.oresAmount];
    private Transform[,] allElements;

    private void Awake()
    {
        elementsInChild =
            content.transform.GetChild(0).childCount;
        allElements = new Transform[Constants.oresAmount, elementsInChild];

        for (int oreGO = 0; oreGO < Constants.oresAmount; oreGO++)
        {
            for (int oreElement = 0; oreElement < elementsInChild; oreElement++)
            {
                allElements[oreGO, oreElement] =
                    content.transform.GetChild(oreGO).GetChild(oreElement);
            }
        }
    }

    private void OnEnable()
    {
        for (int ore = 0; ore < Constants.oresAmount; ore++)
        {
            ChangeOre(false, ore);
        }
    }

    public void UpdateOres()
    {
        for (int ore = 0; ore < Constants.oresAmount; ore++)
        {
            if (isOreUnlocked[ore] == false &&
                Statistics.minedOres[ore])
            {
                ChangeOre(true, ore);
            }
        }
    }

    // true for unlock ore, false for hide it
    void ChangeOre(bool unlock, int ore)
    {
        for (int element = 0; element < elementsInChild; element++)
        {
            if (allElements[ore, element].CompareTag($"Hidden"))
            {
                if (unlock && isOreUnlocked[ore] == false)
                    ChangeElementActivity(false);
            }
            else
            {
                ChangeElementActivity(unlock);
            }

            void ChangeElementActivity(bool activity)
            {
                allElements[ore, element].gameObject.SetActive(activity);
            }
        }

        isOreUnlocked[ore] = unlock;
    }
}