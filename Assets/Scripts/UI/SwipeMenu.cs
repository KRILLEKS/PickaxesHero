using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SwipeMenu : MonoBehaviour
{
    [SerializeField] public float transitionLerpTime = 0.01f;
    [SerializeField] public Scrollbar scrollbar;

    // local variables
    [HideInInspector] public float[] pos;
    [HideInInspector] public float distance; // distance between objects
    
    public void ScrollToNearest()
    {
        for (int i = 0; i < pos.Length; i++)
            if (scrollbar.value < pos[i] + (distance / 2) &&
                scrollbar.value > pos[i] - (distance / 2))
                scrollbar.value = Mathf.Lerp(
                    scrollbar.value,
                    pos[i],
                    transitionLerpTime * Time.fixedDeltaTime);
    }
}