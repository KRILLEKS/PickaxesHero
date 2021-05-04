﻿using System.Linq;
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
    var oreCostDatabase = FindObjectOfType<Data>().oreCostsDatabase;
    
    return new Ore
    {

      name = Constants.oresNames[index],
      shardTexture = Resources.LoadAll<Texture>("Shards")[index],
      buyPrice = oreCostDatabase[index].buyCost,
      sellPrice = oreCostDatabase[index].sellCost
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
