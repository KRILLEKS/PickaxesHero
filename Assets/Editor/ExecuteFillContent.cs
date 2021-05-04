using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// it executes all fillContent methods
public class ExecuteFillContent : MonoBehaviour
{
    [MenuItem("Utilities/Fill content")]
    public static void Execute()
    {
        FillContent[] fillContentMethods = FindObjectsOfType<FillContent>();

        foreach (var method in fillContentMethods)
        {
            method.Execute();
        }
    }
}