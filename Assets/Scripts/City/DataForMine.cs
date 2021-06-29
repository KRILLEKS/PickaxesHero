using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataForMine : MonoBehaviour
{
    // public static variable
    public static int whichLevel2Load;
    
    // private static variables
    private static DataForMine dataForMine;
    
    private void Awake()
    {
        if (dataForMine != null)
            dataForMine = this;
        
        DontDestroyOnLoad(gameObject);
    }
}
