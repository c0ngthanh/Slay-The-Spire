using System;
using UnityEngine;

[Serializable]
public class AttackBehavior : Behavior
{
    private enum AttackType
    {
        FixedValue,
        CasterBlock
    }
    [SerializeField] private AttackType attackType;
    [SerializeField] private int value;
    public override void Execute(CardExecutionContext context)
    {
        int damage = attackType switch
        {
            AttackType.FixedValue => value,
            AttackType.CasterBlock => context.Caster.Attribute.Block * value,
            _ => 0
        };
        string targetNames = context.Targets.Count == 0 ? "no target" : context.Targets[0].name;
        
        Debug.Log($"Deal {damage} to {targetNames}");
        foreach (var target in context.Targets)
        {
            target.ModifyHP(-damage);
        }
    }
}
