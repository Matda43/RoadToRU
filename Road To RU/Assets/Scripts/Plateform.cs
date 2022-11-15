using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Plateform
{
    public GameObject prefab;
    public int minOccurence;
    GameObject[] elements;

    public Plateform()
    {
        if(minOccurence < 0)
        {
            minOccurence = 0;
        }
    }
}
