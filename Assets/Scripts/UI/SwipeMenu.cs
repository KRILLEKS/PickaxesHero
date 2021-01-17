using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{
  [SerializeField]
  private bool changeElementsSize = true;
  [SerializeField]
  private float transitionLerpTime = 0.01f;
  [SerializeField]
  private float increaseSizeLerpTime = 0.01f;
  [SerializeField]
  private float decreaseSizeLerpTime = 0.01f;
  [SerializeField]
  private Vector2 CentralElementSize = new Vector2(10f, 10f);
  [SerializeField]
  private Vector2 OtherElementsSize = new Vector2(0.8f, 0.8f);

  [Space]
  [SerializeField]
  private GameObject scrollbar;

  // global variables
  private CsGlobal csGlobal;

  // local variables
  private float scrollPosition = 0;
  private float[] pos;
  private float distance; // distance between objects

  private void Awake()
  {
    csGlobal = FindObjectOfType<CsGlobal>();
  }

  private void Start()
  {
    pos = new float[transform.childCount];
    distance = 1f / (pos.Length - 1);

    for (int i = 0; i < pos.Length; i++)
      pos[i] = distance * i; // sets distance value for every object
  }

  void Update()
  {
    scrollPosition = scrollbar.GetComponent<Scrollbar>().value;

    if (!csGlobal.isClicking())
      ScrollToNearest();

    if (changeElementsSize)
      ChangeElementsSize();

    void ScrollToNearest()
    {
      for (int i = 0; i < pos.Length; i++)
        if (scrollPosition < pos[i] + (distance / 2) && scrollPosition > pos[i] - (distance / 2))
          scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], transitionLerpTime * Time.fixedDeltaTime);
    }
    void ChangeElementsSize()
    {
      for (int i = 0; i < pos.Length; i++)
        if (pos[i] < scrollPosition + (distance / 2) && pos[i] > scrollPosition - (distance / 2))
          IncreaseCentralElementSize(i);
        else // if u over slide on right or on left without if elements size will decrease same with if second condition
          DecreaseOtherElementsSize(i);


      void DecreaseOtherElementsSize(int index)
      {
        for (int i = 0; i < pos.Length; i++)
          if (index != i)
            transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, OtherElementsSize, decreaseSizeLerpTime * Time.fixedDeltaTime);
      }
      void IncreaseCentralElementSize(int index) =>
      transform.GetChild(index).localScale = Vector2.Lerp(transform.GetChild(index).localScale, CentralElementSize, increaseSizeLerpTime * Time.fixedDeltaTime);
    }
  }
}
