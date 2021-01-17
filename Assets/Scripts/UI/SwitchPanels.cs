using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPanels : MonoBehaviour
{
  [SerializeField]
  private GameObject bigPanel;
  [SerializeField]
  private GameObject listPanel;

  public void Switch()
  {
    if (bigPanel.activeSelf)
      bigPanel.SetActive(false);
    else
      bigPanel.SetActive(true);

    if (listPanel.activeSelf)
      listPanel.SetActive(false);
    else
      listPanel.SetActive(true);
  }
}
