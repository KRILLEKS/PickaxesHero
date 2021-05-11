using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShopMenuData
{
    public int pickaxeHeadIndex;
    public int stickIndex;
    public int hatIndex;
    
    public ShopMenuData(ShopMenuController shopMenuController)
    {
        pickaxeHeadIndex = shopMenuController.pickaxeHeadIndex;
        stickIndex = shopMenuController.stickIndex;
        hatIndex = shopMenuController.hatIndex;
    }
}
