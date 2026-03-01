using System.Collections.Generic;
using UnityEngine;

public class EventType
{

}
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