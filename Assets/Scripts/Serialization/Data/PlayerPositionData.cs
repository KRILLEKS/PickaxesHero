using System;
using UnityEngine;

[System.Serializable]
public class PlayerPositionData
{
  public float[] position = new float[3];

  public PlayerPositionData(GameObject player)
  {
    position[0] = player.transform.position.x;
    position[1] = player.transform.position.y;
    position[2] = player.transform.position.z;
  }
}
