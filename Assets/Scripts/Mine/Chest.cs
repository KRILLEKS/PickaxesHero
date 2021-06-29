using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Get chest");
        Destroy(gameObject);
        ChestSpawnController.onChestPick.Invoke();
    }
}
