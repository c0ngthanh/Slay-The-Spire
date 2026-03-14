using UnityEngine;
using ObservableCollections;
using System.Collections.Specialized;


public class CardCombat : MonoBehaviour, ISubView
{
    [SerializeField] private Card cardPrefab;

    private SPool<Card> hpBarPool;


    private CardModel cardModel => GameModel.Instance.GetModel<CardModel>();
    public void Init()
    {
        hpBarPool = new SPool<Card>(cardPrefab, 3, transform);
        cardModel.CurrentPlayerCard.CollectionChanged += OnAddCardToHand;
        ShowCurrentCard(); // NEED TO REMOVE WHEN REMOVE TEST CODE
    }

    public void Tick()
    {
        
    }

    private void OnAddCardToHand(in NotifyCollectionChangedEventArgs<CardSO> @event)
    {
        switch(@event.Action)
        {
            case NotifyCollectionChangedAction.Add:
                Card card = hpBarPool.Get();
                card.Init(@event.NewItem);
                break;
            case NotifyCollectionChangedAction.Remove:
                // Handle card removal if necessary
                break;
            case NotifyCollectionChangedAction.Reset:
                hpBarPool.ReturnAllToPool();
                break;
        }
    }

    private void ShowCurrentCard()
    {
        foreach (var cardSO in cardModel.CurrentPlayerCard)
        {
            Card newCard = hpBarPool.Get();
            newCard.Init(cardSO);
        }
    }
}
