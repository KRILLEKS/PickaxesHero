using System.Collections;
using UnityEngine;

public class ClosePanels : MonoBehaviour
{
    [SerializeField] private GameObject[] panelsToClose;

    public void ClosePanel()
    {
        StartCoroutine(ClosePanelsWithDelay());
    }

    private IEnumerator ClosePanelsWithDelay()
    {
        yield return new WaitForSeconds(0.02f);
        
        foreach (var panel in panelsToClose)
        {
            if (panel.activeSelf)
                panel.SetActive(false);
        }
    }
}