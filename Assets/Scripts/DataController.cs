﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
  private GameObject player;
  private GenerateLevel generateLevel;
  private OreGenerator oreGenerator;
  private GridBehavior gridBehavior;
  private DescentToTheNextLevel descentToTheNextLevel;
  private ProgressBar progressBar;
  private ExtractedOresCounter extractedOresCounter;

  private void Awake()
  {
    player = GameObject.FindGameObjectWithTag("Player");
    generateLevel = FindObjectOfType<GenerateLevel>();
    gridBehavior = FindObjectOfType<GridBehavior>();
    oreGenerator = FindObjectOfType<OreGenerator>();
    descentToTheNextLevel = FindObjectOfType<DescentToTheNextLevel>();
    progressBar = FindObjectOfType<ProgressBar>();
    extractedOresCounter = FindObjectOfType<ExtractedOresCounter>();
  }

  public void Save()
  {
    SaveSystem.Save("player", new PlayerData(player));
    SaveSystem.Save("level", new LevelPrefabData(generateLevel.rand));
    SaveSystem.Save("grid", new GridData(gridBehavior));
    SaveSystem.Save("ores", new OresData(oreGenerator));
    SaveSystem.Save("descent", new DescentData(descentToTheNextLevel));
    SaveSystem.Save("progressBar", new ProgressBarData(progressBar));
    SaveSystem.Save("extractedOres", new ExtractedOresData(extractedOresCounter.ores));
  }

  public void Load()
  {
    PlayerData playerData = LoadSystem.Load<PlayerData>("player");
    player.transform.position = new Vector3(playerData.position[0], playerData.position[1], playerData.position[2]);

    generateLevel.LoadLevelPrefab(LoadSystem.Load<LevelPrefabData>("level").rand);
    gridBehavior.LoadGrid(LoadSystem.Load<GridData>("grid"));
    descentToTheNextLevel.LoadDescent(LoadSystem.Load<DescentData>("descent"));
    oreGenerator.LoadOres(LoadSystem.Load<OresData>("ores"));
    progressBar.LoadProgressBar(LoadSystem.Load<ProgressBarData>("progressBar"));
    extractedOresCounter.LoadOres(LoadSystem.Load<ExtractedOresData>("extractedOres"));
  }
}
