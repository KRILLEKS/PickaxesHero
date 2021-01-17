using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAllMenus : MonoBehaviour
{
  [SerializeField]
  private GameObject[] menus;

  public void Close()
  {
    foreach (GameObject obj in menus)
      if (obj.activeSelf)
        obj.SetActive(false);

    gameObject.SetActive(false);
  }
}
