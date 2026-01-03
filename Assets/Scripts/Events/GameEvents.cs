using System.Collections.Generic;

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
    public string Description; // e.g. "Attack", "Skill"
}

public class CardDrawEvent : GameEvent
{
    public int Amount;
}

public class DeckShuffledEvent : GameEvent { }

// --- Special Events (examples) ---
public class ObtainCurseEvent : GameEvent { }

public class PotionUsedEvent : GameEvent { }
