using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonusOresMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void SetTextEqualToCurrentLevel()
    {
        text.text = "Level " + FindObjectOfType<OreGenerator>().currentLevel;
    }
}
