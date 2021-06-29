using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    public void TEST_SCRIPT()
    {
        InstantiatePopUpTextController.InstantiateText(transform.position, "Hello",10);
    }
}
