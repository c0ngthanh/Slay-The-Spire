using System.Collections.Generic;

// --- Combat Events ---
public class CombatStartEvent : GameEvent
{
    // Potentially add info about enemies, etc.
}

public class CombatEndEvent : GameEvent
{
    public bool PlayerWon;
}

public class TurnStartEvent : GameEvent
{
    public int TurnNumber;
}

public class TurnEndEvent : GameEvent { }

public class PlayerAttackEvent : GameEvent
{
    // public CardSource SourceCard;
    public int DamageDealt;
    public bool IsUnblocked;
}

public class PlayerLoseHPEvent : GameEvent
{
    public int Amount;
    public object Source;
}

public class EndTurnEvent : GameEvent
{

}

public class CombatInfoEvent : GameEvent
{
    public string CurrentPhase;
    public List<CombatUnit> PlayerTeam;
    public List<CombatUnit> EnemyTeam;
    public CombatInfoEvent(string phase, List<CombatUnit> playerTeam, List<CombatUnit> enemyTeam)
    {
        this.CurrentPhase = phase;
        this.PlayerTeam = playerTeam;
        this.EnemyTeam = enemyTeam;
    }
}

public class HPChangedEvent : GameEvent// Currently just need to choose CombatUnit as target
{
    public int UnitID;
    public int CurrentHP;
    public int MaxHP;
    public HPChangedEvent(int unitID, int currentHP, int maxHP)
    {
        this.UnitID = unitID;
        this.CurrentHP = currentHP;
        this.MaxHP = maxHP;
    }
}
