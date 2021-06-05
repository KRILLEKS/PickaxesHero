using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SwipeMenu : MonoBehaviour
{
    [SerializeField] public float transitionLerpTime = 0.01f;
    [SerializeField] public Scrollbar scrollbar;

    // local variables
    [HideInInspector] public float[] positions;
    [HideInInspector] public float distance; // distance between objects
    
    public void ScrollToNearest()
    {
        foreach (var position in positions)
            if (scrollbar.value < position + (distance / 2) &&
                scrollbar.value > position - (distance / 2))
                scrollbar.value = Mathf.Lerp(
                    scrollbar.value,
                    position,
                    transitionLerpTime * Time.fixedDeltaTime);
    }
}