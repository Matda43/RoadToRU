using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class Part
/// </summary>
[Serializable]
public class Part
{
    // Width of the part
    public int width;

    // gameObjects table
    public GameObject[] plateforms;

    // Part constructor
    public Part()
    {
        if (width < 0)
        {
            width = 0;
        }
    }
}
