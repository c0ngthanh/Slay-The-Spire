using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitSystem : SystemBase
{
    private UnitModel unitModel => GameModel.Instance.GetModel<UnitModel>();

    private int currentUID = 0;

    public CombatUnit CreateCombatUnits(CombatAttribute attributes)
    {
        CombatUnit unit = CombatUnit.Create(attributes);
        unit.UID = currentUID++;
        unitModel.CurrentUnits.Add(unit);
        return unit;
    }
    
    private void RemoveUnit(CombatUnit unit)
    {
        // Destroy or return to pool // TODO: Implement object pooling for better performance
        unitModel.CurrentUnits.Remove(unit);
        GameObject.Destroy(unit.gameObject);
        // Additional cleanup if needed
    }
}
