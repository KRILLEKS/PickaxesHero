using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonusOresMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    
    // local values
    private OreGenerator _oreGenerator;

    private void Awake()
    {
        _oreGenerator = FindObjectOfType<OreGenerator>();
    }

    public void UpdateValue()
    {
        text.text = "Level " + _oreGenerator.currentLevel;
    }
}
