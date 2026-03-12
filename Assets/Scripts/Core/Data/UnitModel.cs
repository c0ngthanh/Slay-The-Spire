using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnitModel : ModelBase
{
    public List<BaseUnit> CurrentUnits {get; private set;} = new List<BaseUnit>();

    public UnitModel()
    {
        CurrentUnits = new List<BaseUnit>();
    }
}
