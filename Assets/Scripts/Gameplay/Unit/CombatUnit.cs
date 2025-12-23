using System.Collections.Generic;
using UnityEngine;

public class CombatUnit : BaseUnit
{
    public CombatAttribute Attribute {get; private set;}
    private GameObject model;
    private List<Effect> effects;
    public CombatUnit(CombatAttribute attribute)
    {
        Attribute = attribute.MakeCopy();
        effects = new List<Effect>();
        model = Instantiate(attribute.tempCharacterModel);
    }

    public void SetPosition(Vector3 position)
    {
        if(model != null)
        {
            model.transform.position = position;
        }
    }
    // private void Awake()
    // {
    //     Initalize();
    // }   
    // public void Initalize()
    // {
    //     Attribute = Attribute.CopyAttribute(BaseAttribute);
    //     effects = new List<Effect>();
    // }
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
