using System;
using System.Collections.Generic;
using UnityEngine;

public class CardExecutionContext
{
    public CombatUnit Caster;
    public List<CombatUnit> Targets;
    public CardExecutionContext(CombatUnit caster)
    {
        this.Caster = caster;
        this.Targets = new List<CombatUnit>();
    }
    public CardExecutionContext(CombatUnit caster, List<CombatUnit> targets)
    {
        this.Caster = caster;
        this.Targets = targets;
    }
    public CardExecutionContext()
    {
        this.Caster = null;
        this.Targets = new List<CombatUnit>();
    }
    public void AddTarget(CombatUnit target)
    {
        if (Targets == null)
        {
            Targets = new List<CombatUnit>();
        }
        Targets.Add(target);
    }
}
[Serializable]
public abstract class Behavior
{
    public abstract void Execute(CardExecutionContext context);
}
