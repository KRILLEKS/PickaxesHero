using UnityEngine;

public class PauseButtonController : MonoBehaviour
{
  [SerializeField]
  private GameObject pausePanel;
  [SerializeField]
  private GameObject pauseButton;

  public void SwitchDisplay()
  {
    Switch(pausePanel);
    Switch(pauseButton);

    void Switch(GameObject objectToSwitch)
    {
      if (!objectToSwitch.activeSelf)
        objectToSwitch.SetActive(true);
      else
        objectToSwitch.SetActive(false);
    }
  }
}
