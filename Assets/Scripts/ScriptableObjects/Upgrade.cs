using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class Upgrade : ScriptableObject
{
  public float value;
  public int[] cost = new int[Constants.oresAmount];
}
