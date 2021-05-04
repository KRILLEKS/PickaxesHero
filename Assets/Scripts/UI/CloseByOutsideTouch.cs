using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseByOutsideTouch : MonoBehaviour
{
    [SerializeField] private GameObject[] menus;

    public void ClosePanels()
    {
        Debug.Log("all menus was closed");

        foreach (GameObject obj in menus)
            if (obj.activeSelf)
                obj.SetActive(false);

        gameObject.SetActive(false);
    }
}