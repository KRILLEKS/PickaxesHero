using UnityEngine;

public class CameraController : MonoBehaviour
{
  // global variables
  private GameObject player;

  // local variables
  private Vector3 offset = new Vector3(0, 0, -5);

  private void Awake()
  {
    player = GameObject.FindGameObjectWithTag("Player");
  }

  private void Update()
  {
    transform.position = player.transform.position + offset;
  }
}
