using System.Collections.Generic;
using UnityEngine;

// Base class for all events
public abstract class GameEvent { }

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

// --- World/Map Events ---
public class EnterRoomEvent : GameEvent
{
    // public RoomType Type;
}

public class RestSiteEnteredEvent : GameEvent { }

public class ShopEnteredEvent : GameEvent { }

// --- Card Events ---
public class CardPlayedEvent : GameEvent
{
    // public CardData Card;
    public int EnergyCost;
    public CardType Type;
}

public class CardDrawEvent : GameEvent
{
    public int Amount;
}

public class DeckShuffledEvent : GameEvent { }

// --- Special Events (examples) ---
public class ObtainCurseEvent : GameEvent { }

public class PotionUsedEvent : GameEvent { }


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

public class AddCardToHandEvent : GameEvent
{
    public CardSO CardSO;
    public AddCardToHandEvent(CardSO cardSO)
    {
        this.CardSO = cardSO;
    }
}

public class CardSelectedEvent : GameEvent
{
    public CardSO CardSO;
    public Card CurrentCard;
    public CardSelectedEvent(CardSO cardSO, Card currentCard)
    {
        this.CardSO = cardSO;
        this.CurrentCard = currentCard;
    }
}

public class StartTargetingEvent : GameEvent// Currently just need to choose CombatUnit as target
{
    public LayerMask LayerMask;
    public StartTargetingEvent(LayerMask layerMask)
    {
        this.LayerMask = layerMask;
    }
}

public class TargetSelectedEvent : GameEvent// Currently just need to choose CombatUnit as target
{
    public CombatUnit Unit;
    public TargetSelectedEvent(CombatUnit unit)
    {
        this.Unit = unit;
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
