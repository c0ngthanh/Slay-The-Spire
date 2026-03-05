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
        unit.Attribute.HP = unit.Attribute.MaxHP;
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

    public void ModifyHP(int amount)
    {
        Attribute.HP += amount;
        if (Attribute.HP > Attribute.MaxHP)
        {
            Attribute.HP = Attribute.MaxHP;
        }
        else if (Attribute.HP < 0)
        {
            Attribute.HP = 0;
            // Handle unit death if needed
        }
        EventBusSystem.Publish(new HPChangedEvent(UID, Attribute.HP, Attribute.MaxHP));
    }
}
