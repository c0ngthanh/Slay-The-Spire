using System;
using System.Collections.Generic;
using UnityEngine;

public class CardExecutionContext
{
    public CombatUnit caster;
    public List<CombatUnit> targets;
    public CardExecutionContext(CombatUnit caster)
    {
        this.caster = caster;
        this.targets = new List<CombatUnit>();
    }
    public CardExecutionContext(CombatUnit caster, List<CombatUnit> targets)
    {
        this.caster = caster;
        this.targets = targets;
    }
    public CardExecutionContext()
    {
        this.caster = null;
        this.targets = new List<CombatUnit>();
    }
    public void AddTarget(CombatUnit target)
    {
        if (targets == null)
        {
            targets = new List<CombatUnit>();
        }
        targets.Add(target);
    }
}
[Serializable]
public abstract class Behavior
{
    public abstract void Execute(CardExecutionContext context);
}
