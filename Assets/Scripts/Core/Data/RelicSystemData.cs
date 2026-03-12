using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RelicSystemData : ModelBase
{
    public List<RelicRuntime> ownedRelics = new List<RelicRuntime>();
}
