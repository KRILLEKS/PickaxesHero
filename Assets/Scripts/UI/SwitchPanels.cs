using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPanels : MonoBehaviour
{
    [SerializeField] private GameObject bigPanel;
    [SerializeField] private GameObject listPanel;

    public void Switch()
    {
        bigPanel.SetActive(!bigPanel.activeSelf);
        listPanel.SetActive(!listPanel.activeSelf);
    }
}