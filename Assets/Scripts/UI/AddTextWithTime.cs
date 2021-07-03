using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddTextWithTime : MonoBehaviour
{
    [SerializeField] private float timeBetweenChanges = 1f;
    [SerializeField] private string text2Add;

    // local variables 
    private TextMeshProUGUI _textMeshProUGUI;
    private int _symbol2Add = 0;
    
    private void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        StartCoroutine(textAdder());
    }

    private IEnumerator textAdder()
    {
        while (_symbol2Add < text2Add.Length)
        {
            _textMeshProUGUI.text += text2Add[_symbol2Add++];
            yield return new WaitForSeconds(timeBetweenChanges);
        }
    }
}
