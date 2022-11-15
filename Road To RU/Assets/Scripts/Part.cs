using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Part
{
    public int width;
    [SerializeField] public Plateform[] plateforms;

    public Part()
    {
        if (width < 0)
        {
            width = 0;
        }
    }
}
