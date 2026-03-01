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
            AttackType.CasterBlock => context.caster.Attribute.Block * value,
            _ => 0
        };
        string targetNames = context.targets.Count == 0 ? "no target" : context.targets[0].name;
        Debug.Log($"Deal {damage} to {targetNames}");
    }
}
