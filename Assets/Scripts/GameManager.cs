using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // static variables
    private static GameManager _gameManager;

    // determines in which scene player left the game
    private static int sceneIndex = 1;
    public static int previousSceneIndex = 0;

    private void Awake()
    {
        if (_gameManager != null)
            Destroy(_gameManager);
        else
        {
            _gameManager = this;
            DontDestroyOnLoad(_gameManager);
        }
    }

    public static void LoadIndex(GameManagerData data)
    {
        sceneIndex = data.index;
    }

    // it`ll load where player left
    public void LoadBasedOnLeftIndex()
    {
        if (sceneIndex == 2)
            LoadMine();
        else
            LoadCity();
    }

    public void LoadCity()
    {
        previousSceneIndex = GetSceneIndex();
        SceneManager.LoadScene("City");
    }

    public void LoadMine()
    {
        previousSceneIndex = GetSceneIndex();
        SceneManager.LoadScene("Mine");
    }

    public static int GetSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}