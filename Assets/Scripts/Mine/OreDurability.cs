using System;
using System.Collections.Generic;
using System.Linq;
using TreeEditor;
using UnityEngine;
using Random = System.Random;

public class OreDurability : MonoBehaviour
{
    [SerializeField] private OreStats oreStats;

    // global variables
    private GridBehavior _gridBehavior;
    private ProgressBar progressBar;
    private SingleExtractedOresCounter extractedOresCounter;
    private CharacterAnimatorController animatorController;

    // local variables
    private float durability = 100f;
    private float armor = 10f;
    private int index;
    private IEnumerable<BoostersController.BoosterTypes> boosterTypes;
    private readonly Random _random = new Random();
    private float randVal;

    private void Awake()
    {
        boosterTypes = Enum.GetValues(typeof(BoostersController.BoosterTypes))
                           .Cast<BoostersController.BoosterTypes>();

        _gridBehavior = FindObjectOfType<GridBehavior>();
        progressBar = FindObjectOfType<ProgressBar>();
        extractedOresCounter = FindObjectOfType<SingleExtractedOresCounter>();
        animatorController = FindObjectOfType<CharacterAnimatorController>();

        durability = oreStats.durability;
        armor = oreStats.armor;
        index = oreStats.index;
        gameObject.GetComponent<SpriteRenderer>().sprite =
            oreStats.sprites[UnityEngine.Random.Range(0, oreStats.sprites.Length)];
    }

    // Decrease ore health
    public void TakeDamage(float damage)
    {
        durability -= BoostersController.boosters[0].activeSelf ? damage * 1.5f - armor : damage - armor;

        if (durability <= 0)
            DestroyOre(true);
    }

    public void DestroyOre(bool getResource)
    {
        if (getResource)
        {
            extractedOresCounter.GetOre(index, BoostersController.boosters[2].activeSelf ? 2 : 1);
            animatorController.InvertIsMiningVar();
            progressBar.IncreaseValue();
            ChestSpawnController.Try2SpawnChest(transform.position);
        }

        Destroy(gameObject);
        _gridBehavior.InstantiateNewGridPrefab(transform.position);

        foreach (var type in boosterTypes)
        {
            ChanceToGetBooster(type);
        }
    }

    private void ChanceToGetBooster(BoostersController.BoosterTypes booster)
    {
        randVal = (float) (_random.NextDouble() * 100);

        if (randVal < BoostersController.chanceOnBooster)
        {
            BoostersController.GetBooster(booster);
        }
    }
}