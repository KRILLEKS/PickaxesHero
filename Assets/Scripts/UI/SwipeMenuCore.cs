using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SwipeMenuCore : MonoBehaviour
{
    [SerializeField] public ScrollerType scrollerType = ScrollerType.horizontal;
    [SerializeField] public float transitionLerpTime = 0.01f;
    [SerializeField] public Scrollbar scrollbar;
    [Space] [SerializeField] public bool changeElementsSize = true;
    [SerializeField] public float increaseSizeLerpTime = 0.01f;
    [SerializeField] public float decreaseSizeLerpTime = 0.01f;
    [SerializeField] public Vector2 CentralElementSize = new Vector2(10f, 10f);
    [SerializeField] public Vector2 OtherElementsSize = new Vector2(0.8f, 0.8f);

    // local variables
    [HideInInspector] public float[] positions;
    [HideInInspector] public float distance; // distance between objects
    [HideInInspector] public float _scrollBarValue;

    public enum ScrollerType
    {
        horizontal,
        vertical
    }

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
    
    public void ChangeElementsSize()
    {
        if (scrollerType == ScrollerType.horizontal)
        {
            _scrollBarValue = scrollbar.value;
        }
        else
        {
            _scrollBarValue = 1f - scrollbar.value;
        }
        
        for (int i = 0; i < positions.Length; i++)
            if (positions[i] < _scrollBarValue + (distance / 2) &&
                positions[i] > _scrollBarValue - (distance / 2))
            {
                IncreaseCentralElementSize(i);
            }
            else // if u over slide on right or on left without if elements size will decrease same with if second condition
                DecreaseOtherElementsSize(i);
            
        void DecreaseOtherElementsSize(int index)
        {
            for (int i = 0; i < positions.Length; i++)
                if (index != i)
                    transform.GetChild(i).localScale = Vector2.Lerp(
                        transform.GetChild(i).localScale,
                        OtherElementsSize,
                        decreaseSizeLerpTime * Time.fixedDeltaTime);
        }

        void IncreaseCentralElementSize(int index) =>
            transform.GetChild(index).localScale = Vector2.Lerp(
                transform.GetChild(index).localScale,
                CentralElementSize,
                increaseSizeLerpTime * Time.fixedDeltaTime);
    }
}