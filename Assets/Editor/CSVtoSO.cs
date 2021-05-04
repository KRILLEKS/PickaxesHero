using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;

// TODO: remove this crutch / store this logic in database
// TODO: clear folders before fill them with data
public class CSVtoSO : MonoBehaviour
{
    // local variables
    private static string savePath;

    // key - save path, value - csv path
    private static Dictionary<string, string> paths =
        new Dictionary<string, string>
        {
            {"Assets/Resources/Upgrades/hat/", "/Editor/CSVs/hatCSV.csv"},
            {"Assets/Resources/Upgrades/stick/", "/Editor/CSVs/stickCSV.csv"},
            {
                "Assets/Resources/Upgrades/pickaxeHead/",
                "/Editor/CSVs/pickaxeHeadCSV.csv"
            }
        };

    [MenuItem("Utilities/Generate upgrades")]
    public static void GenerateUpgrades()
    {
        foreach (var path in paths)
        {
            GenerateUpgrade(path.Value, path.Key);
        }

        void GenerateUpgrade(string path,
            string saveFolder)
        {
            string[] allLines = File.ReadAllLines(Application.dataPath + path);
            int iterator = 0;

            foreach (var line in allLines)
            {
                // we`re get of rid empty lines and lines where we don`t have values
                string[] splitLine = line.Split(new char[] {';', '/'}).Where
                    (line => line != "").ToArray();
                if (splitLine.Length == 0)
                    continue;

                Upgrade upgrade = ScriptableObject.CreateInstance<Upgrade>();

                upgrade.value = float.Parse(splitLine[0]);
                for (int i = 1; i < splitLine.Length; i = i + 2)
                {
                    upgrade.cost[int.Parse(splitLine[i + 1])] =
                        int.Parse(splitLine[i]);
                }

                // we need to add ` in SO name, because we want to load it in right order
                savePath = iterator < 10
                    ? saveFolder + "'" + iterator++ + ".asset"
                    : saveFolder + iterator++ + ".asset";

                AssetDatabase.CreateAsset(upgrade, savePath);
            }

            AssetDatabase.SaveAssets();
        }
    }
}