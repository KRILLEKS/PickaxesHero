using UnityEngine;

[CreateAssetMenu(fileName = "New Ore", menuName = "Ore")]
public class OreStats : ScriptableObject
{
  public int index;
  public float armor;
  public float durability;

  public Sprite[] sprites;
}
