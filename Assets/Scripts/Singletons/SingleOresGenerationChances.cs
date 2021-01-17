using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleOresGenerationChances : MonoBehaviour
{
  public List<int[]> generationChances = new List<int[]>();

  // static variables
  static SingleOresGenerationChances oresGenerationChances;

  private void Awake()
  {
    if (oresGenerationChances != null)
      Destroy(oresGenerationChances);
    else
    {
      oresGenerationChances = this;
      DontDestroyOnLoad(oresGenerationChances);
    }
  }
}
