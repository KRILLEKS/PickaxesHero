using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ExtractedOresCounter : MonoBehaviour
{
    public int[] ores = new int[24]; // all ores

    public void GetOre(string oreName)
    {
        Regex regex = new Regex(@"^\d*");
        oreName = regex.Match(oreName).Value; // gets ore number

        int index = int.Parse(oreName) - 1; // gets index

        ores[index]++;
    }

    public void LoadOres(ExtractedOresData extractedOresData) =>
        ores = extractedOresData.ores;
}
