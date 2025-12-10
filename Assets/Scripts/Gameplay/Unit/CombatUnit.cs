using System.Collections.Generic;
using UnityEngine;

public class CombatUnit : BaseUnit
{
    public CombatAttribute Attribute { get; private set; }

    List<Effect> effects = new List<Effect>();

    private void ApplyEffects(BaseUnit unit)
    {
        foreach (var effect in effects)
        {
            effect.ApplyEffect(this);
        }
    }

    public void AddEffect(Effect effect)
    {
        effects.Add(effect);
    }

}
