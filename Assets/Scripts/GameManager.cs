using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // it`ll load where player left
    public void LoadBasedOnLeftIndex()
    {
        if (SinglePlayerValues.sceneIndex == 2)
            LoadMine();
        else
            LoadCity();
    }

    public void LoadCity() =>
        SceneManager.LoadScene("City");

    public void LoadMine() =>
        SceneManager.LoadScene("Mine");
}