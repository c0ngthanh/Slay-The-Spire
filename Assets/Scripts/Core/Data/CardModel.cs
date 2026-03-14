using ObservableCollections;

public class CardModel : ModelBase
{
    public CardSO CurrentCardSO {get; private set;} = null;
    public ObservableList<CardSO> CurrentPlayerCard {get; private set;} = new ();
    public ObservableList<CardSO> PlayerDeck {get; private set;} = new();
    public ObservableList<CardSO> PlayerGraveyard {get; private set;} = new();


    public CardModel()
    {
        CurrentCardSO = null;
        CurrentPlayerCard = new();
        PlayerDeck = new();
        PlayerGraveyard = new();
    }
    public void SetCurrentCard(CardSO cardSO)
    {
        CurrentCardSO = cardSO;
    }
    public void AddCardToHand(CardSO cardSO)
    {
        CurrentPlayerCard.Add(cardSO);
    }
    public void ClearHand()
    {
        CurrentPlayerCard.Clear();
    }
}
