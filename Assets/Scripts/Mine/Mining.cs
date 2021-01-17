﻿using System.Collections;
using UnityEngine;

public class Mining : MonoBehaviour
{
  [SerializeField]
  public float damage = 20f;

  // global variables
  private CsGlobal csGlobal;
  private GridBehavior gridBehavior;
  private ChracterAnimatorController animatorController;

  // local variables
  private Vector3 blockPosition = new Vector3();
  private OreDurability block; // block to mine


  private void Awake()
  {
    csGlobal = FindObjectOfType<CsGlobal>();
    gridBehavior = FindObjectOfType<GridBehavior>();
    animatorController = FindObjectOfType<ChracterAnimatorController>();
  }

  // Invokes on touch and sets block value
  public void SelectBlockToMine()
  {
    StopAllCoroutines();

    blockPosition = csGlobal.g_mousePosition;
    Collider2D hitInfo = Physics2D.OverlapPoint(blockPosition);

    if (hitInfo && hitInfo.tag == "Ore" && gridBehavior.CanAchive(blockPosition))
      block = hitInfo.GetComponent<OreDurability>();
    else
      block = null;
  }

  // Invokes on PlayerStop and Start MineCoroutine
  public void InvokeMiningAnimation()
  {
    if (block)
    {
      animatorController.SetValues(block.transform.position);
      animatorController.InvertIsMiningVar();
    }
  }
  public void DamageOre()
  {
    if (block)
      block.TakeDamage(damage);
  }
}