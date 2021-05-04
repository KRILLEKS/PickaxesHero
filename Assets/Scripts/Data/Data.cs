using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public OreCost[] oreCostsDatabase;

    [Serializable]
    public struct OreCost
    {
        public string oreName;
        public int index;
        public float buyCost;
        public float sellCost;
    }
}