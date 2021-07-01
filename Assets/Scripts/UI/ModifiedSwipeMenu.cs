using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifiedSwipeMenu : SwipeMenuCore
{
    [SerializeField] private int visibleSegments = 6;
    // means how many segments we can see without scrolling somewhere

    // global variables
    private CsGlobal csGlobal;

    private void Awake()
    {
        csGlobal = FindObjectOfType<CsGlobal>();
    }

    void Start()
    {
        // -visibleSegments, because we simultaneously have this number
        // like if we have only 6 segments, we can`t scroll anywhere
        positions = new float [transform.childCount - visibleSegments];
        distance = 1f / (positions.Length);

        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = distance * i;
        }
    }

    void Update()
    {
        if (!csGlobal.isClicking())
            ScrollToNearest();
    }
}