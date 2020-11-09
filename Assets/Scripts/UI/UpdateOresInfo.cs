using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateOresInfo : MonoBehaviour
{
  // global variables
  private ExtractedOresCounter extractedOresCounter;

  // local variables
  private TextMeshProUGUI[] values;

  private void Awake()
  {
    extractedOresCounter = FindObjectOfType<ExtractedOresCounter>();

    // gameObject == content
    int children = gameObject.transform.childCount;
    values = new TextMeshProUGUI[children];

    for (int i = 0; i < children; i++)
      values[i] = gameObject.transform.GetChild(i).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
  }

  public void SetValues()
  {
    for (int i = 0; i < values.Length; i++)
      values[i].text = extractedOresCounter.ores[i].ToString();
  }
}
