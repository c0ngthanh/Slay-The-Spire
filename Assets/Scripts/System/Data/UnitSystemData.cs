using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnitSystemData : SystemData
{
    public List<BaseUnit> CurrentUnits = new List<BaseUnit>();
    public int currentUID = 0;
}
