using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityController : MonoBehaviour
{
  [SerializeField]
  private GameObject shopMenu;
  [SerializeField]
  private GameObject closeButton;

  // local variables
  private bool needsToLoadMine = false;
  private bool needsToOpenShopMenu = false;

  // invokes ob touch
  public void CheckTarget()
  {
    Collider2D hitInfo = Physics2D.OverlapPoint(FindObjectOfType<CsGlobal>().g_mousePosition);
    if (hitInfo && hitInfo.tag == "Descent")
      needsToLoadMine = true;
    if (hitInfo && hitInfo.tag == "Shop")
      needsToOpenShopMenu = true;
  }

  // invokes on player stop
  public void AtStop()
  {
    if (needsToLoadMine)
      FindObjectOfType<GameManager>().LoadMine();
    if (needsToOpenShopMenu)
      OpenMenu(shopMenu, ref needsToOpenShopMenu);

    void OpenMenu(GameObject menu, ref bool menuController)
    {
      menu.SetActive(true);
      menuController = false;
      closeButton.SetActive(true);
    }
  }
}
