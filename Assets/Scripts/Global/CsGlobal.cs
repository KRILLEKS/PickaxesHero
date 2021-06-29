using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CsGlobal : MonoBehaviour
{
    // TODO: add music
    [HideInInspector] public Vector3 g_mousePosition = new Vector3();

    [Space] public UnityEvent onTouch; // Invokes on touch

    // global variables
    private Camera cam;

    private void Awake()
    {
        if (onTouch == null)
            new UnityEvent();

        cam = FindObjectOfType<Camera>();
    }

    void Update()
    {
        if (IsPointerOverUIObject())
        {
            return;
        }

        SetMousePosition();

        if (Input.GetButtonUp("Fire1"))
            onTouch.Invoke();

        bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position =
                new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
    }

    private void SetMousePosition()
    {
        g_mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    public bool isClicking() =>
        Input.GetButton("Fire1");
}