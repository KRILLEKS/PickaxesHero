using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// this script should be on content
public class UpdateOresInfo : MonoBehaviour
{
    // local variables
    private TextMeshProUGUI[] _textMeshProUguis = new
        TextMeshProUGUI[Constants.oresAmount];

    private void Awake()
    {
        for (int i = 0; i < Constants.oresAmount; i++)
        {
            if (gameObject.activeSelf)
                _textMeshProUguis[i] = gameObject.transform.GetChild(i).Find
                    ("Amount").gameObject.GetComponent<TextMeshProUGUI>();
        }
    }

    public void SetContentInfo()
    {
        SetValues(_textMeshProUguis, SingleExtractedOresCounter.ores);
    }

    public void SetSellInfo()
    {
        // TODO: WHAT IS THIS
        int[] values = new int[Constants.oresAmount];

        for (int i = 0; i < Constants.oresAmount; i++)
        {
            if (SellingController._oresToSell.ContainsKey(i))
                values[i] = SellingController._oresToSell[i];
            else
                values[i] = 0;
        }

        SetValues(_textMeshProUguis, values);
    }

    public static void SetValues(TextMeshProUGUI[] textMeshProUGUI,
        int[] values)
    {
        for (int i = 0; i < Constants.oresAmount; i++)
            if (textMeshProUGUI[i] != null)
                textMeshProUGUI[i].text =
                    values[i].ToString();
    }
}