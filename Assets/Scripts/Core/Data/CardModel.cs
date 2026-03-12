using System;
using System.Collections.Generic;

[Serializable]
public class CardModel : ModelBase
{
    public CardSO CurrentCardSO {get; private set;} = null;
    public List<CardSO> CurrentPlayerCard {get; private set;} = new List<CardSO>();
    public List<CardSO> PlayerDeck {get; private set;} = new List<CardSO>();
    public List<CardSO> PlayerGraveyard {get; private set;} = new List<CardSO>();

    public CardModel()
    {
        CurrentCardSO = null;
        CurrentPlayerCard = new List<CardSO>();
        PlayerDeck = new List<CardSO>();
        PlayerGraveyard = new List<CardSO>();
    }
    public void SetCurrentCard(CardSO cardSO)
    {
        CurrentCardSO = cardSO;
    }
}
