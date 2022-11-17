using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Part
{
    public int width;
    public GameObject[] plateforms;

    public Part()
    {
        if (width < 0)
        {
            width = 0;
        }
    }
}
