using System;
using System.Collections.Generic;
using UnityEngine;

public class RelicRuntime
{
    public RelicSO Data { get; private set; }
    public int Counter { get; set; }
    public bool IsActive { get; set; } = true;

    public RelicRuntime(RelicSO data)
    {
        Data = data;
    }
}
