using UnityEngine;

// Define an interface for effects
public abstract class Effect
{
    private int _effectID;
    private int _effectValue;
    private EffectType _effectType;
    public virtual void ApplyEffect(CombatUnit unit)
    {
        
    }
}
// Effect Stack type
public enum EffectType
{
    No,
    Intensity,
    Duration,
    Counter,
    Special
}