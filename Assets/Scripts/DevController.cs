using System.Linq;
using UnityEngine;

public class DevController : MonoBehaviour
{
    [SerializeField] private bool infinityOres = true;
//    [SerializeField] private bool loadLevel = true;
//    [SerializeField] private int levelIndex = 1;
    
    #if UNITY_EDITOR
    private void Start()
    {
        if (infinityOres)
            SetInfinityOres();
//        if (loadLevel || GameManager.GetSceneIndex() == 2)
//        {
//            Debug.Log("Set level");
//            LoadLevel();
//        }
    }
    #endif

    private void SetInfinityOres()
    {
        Debug.Log(" Infinity money");
        SingleExtractedOresCounter.ores = SingleExtractedOresCounter.ores
            .Select(ore
                => ore = 10000).ToArray();
    }

//    private void LoadLevel()
//    {
//        OreGenerator.currentLevel = levelIndex;
//        FindObjectOfType<NextLevelLoadController>().LoadNextLevel();
//    }
}