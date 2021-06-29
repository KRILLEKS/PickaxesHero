using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


public class ChestSpawnController : MonoBehaviour
{
    [SerializeField] private float chestSpawnChanceSerialize = 1f;
    [SerializeField] private UnityEvent onChestPickSerialize;

    // static variables
    private static float chestSpawnChance;
    private static GameObject chestPrefab;
    public static UnityEvent onChestPick;

    private void Awake()
    {
        if (onChestPickSerialize == null)
            new UnityEvent();

        onChestPick = onChestPickSerialize;
        
        chestSpawnChance = chestSpawnChanceSerialize;
        chestPrefab = Resources.Load<GameObject>("Chest");
    }

    public static void Try2SpawnChest(Vector3 pos2Spawn)
    {
        if (Random.value * 100 < chestSpawnChance)
        {
            SpawnChest();
        }

        void SpawnChest()
        {
            // TODO: make that chest should be rotated to player
            Instantiate(chestPrefab, pos2Spawn, Quaternion.identity);
        }
    }
}
