using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CsGlobal : MonoBehaviour
{
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
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        SetMousePosition();

        if (Input.GetButtonDown("Fire1"))
            onTouch.Invoke();
    }

    private void SetMousePosition()
    {
        g_mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    public bool isClicking() =>
        Input.GetButton("Fire1");
}