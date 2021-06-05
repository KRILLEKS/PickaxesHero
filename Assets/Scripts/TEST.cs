using System.Linq;
using UnityEngine;

public class TEST : MonoBehaviour
{
    [Space] [SerializeField] bool turnLightOn = false;

    [Space] [SerializeField] private bool needsToLoadLevel = false;
    [SerializeField] int level = 1;
    [SerializeField] private bool infinityMoney = true;

    [Space] [SerializeField] private GameObject background;
    [SerializeField] private GameObject globalLight;
    [SerializeField] private GameObject playersLight;

    NextLevelLoadController _nextLevelLoadController;

    private void Start()
    {
        if (infinityMoney)
            SetInfinityMoney(); 
        
        if (needsToLoadLevel)
            LoadLevel();

        if (turnLightOn)
            SwithLight();
    }

    private void LoadLevel()
    {
        _nextLevelLoadController = FindObjectOfType<NextLevelLoadController>();

        _nextLevelLoadController.LoadNextLevel();
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

    private void SetInfinityMoney()
    {
        Debug.Log(" Infinity money");
        SingleExtractedOresCounter.ores = SingleExtractedOresCounter.ores
            .Select(ore
                => ore = 10000).ToArray();
    }

    public void WriteInConsole()
    {
        Debug.Log("Touch");
    }
}