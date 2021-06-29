using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SwipeMenu : SwipeMenuCore
{
    // global variables
    private CsGlobal csGlobal;

    private void Awake()
    {
        csGlobal = FindObjectOfType<CsGlobal>();
    }

    private void Start()
    {
        positions = new float[transform.childCount];
        distance = 1f / (positions.Length - 1);

        for (int i = 0; i < positions.Length; i++)
            positions[i] = distance * i; // sets distance value for every object
    }

    void Update()
    {
        if (!csGlobal.isClicking())
            ScrollToNearest();

        if (changeElementsSize)
            ChangeElementsSize();
    }
}