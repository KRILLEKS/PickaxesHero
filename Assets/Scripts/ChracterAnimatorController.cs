using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class ChracterAnimatorController : MonoBehaviour
{
  // private varuables
  private Animator animator;

  private void Awake()
  {
    animator = gameObject.GetComponent<Animator>();
  }
  public void SetValues(Vector3 nextPosition)
  {
    animator.SetFloat("Horizontal", Mathf.Round(nextPosition.x - gameObject.transform.position.x));
    animator.SetFloat("Vertical", Mathf.Round(nextPosition.y - gameObject.transform.position.y));
  }

  public void InvertChillVar() =>
    animator.SetBool("Chill", !animator.GetBool("Chill"));

  public void InvertIsMiningVar() =>
    animator.SetBool("IsMining", !animator.GetBool("IsMining"));

  public void EndMining()
  {
    if (animator.GetBool("IsMining"))
      animator.SetBool("IsMining", false);
  }

  // it`s set samples for all mining animation, so it`s like mine rate
  public void SetAnimationsSpeed(float speed) =>
    animator.SetFloat("AnimationsSpeed", speed);
}