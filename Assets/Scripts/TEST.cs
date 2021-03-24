using System.Linq;
using UnityEngine;

public class TEST : MonoBehaviour
{
    [SerializeField] bool turnLightOn = false;

    [Space] [SerializeField] private bool needsToLoadLevel = false;
    [SerializeField] int level = 1;
    [SerializeField] private bool infinityMoney = true;

    [Space] [SerializeField] private GameObject background;
    [SerializeField] private GameObject globalLight;
    [SerializeField] private GameObject playersLight;

    DescentToTheNextLevel descentToTheNextLevel;

    private void Start()
    {
        if (needsToLoadLevel)
            LoadLevel();

        if (turnLightOn)
            SwithLight();

        if (infinityMoney)
            SetInfinityMoney();
    }

    private void LoadLevel()
    {
        descentToTheNextLevel = FindObjectOfType<DescentToTheNextLevel>();

        descentToTheNextLevel.needsToLoadNextLevel = true;
        FindObjectOfType<OreGenerator>().currentLevel = level;
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

    private void SetInfinityMoney()
    {
        Debug.Log(" Infinity money");
        SingleExtractedOresCounter.ores = SingleExtractedOresCounter.ores
            .Select(ore
                => ore = 10000).ToArray();
    }
}