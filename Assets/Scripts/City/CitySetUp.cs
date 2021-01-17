using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySetUp : MonoBehaviour
{
  private void Start()
  {
    FindObjectOfType<GridBehavior>().GenerateGrid();
  }
}
