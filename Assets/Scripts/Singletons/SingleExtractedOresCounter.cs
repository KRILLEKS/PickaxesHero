using System.Text.RegularExpressions;
using UnityEngine;

public class SingleExtractedOresCounter : MonoBehaviour
{
  // local variables
  public int[] ores = new int[23]; // all ores

  // static variables
  static SingleExtractedOresCounter singleExtractedOresCounter;

  private void Awake()
  {
    if (singleExtractedOresCounter != null)
      Destroy(singleExtractedOresCounter);
    else
    {
      singleExtractedOresCounter = this;
      DontDestroyOnLoad(gameObject);
    }
  }

  public void GetOre(string oreName)
  {
    Regex regex = new Regex(@"^\d*");
    oreName = regex.Match(oreName).Value; // gets ore number

    int index = int.Parse(oreName) - 1; // gets index

    ores[index]++;
  }

  public void LoadOres(ExtractedOresData extractedOresData) =>
      ores = extractedOresData.ores;
}
