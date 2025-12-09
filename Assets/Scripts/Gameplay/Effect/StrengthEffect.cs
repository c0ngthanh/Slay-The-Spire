using UnityEngine;

public class StrengthEffect : Effect
{
    private int _effectID = 1;
    private int _effectValue;
    private EffectType _effectType = EffectType.Counter;
    public override void ApplyEffect(CombatUnit unit)
    {
        unit.Attribute.Strengh = _effectValue;
    }
}
