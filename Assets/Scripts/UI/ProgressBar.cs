using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
  [SerializeField]
  private GameObject progressBar;
  [SerializeField]
  private Image progressBarImg;
  private Slider slider;
  private Button button;
  private Image filler;
  private DescentToTheNextLevel descentToTheNextLevel;

  // local variables
  public int value = 0;

  private void Awake()
  {
    filler = gameObject.GetComponentInChildren<Image>();
    slider = gameObject.GetComponentInChildren<Slider>();
    descentToTheNextLevel = FindObjectOfType<DescentToTheNextLevel>();
    button = gameObject.GetComponentInChildren<Button>();
  }

  public void LoadProgressBar(ProgressBarData progressBarData)
  {
    value = progressBarData.value;
    slider.value = value;

    if ((value == slider.maxValue || value > slider.maxValue) && descentToTheNextLevel.descentWasSpawned)
    {
      DisableProgressBar();
      Reset();
    }
    else if ((value == slider.maxValue || value > slider.maxValue) && !descentToTheNextLevel.descentWasSpawned)
    {
      EnableProgressBar();
      ReachedMaxValue();
    }
    else
      EnableProgressBar();
  }

  public void IncreaseValue()
  {
    slider.value = ++value;

    if (value == slider.maxValue)
      ReachedMaxValue();
  }

  private void ReachedMaxValue()
  {
    GenerateNextLevelButton();
    ChangeFillerColour();
    ChangeImageColor();

    void GenerateNextLevelButton() =>
        button.enabled = true;
    void ChangeFillerColour() =>
        filler.color = Color.green;
    void ChangeImageColor() =>
    progressBarImg.color = Color.green;
  }

  #region ProgressBarController

  public void DisableProgressBar()
  {
    progressBar.SetActive(false);
    button.enabled = false;
  }

  public void EnableProgressBar() =>
      progressBar.SetActive(true);

  public void Reset()
  {
    slider.value = 0;
    value = 0;
    filler.color = Color.white;
  }

  #endregion
}
