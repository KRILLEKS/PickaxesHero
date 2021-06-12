using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InfinityScroller : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private int visibleElements;
    [Space] [SerializeField] private float startScrollingSpeed;
    [SerializeField] private float scrollingTime = 10f;
    [SerializeField] private float scrollStopSpeed = 0.002f;

    // local variables
    private Scrollbar _scrollbar;
    private readonly List<Transform> contentList = new List<Transform>();
    private Transform objectToMove;
    private int elementsAmount;
    private float scrollingSpeed;

    private void Awake()
    {
        _scrollbar = GetComponentInChildren<Scrollbar>();

        foreach (Transform child in content.transform)
            contentList.Add(child);

        elementsAmount = contentList.Count;
        scrollingSpeed = startScrollingSpeed;
    }

    private void Update()
    {
        if (scrollingSpeed > 0)
        {
            _scrollbar.value += scrollingSpeed; // scrolling
            if (scrollingSpeed > scrollStopSpeed)
                scrollingSpeed -= startScrollingSpeed / scrollingTime * Time.deltaTime;
            else if (_scrollbar.value < 0.001 && _scrollbar.value > -0.001)
            {
                scrollingSpeed = 0;
                _scrollbar.value = 0;
            }

            Debug.Log(1f / contentList.Count + " " + (1f / (contentList.Count + 1f)));
        }
        else
        {
            Debug.Log("Give reward");
        }

        MoveElements();

        void MoveElements()
        {
            if (_scrollbar.value > 1f / (contentList.Count - visibleElements))
            {
                contentList[0].SetSiblingIndex(elementsAmount - 1);
                _scrollbar.value -= 1f / (elementsAmount - visibleElements);

                objectToMove = contentList[0];
                contentList.RemoveAt(0);
                contentList.Add(objectToMove);
            }
        }
    }
}