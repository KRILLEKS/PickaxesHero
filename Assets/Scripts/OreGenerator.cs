using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Globalization;

public class OreGenerator : MonoBehaviour
{
  // global variables
  private OreGenerationChances oreGenerationChances; // 24 ores in total
  private GridBehavior gridBehaviour;
  private GameObject player;

  // local variables
  private Dictionary<GameObject, float> oresSpawnNumbers = new Dictionary<GameObject, float>(); // orePrefab, spawnChance
  private List<GameObject> ores = new List<GameObject>();
  private float rand;
  public List<float> randNumbers = new List<float>(); // it needs for loading system
  public int indexer = 0; // Tells what level needs to be generated

  private void Awake()
  {
    gridBehaviour = FindObjectOfType<GridBehavior>();
    player = GameObject.FindGameObjectWithTag("Player");

  }

  public void GenerateNextLevelOres()
  {
    indexer++;

    SetUp();

    Debug.Log("Level : " + indexer);

    GenerateOres(true);
  }

  public void LoadOres(OresData oresData)
  {
    indexer = oresData.indexer;
    randNumbers = oresData.randNumbers;

    SetUp();

    Debug.Log("Level : " + indexer);

    GenerateOres(false);
  }



  #region Set Up

  private void SetUp()
  {
    oreGenerationChances = Resources.Load<OreGenerationChances>("OreGenerationChances/" + indexer.ToString());
    oresSpawnNumbers.Clear();

    DestroyOres();
    GenerateOresNumbers();
  }

  private void DestroyOres()
  {
    if (ores != null)
      foreach (var ore in ores)
        Destroy(ore);

    ores.Clear();
  }

  // it`s set spawn number for ores.
  // If we have 50% to spawn stone, 30% to spawn Tin and 20% to spawn Copper we get next numbers. Stone = 50, Tin = 80, Copper = 100;
  private void GenerateOresNumbers()
  {
    float inc = 0; // it needs for AddOreMethod
    int oreNumber = 1; // it needs for AddOreMethod

    AddOre("Stone", oreGenerationChances.stone);
    AddOre("Tin", oreGenerationChances.tin);
    AddOre("Copper", oreGenerationChances.copper);
    AddOre("Zinc", oreGenerationChances.zinc);
    AddOre("Basalt", oreGenerationChances.basalt);
    AddOre("Iron", oreGenerationChances.iron);
    AddOre("Bismuth", oreGenerationChances.bismuth);
    AddOre("Chromite", oreGenerationChances.chromite);
    AddOre("Silver", oreGenerationChances.silver);
    AddOre("Gold", oreGenerationChances.gold);
    AddOre("Platinum", oreGenerationChances.platinum);
    AddOre("HellStone", oreGenerationChances.hellStone);
    AddOre("Titan", oreGenerationChances.titan);
    AddOre("Tungusten", oreGenerationChances.tungusten);
    AddOre("Cobald", oreGenerationChances.cobald);
    AddOre("Quartz", oreGenerationChances.quartz);
    AddOre("Cinnabar", oreGenerationChances.cinnabar);
    AddOre("Palladium", oreGenerationChances.palladium);
    AddOre("Obsidian", oreGenerationChances.obsidian);
    AddOre("Talmallit", oreGenerationChances.talmallit);
    AddOre("Diamond", oreGenerationChances.diamond);
    AddOre("Nanite", oreGenerationChances.nanite);
    AddOre("Dragonite", oreGenerationChances.dragonite);
    AddOre("Mythit", oreGenerationChances.mythit);

    void AddOre(string oreName, float oreGenerationChance)
    {
      oresSpawnNumbers.Add(Resources.Load<GameObject>($"OresPrefabs/{oreNumber++}_{oreName}")
      , oreGenerationChance + inc);
      inc += oreGenerationChance;
    }

    Debug.Log("Total chances : " + inc);
  }

  #endregion



  #region Generate Ores

  private void GenerateOres(bool newOres)
  {
    float[] values = oresSpawnNumbers.Values.ToArray();
    GameObject[] oresPrefabs = oresSpawnNumbers.Keys.ToArray();

    if (newOres) // generate new ores
    {
      randNumbers.Clear();

      for (int x = 0; x < GridBehavior.WIDTH; x++)
        for (int y = 0; y < GridBehavior.HEIGHT; y++)
        {
          randNumbers.Add(rand = Random.Range(0, 100));

          if (GenerateFreeSpace())
            continue;

          GenerateOre(x, y, rand);

          // check is there should be free space or no
          bool GenerateFreeSpace()
          {
            for (int i = -3; i < 3; i++)
              for (int j = 3; j > -3; j--)
                if (SkipTile(i, j))
                  return true;

            return false;

            bool SkipTile(int _x, int _y) =>
              (Mathf.Abs(gridBehaviour.originPosition.x) + Vector3Int.FloorToInt(player.transform.position).x - x == _x
              && Mathf.Abs(gridBehaviour.originPosition.y) + Vector3Int.FloorToInt(player.transform.position).y - y == _y);
          }
        }
    }

    else // load old ores
      for (int x = 0, i = 0; x < GridBehavior.WIDTH; x++)
        for (int y = 0; y < GridBehavior.HEIGHT; y++)
          if (!gridBehaviour.gridArray[x, y])
            GenerateOre(x, y, randNumbers[i++]);
          else
            i++;

    void GenerateOre(int x, int y, float value)
    {
      if (!gridBehaviour.IsTileExists(x, y))
        for (int j = 0; j < values.Length; j++) // generates 1 ore
          if (values[j] > value)
          {
            ores.Add(Instantiate(oresPrefabs[j], gridBehaviour.GetWorldPosition(x, y) + new Vector3(.5f, .5f), Quaternion.identity));
            ores[ores.Count - 1].transform.SetParent(gameObject.transform);
            break;
          }
    }
  }

  #endregion
}
