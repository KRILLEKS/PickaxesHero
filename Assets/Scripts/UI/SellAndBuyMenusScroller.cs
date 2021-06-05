using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellAndBuyMenusScroller : MonoBehaviour
{
    [SerializeField] private Scrollbar _scrollbar;
    [SerializeField] private float lerpDuration = 1f;
    [SerializeField] private int iterationsPerSecond = 50;

    // local variables
    private float timeElapsed = 0f;
    private IEnumerator coroutine;

    public void ScrollToTheEnd()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = Scroll(0, 1);
        StartCoroutine(coroutine);
    }

    public void ScrollToTheStart()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = Scroll(1, 0);
        StartCoroutine(coroutine);
    }

    private IEnumerator Scroll(int startPoint,
        int endPoint)
    {
        while (timeElapsed <= lerpDuration)
        {
            _scrollbar.value = Mathf.Lerp(startPoint,
                endPoint,
                timeElapsed / lerpDuration);

            timeElapsed += 1f / iterationsPerSecond;

            yield return 1 / iterationsPerSecond;
        }

        timeElapsed = 0f;
    }
}