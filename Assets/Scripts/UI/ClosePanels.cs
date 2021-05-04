using UnityEngine;

public class ClosePanels : MonoBehaviour
{
    [SerializeField] private GameObject[] panelsToClose;

    public void ClosePanel()
    {
        foreach (var panel in panelsToClose)
        {
            if (panel.activeSelf)
                panel.SetActive(false);
        }
    }
}