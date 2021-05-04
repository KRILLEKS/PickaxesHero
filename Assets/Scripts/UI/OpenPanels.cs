using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPanels : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;

    public void OpenPanel()
    {
        foreach (var panel in panels)
        {
            if (!panel.activeSelf)
                panel.SetActive(true);
        }
    }
}
