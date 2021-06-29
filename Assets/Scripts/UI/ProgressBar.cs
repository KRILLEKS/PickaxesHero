using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    private Image filler;
    private RawImage border;
    private NextLevelLoadController _nextLevelLoadController;

    // local variables
    [HideInInspector] public int value = 0;

    private void Awake()
    {
        border = gameObject.GetComponentInChildren<RawImage>();
        filler = gameObject.GetComponentInChildren<Image>();
        slider = gameObject.GetComponentInChildren<Slider>();
        _nextLevelLoadController = FindObjectOfType<NextLevelLoadController>();
    }

    public void LoadProgressBar(ProgressBarData progressBarData)
    {
        value = progressBarData.value;
        slider.value = value;

        if ((value == slider.maxValue || value > slider.maxValue) &&
            _nextLevelLoadController.descentWasSpawned)
        {
            DisableProgressBar();
        }
        else if ((value == slider.maxValue || value > slider.maxValue) &&
                 !_nextLevelLoadController.descentWasSpawned)
        {
            EnableProgressBar();
            ReachedMaxValue();
        }
        else
            EnableProgressBar();
    }

    public void IncreaseValue()
    {
        if (_nextLevelLoadController.descentWasSpawned == false)
            slider.value = ++value;

        if (value == slider.maxValue)
            ReachedMaxValue();
    }

    private void ReachedMaxValue()
    {
        ChangeFillerColour();

        void ChangeFillerColour()
        {
            filler.color = Color.green;
        }
    }

#region ProgressBarController
    
    public void DisableProgressBar()
    {
        Reset();

        filler.gameObject.SetActive(false);
        border.gameObject.SetActive(false);
    }

    public void EnableProgressBar()
    {
        filler.gameObject.SetActive(true);
        border.gameObject.SetActive(true);
    }

    public void Reset()
    {
        slider.value = 0;
        value = 0;

        filler.color = Color.white;
    }
    
    public bool  ReachedMaxVal()
    {
        return slider.maxValue == slider.value;
    }

#endregion
}