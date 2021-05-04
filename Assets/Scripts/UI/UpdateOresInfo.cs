using UnityEngine;
using TMPro;

public class UpdateOresInfo : MonoBehaviour
{
    [SerializeField] private GameObject bigPanelContent;
    [SerializeField] private GameObject listPanelContent;

    // global variables
    private SingleExtractedOresCounter extractedOresCounter;

    // local variables
    private TextMeshProUGUI[] bigPanelValues;
    private TextMeshProUGUI[] listPanelValues;

    private void Awake()
    {
        extractedOresCounter = FindObjectOfType<SingleExtractedOresCounter>();
        int oresAmount = bigPanelContent.transform.childCount;

        SetListPanelArray();
        SetBigPanelArray();

        void SetListPanelArray()
        {
            listPanelValues = new TextMeshProUGUI[oresAmount];
            int iterator = 0;

            for (int i = 0; i < listPanelContent.transform.childCount; i++)
                for (int j = 0;
                    j < listPanelContent.transform.GetChild(i).childCount;
                    j++)
                    listPanelValues[iterator++] = listPanelContent.transform
                        .GetChild(i).GetChild(j).GetChild(0).gameObject
                        .GetComponent<TextMeshProUGUI>();
        }

        void SetBigPanelArray()
        {
            // gameObject == content
            bigPanelValues = new TextMeshProUGUI[oresAmount];

            for (int i = 0; i < oresAmount; i++)
                bigPanelValues[i] = bigPanelContent
                                    .transform.GetChild(i).Find("Amount")
                                    .gameObject.GetComponent<TextMeshProUGUI>();
        }
    }

    public void SetValues()
    {
        for (int i = 0; i < bigPanelValues.Length; i++)
            bigPanelValues[i].text =
                SingleExtractedOresCounter.ores[i].ToString();

        for (int i = 0; i < listPanelValues.Length; i++)
            listPanelValues[i].text =
                SingleExtractedOresCounter.ores[i].ToString();
    }
}