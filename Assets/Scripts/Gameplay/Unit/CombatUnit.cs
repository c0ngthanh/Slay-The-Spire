using System.Collections.Generic;
using UnityEngine;

public class CombatUnit : BaseUnit
{
    public CombatAttribute Attribute {get; private set;}
    private List<Effect> effects;
    public static CombatUnit Create(CombatAttribute attribute)
    {
        CombatUnit unit = Instantiate(attribute.CombatUnitObject);
        unit.Attribute = attribute.MakeCopy();
        unit.effects = new List<Effect>();
        return unit;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
    
    private void ApplyEffects()
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
