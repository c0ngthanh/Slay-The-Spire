using UnityEngine;

public class EffectSystem: SystemBase
{
    public void AddEffect(CombatUnit unit,Effect effect)
    {
        unit.AddEffect(effect);
    }
}
