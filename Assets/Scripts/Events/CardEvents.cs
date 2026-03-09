// --- Card Events ---
using System.Collections.Generic;

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
    public Card CurrentCard;
    public CardSelectedEvent(CardSO cardSO, Card currentCard)
    {
        this.CardSO = cardSO;
        this.CurrentCard = currentCard;
    }
}
