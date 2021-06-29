using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InstantiatePopUpTextController : MonoBehaviour
{
    [SerializeField] private GameObject textGOSerializable;

    // static variables
    private static GameObject _textGO;
    private static Transform _canvasTransform;
    private static TextMeshProUGUI _currentText;
    private static GameObject _currentGO;

    private void Awake()
    {
        _textGO = textGOSerializable;
        _canvasTransform = FindObjectOfType<Canvas>().transform;
    }

    public static void InstantiateText(Vector3 position, string message, float size)
    {
        // TODO: don`t forget about dividing canvaces

        _currentText = _textGO.GetComponent<TextMeshProUGUI>();

        _currentText.text = message;
        _currentText.fontSize = size;

        _currentGO = Instantiate(_textGO, position, Quaternion.identity);
        _currentGO.transform.SetParent(_canvasTransform);
    }
}