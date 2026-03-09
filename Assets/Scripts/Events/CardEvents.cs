// --- Card Events ---
using System.Collections.Generic;

public class CardPlayedEvent : GameEvent
{
    public CardSO CardSO;
    public CardPlayedEvent(CardSO cardSO)
    {
        this.CardSO = cardSO;
    }
}

public class CardDrawEvent : GameEvent
{
    public int Amount;
}

public class DeckShuffledEvent : GameEvent { }

public class AddCardToHandEvent : GameEvent
{
    public List<CardSO> CardSO;
    public AddCardToHandEvent(List<CardSO> cardSO)
    {
        this.CardSO = cardSO;
    }
}

public class CardSelectedEvent : GameEvent
{
    public CardSO CardSO;
    public CardSelectedEvent(CardSO cardSO)
    {
        this.CardSO = cardSO;
    }
}
