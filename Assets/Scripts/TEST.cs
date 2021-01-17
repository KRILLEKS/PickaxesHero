using UnityEngine;

public class TEST : MonoBehaviour
{
  [SerializeField]
  bool turnLightOn = false;

  [Space]
  [SerializeField]
  int level = 1;

  [Space]
  [SerializeField]
  private GameObject background;
  [SerializeField]
  private GameObject globalLight;
  [SerializeField]
  private GameObject playersLight;

  DescentToTheNextLevel descentToTheNextLevel;

  private void Start()
  {
    LoadLevel();

    if (turnLightOn)
      SwithLight();
  }
  private void LoadLevel()
  {
    descentToTheNextLevel = FindObjectOfType<DescentToTheNextLevel>();

    descentToTheNextLevel.needsToLoadNextLevel = true;
    FindObjectOfType<OreGenerator>().indexer = level;
    descentToTheNextLevel.LoadNextLevel();
    descentToTheNextLevel.needsToLoadNextLevel = false;
  }
  private void SwithLight()
  {
    if (background.activeSelf)
      background.SetActive(false);
    else
      background.SetActive(true);

    if (playersLight.activeSelf)
      playersLight.SetActive(false);
    else
      playersLight.SetActive(true);

    if (!globalLight.activeSelf)
      globalLight.SetActive(true);
    else
      globalLight.SetActive(false);
  }

}

