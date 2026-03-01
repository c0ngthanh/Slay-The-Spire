using System.Collections.Generic;
using UnityEngine;


public struct EndTurnEvent
{

}
public struct CombatInfoEvent
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

public struct AddCardToHandEvent
{
    public CardSO CardSO;
    public AddCardToHandEvent(CardSO cardSO)
    {
        this.CardSO = cardSO;
    }
}

public struct CardSelectedEvent
{
    public CardSO CardSO;
    public Card CurrentCard;
    public CardSelectedEvent(CardSO cardSO, Card currentCard)
    {
        this.CardSO = cardSO;
        this.CurrentCard = currentCard;
    }
}

public struct StartTargetingEvent // Currently just need to choose CombatUnit as target
{
    public LayerMask LayerMask;
    public StartTargetingEvent(LayerMask layerMask)
    {
        this.LayerMask = layerMask;
    }
}

public struct TargetSelectedEvent // Currently just need to choose CombatUnit as target
{
    public CombatUnit Unit;
    public TargetSelectedEvent(CombatUnit unit)
    {
        this.Unit = unit;
    }
}

public struct HPChangedEvent // Currently just need to choose CombatUnit as target
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
