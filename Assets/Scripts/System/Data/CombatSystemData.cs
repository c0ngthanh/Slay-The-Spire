using System;
using System.Collections.Generic;

[Serializable]
public class CombatSystemData : SystemData
{
    public Dictionary<CombatPhase, List<CombatUnit>> PhaseUnits = new Dictionary<CombatPhase, List<CombatUnit>>();
    public int CurrentTurn = 0;
    public CombatUnit currentUnit = null;
    public CombatPhase currentPhase = CombatPhase.PlayerTurn;
}
