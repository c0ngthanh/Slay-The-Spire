using System;
using UnityEngine;

[Serializable]
public class TargetingSystemData : SystemData
{
    public bool IsTargeting = false;
    public LayerMask targetLayerMask;
}
