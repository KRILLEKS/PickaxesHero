using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GenerateLevelSelector : MonoBehaviour
{
    [SerializeField] private GameObject content;
    
    // local variables
    private GameObject objectToDuplicate;
    private GameObject currentObject;
    private int indexer = 5; // defines which level will be generated

    public void GenerateLevels()
    {
        objectToDuplicate = content.transform.GetChild(0).gameObject;
        objectToDuplicate.GetComponent<LevelSelectorButton>().whichLevel2Load = 1;
        objectToDuplicate.GetComponentInChildren<TextMeshProUGUI>().text = "1";

        GenerateOtherLevels();
        
        void GenerateOtherLevels()
        {
            for (; indexer <= Statistics.maxLevelReached - 5; indexer += 5)
            {
                currentObject = Instantiate(objectToDuplicate, content.transform);
                currentObject.GetComponent<LevelSelectorButton>().whichLevel2Load = indexer;
                currentObject.GetComponentInChildren<TextMeshProUGUI>().text = indexer.ToString();
            }
        }
    }
}
