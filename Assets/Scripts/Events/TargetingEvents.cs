using UnityEngine;

public class StartTargetingEvent : GameEvent// Currently just need to choose CombatUnit as target
{
    public LayerMask LayerMask;
    public StartTargetingEvent(LayerMask layerMask)
    {
        this.LayerMask = layerMask;
    }
}

public class TargetSelectedEvent : GameEvent// Currently just need to choose CombatUnit as target
{
    public CombatUnit Unit;
    public TargetSelectedEvent(CombatUnit unit)
    {
        this.Unit = unit;
    }
}
