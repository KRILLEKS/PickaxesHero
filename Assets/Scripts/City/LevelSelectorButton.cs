using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectorButton : MonoBehaviour
{
    // public variables
    public int whichLevel2Load = -1;

    public void SetLevel()
    {
        DataForMine.whichLevel2Load = whichLevel2Load;
    }
}
