using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CsGlobal : MonoBehaviour
{
  [HideInInspector]
  public Vector3 g_mousePosition;

  [Space]
  public UnityEvent onTouch; // Invokes on touch

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

    g_mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

    if (Input.GetButtonDown("Fire1"))
      onTouch.Invoke();
  }

  public bool isClicking() =>
           Input.GetButton("Fire1");
}
