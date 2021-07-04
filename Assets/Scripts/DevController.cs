using System.Linq;
using UnityEngine;

public class DevController : MonoBehaviour
{
    [SerializeField] private bool turnItOn = false;
    [SerializeField] private bool infinityOres = true;
    [SerializeField] private bool decreaseOres2NextLevel;
    
    private void Start()
    {
        if (turnItOn == false)
            return;
        
        if (decreaseOres2NextLevel)
            Ores2NextLevel();
        
        if (infinityOres)
            SetInfinityOres();
    }

    private void SetInfinityOres()
    {
        Debug.Log(" Infinity money");

        for (var ore = 0; ore < Constants.oresAmount; ore++)
        {
            Statistics.minedOres[ore] = true;
        }
        SingleExtractedOresCounter.ores = SingleExtractedOresCounter.ores
            .Select(ore
                => ore = 10000).ToArray();
    }

    private void Ores2NextLevel()
    {
        FindObjectOfType<ProgressBar>().slider.maxValue = 2; // can be changed
    }
}