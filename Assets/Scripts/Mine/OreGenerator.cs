using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;

public class OreGenerator : MonoBehaviour
{
    // global variables
    private GridBehavior _gridBehaviour;
    private GameObject player;
    public ChancesGenerator chancesGenerator;

    // local variables
    private float[] values;
    private readonly List<GameObject> oresPrefabs = new List<GameObject>();
    private readonly List<GameObject> ores = new List<GameObject>();
    private float rand;
    public List<float> randNumbers = new List<float>(); // it needs for loading system
    public int currentLevel = 0; // Tells what level needs to be generated

    private void Awake()
    {
        _gridBehaviour = FindObjectOfType<GridBehavior>();
        player = GameObject.FindGameObjectWithTag("Player");
        chancesGenerator = GameObject.FindObjectOfType<ChancesGenerator>();

        LoadOresPrefabs();
    }

    public void LoadOres(OresData oresData)
    {
        currentLevel = oresData.indexer;
        randNumbers = oresData.randNumbers;

        DestroyOres();

        Debug.Log("Level : " + currentLevel);

        GenerateOres(false);
    }

    private void LoadOresPrefabs()
    {
        int oreNumber = 1; // it needs for AddOreMethod

        AddOre("Stone");
        AddOre("Tin");
        AddOre("Copper");
        AddOre("Zinc");
        AddOre("Basalt");
        AddOre("Iron");
        AddOre("Bismuth");
        AddOre("Chromite");
        AddOre("Gold");
        AddOre("Platinum");
        AddOre("HellStone");
        AddOre("Titan");
        AddOre("Tungsten");
        AddOre("Cobalt");
        AddOre("Quartz");
        AddOre("Cinnabar");
        AddOre("Palladium");
        AddOre("Obsidian");
        AddOre("Talmallit");
        AddOre("Diamond");
        AddOre("Nanite");
        AddOre("Dragonite");
        AddOre("Mythit");

        void AddOre(string oreName)
        {
            oresPrefabs.Add(Resources.Load<GameObject>($"OresPrefabs/{oreNumber++}_{oreName}"));
        }
    }

    private void DestroyOres()
    {
        if (ores != null)
            foreach (var ore in ores)
                Destroy(ore);

        ores.Clear();
    }

    public void GenerateNextLevelOres()
    {
        currentLevel++;

        DestroyOres();

        Debug.Log("Level : " + currentLevel);

        GenerateOres(true);
    }

    [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
    private void GenerateOres(bool newOres)
    {
        values = chancesGenerator.GetChance();

        Debug.Log("Total Chances = " + values.Sum());

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
                            (Mathf.Abs(GridBehavior.originPosition.x) +
                             Vector3Int.FloorToInt(player.transform.position).x -
                             x ==
                             _x &&
                             Mathf.Abs(GridBehavior.originPosition.y) +
                             Vector3Int.FloorToInt(player.transform.position).y -
                             y ==
                             _y);
                    }
                }
        }

        else // load old ores
            for (int x = 0, i = 0; x < GridBehavior.WIDTH; x++)
                for (int y = 0; y < GridBehavior.HEIGHT; y++)
                    if (!_gridBehaviour.gridArray[x, y])
                        GenerateOre(x, y, randNumbers[i++]);
                    else
                        i++;

        void GenerateOre(int x, int y, float value)
        {
            float increaseValue = 0;
            if (!_gridBehaviour.IsTileExists(x, y))
                for (int j = 0; j < values.Length; j++) // generates 1 ore
                {
                    if (values[j] + increaseValue > value)
                    {
                        ores.Add(Instantiate(oresPrefabs[j],
                            _gridBehaviour.GetWorldPosition(x, y) + new Vector3(.5f, .5f),
                            Quaternion.identity));
                        ores[ores.Count - 1].transform.SetParent(gameObject.transform);
                        break;
                    }

                    increaseValue += values[j];
                }
        }
    }
}