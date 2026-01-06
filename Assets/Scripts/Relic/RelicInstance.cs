using System;
using System.Collections.Generic;
using UnityEngine;

public class RelicInstance
{
    public RelicSO Data { get; private set; }
    public int Counter { get; set; }
    public bool IsActive { get; set; } = true;

    public RelicInstance(RelicSO data)
    {
        Data = data;
    }
}
