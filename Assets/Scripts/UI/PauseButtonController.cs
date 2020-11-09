using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonController : MonoBehaviour
{
  [SerializeField]
  private GameObject pausePanel;

  // local variables
  bool isPauseMenuActive = false;

  public void SwitchPauseMenu()
  {
    if (!isPauseMenuActive)
      pausePanel.SetActive(true);
    else
      pausePanel.SetActive(false);

    isPauseMenuActive = !isPauseMenuActive;
  }
}
