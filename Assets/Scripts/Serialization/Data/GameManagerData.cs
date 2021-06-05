using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameManagerData
{
    public int index;

    public GameManagerData()
    {
        index = GameManager.GetSceneIndex();
    }
}
