using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class DeleteSaves : MonoBehaviour
{
    [MenuItem("Utilities/Delete Saves")]
    public static void Delete()
    {
        string[] files =
            Directory.GetFiles(Application.persistentDataPath, "*.save");

        foreach (var file in files)
        {
            File.Delete(file);
        }
    }
}
