using System;
using System.Collections.Generic;

[Serializable]
public class CombatModel : ModelBase
{
    public Dictionary<CombatPhase, List<CombatUnit>> PhaseUnits = new Dictionary<CombatPhase, List<CombatUnit>>();
    public int CurrentTurn = 0;
    public CombatUnit CurrentUnit = null;
    public CombatPhase CurrentPhase = CombatPhase.PlayerTurn;
    public Dictionary<CombatPhase, List<CombatUnit>> GetAllUnits()
    {
        return PhaseUnits;
    }

}
