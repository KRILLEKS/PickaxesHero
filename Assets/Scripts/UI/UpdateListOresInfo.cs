using UnityEngine;
using TMPro;

// this script should be on content
// this script separated with UpdateOresInfo, because list content has another structure
public class UpdateListOresInfo : MonoBehaviour
{
    private TextMeshProUGUI[] listPanelValues =
        new TextMeshProUGUI[Constants.oresAmount];

    private void Awake()
    {
        int iterator = 0;

        // firstly we get amount of pages
        for (int i = 0; i < transform.childCount; i++)
            // amount of values on each page
            for (int j = 0;
                j <
                transform.GetChild(i).childCount;
                j++)
                listPanelValues[iterator++] = transform
                                              .GetChild(i).GetChild(j)
                                              .GetChild(0).gameObject
                                              .GetComponent<
                                                  TextMeshProUGUI>();
    }

    public void SetContentInfo()
    {
        UpdateOresInfo.SetValues(listPanelValues,SingleExtractedOresCounter.ores);
    }
}