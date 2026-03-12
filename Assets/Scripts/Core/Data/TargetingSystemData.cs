using System;
using UnityEngine;

[Serializable]
public class TargetingModel : ModelBase
{
    public bool IsTargeting { get; private set; } = false;

    public TargetingModel()
    {
        IsTargeting = false;
    }
    public void StartTargeting(bool value)
    {
        IsTargeting = value;
    }
}
