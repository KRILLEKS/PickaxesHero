using System.Collections;
using UnityEngine;

public class Mining : MonoBehaviour
{
  [SerializeField]
  private float mineRate = 1f;
  [SerializeField]
  private float damage = 20f;

  // global variables
  private CsGlobal csGlobal;

  // local variables
  private Vector3 blockPosition = new Vector3();
  private OreDurability block; // block to mine

  private void Awake()
  {
    csGlobal = FindObjectOfType<CsGlobal>();
  }

  // Invokes on touch and sets block value
  public void SelectBlockToMine()
  {
    StopAllCoroutines();

    blockPosition = csGlobal.g_mousePosition;
    Collider2D hitInfo = Physics2D.OverlapPoint(blockPosition);

    if (hitInfo && hitInfo.tag == "Ore")
      block = hitInfo.GetComponent<OreDurability>();
    else
      block = null;
  }

  // Invokes on PlayerStop and Start MineCoroutine
  public void Mine()
  {
    if (block)
      StartCoroutine(MineCoroutine());

    // Player starts mining
    IEnumerator MineCoroutine()
    {
      while (block)
      {
        block.TakeDamage(damage);
        yield return new WaitForSeconds(1 / mineRate);
      }
    }
  }
}
