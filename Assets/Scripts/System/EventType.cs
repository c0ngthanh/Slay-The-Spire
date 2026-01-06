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
    public string currentPhase;
    public List<CombatUnit> playerTeam;
    public List<CombatUnit> enemyTeam;
    public CombatInfoEvent(string phase, List<CombatUnit> playerTeam, List<CombatUnit> enemyTeam)
    {
        this.currentPhase = phase;
        this.playerTeam = playerTeam;
        this.enemyTeam = enemyTeam;
    }
}

public struct AddCardToHandEvent
{
    public CardSO cardSO;
    public AddCardToHandEvent(CardSO cardSO)
    {
        this.cardSO = cardSO;
    }
}