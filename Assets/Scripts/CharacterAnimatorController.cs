using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class CharacterAnimatorController : MonoBehaviour
{
    // private variables
    private Animator animator;
    private static readonly int AnimationsSpeed = Animator.StringToHash("AnimationsSpeed");
    private static readonly int IsMining = Animator.StringToHash("IsMining");
    private static readonly int Chill = Animator.StringToHash("Chill");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private float _multiplier;
    private bool valueIsIncreased = false;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void SetValues(Vector3 nextPosition)
    {
        animator.SetFloat(Horizontal,
                          Mathf.Round(nextPosition.x - gameObject.transform.position.x));
        animator.SetFloat(Vertical, Mathf.Round(nextPosition.y - gameObject.transform.position.y));
    }

    public void InvertChillVar() =>
        animator.SetBool(Chill, !animator.GetBool(Chill));

    public void InvertIsMiningVar() =>
        animator.SetBool(IsMining, !animator.GetBool(IsMining));

    public void EndMining()
    {
        if (animator.GetBool(IsMining))
            animator.SetBool(IsMining, false);
    }

    // it`s set samples for all mining animation, so it`s like mine rate
    public void SetAnimationsSpeed(float speed) =>
        animator.SetFloat(AnimationsSpeed, speed);

    // for booster
    public void IncreaseAnimationSpeed(float multiplier)
    {
        if (valueIsIncreased)
            return;

        _multiplier = multiplier;
        animator.SetFloat(AnimationsSpeed, animator.GetFloat(AnimationsSpeed) * multiplier);
        valueIsIncreased = true;
    }

    public void DecreaseAnimationSpeed()
    {
        animator.SetFloat(AnimationsSpeed, animator.GetFloat(AnimationsSpeed) / _multiplier);
    }
}