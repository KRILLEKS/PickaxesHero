using System.Linq;
using UnityEngine;

public class Constants : MonoBehaviour
{
  // TODO: is constants import when I use a field in it

  public enum Items
  {
    PickaxeHead,
    Stick,
    Hat
  }

  public const int oresAmount = 23;

  public static readonly string[] oresNames =
  {
    "Stone",
    "Tin",
    "Copper",
    "Zinc",
    "Basalt",
    "Iron",
    "Bismuth",
    "Chromite",
    "Gold",
    "Platinum",
    "Hellstone",
    "Titan",
    "Tungsten",
    "Cobalt",
    "Quartz",
    "Cinnabar",
    "Palladium",
    "Obsidian",
    "Talmallit",
    "Diamond",
    "Nanite",
    "Dragonit",
    "Mythit"
  };

  public static readonly int[] fillerIndexes = { 0, 4, 10, 18 };

  public static Ore GetOre(int index)
  {
    return new Ore
    {

      name = Constants.oresNames[index],
      
      index = index,
      
      shardTexture = Resources.LoadAll<Texture>("Shards")[index],
      
      buyPrice = Mathf.Pow(index + 1, 3) * 5,
      sellPrice = Mathf.Pow(index + 1, 3)
    };
  }

  public static Ore[] GetOres()
  {
    var ores = new Ore[Constants.oresAmount];
    
    for (int i = 0; i < Constants.oresAmount; i++)
    {
      ores[i] = GetOre(i);
    }

    return ores;
  }
}
