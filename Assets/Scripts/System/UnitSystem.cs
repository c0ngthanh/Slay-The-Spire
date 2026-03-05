using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitSystem : SystemBase
{
    public List<BaseUnit> CurrentUnits { get; private set; } = new List<BaseUnit>();

    private int currentUID = 0;

    public CombatUnit CreateCombatUnits(CombatAttribute attributes)
    {
        CombatUnit unit = CombatUnit.Create(attributes);
        unit.UID = currentUID++;
        CurrentUnits.Add(unit);
        return unit;
    }
    
    private void RemoveUnit(CombatUnit unit)
    {
        // Destroy or return to pool // TODO: Implement object pooling for better performance
        CurrentUnits.Remove(unit);
        GameObject.Destroy(unit.gameObject);
        // Additional cleanup if needed
    }
}
