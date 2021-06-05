using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BonusOresMenuTest
{
    [Test]
    public void wasMenuOpenedBefore_returnsFalse()
    {
        // ARRANGE

        // ACT

        // ASSERT
    }

    [Test]
    public void test_next_level_if_u_need_to_load_it()
    {
        // ARRANGE
        var gameObject = new GameObject();
        var descentToTheNextLevel = gameObject.AddComponent<NextLevelLoadController>();
        var oreGenerator = gameObject.AddComponent<OreGenerator>();

        descentToTheNextLevel.generateLevel = gameObject.AddComponent<GenerateLevel>();
        descentToTheNextLevel.generateLevel.player = new GameObject();
        descentToTheNextLevel.oreGenerator = new OreGenerator();
        descentToTheNextLevel._gridBehavior = gameObject.AddComponent<GridBehavior>();
        descentToTheNextLevel.progressBar = gameObject.AddComponent<ProgressBar>();
        descentToTheNextLevel.nextLevelMenu = new GameObject();

        int previousLevel = oreGenerator.currentLevel;
        
        // ACT
        descentToTheNextLevel.LoadNextLevel();
        int currentLevel = oreGenerator.currentLevel;
        
        // ASSERT
        Assert.AreEqual(previousLevel + 1, currentLevel);
    }
}