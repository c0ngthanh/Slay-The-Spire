using System;
using System.Collections.Generic;
using UnityEngine;

public class CardCombat : MonoBehaviour
{
    [SerializeField] private Card cardPrefab;

    private SPool<Card> hpBarPool;


    private CardModel cardModel => GameModel.Instance.GetModel<CardModel>();
    public void Init()
    {
        EventBusSystem.Subscribe<AddCardToHandEvent>(OnAddCardToHand);
        hpBarPool = new SPool<Card>(cardPrefab, 3, transform);

        hpBarPool.ReturnAllToPool(); // Clear existing cards before adding new ones
        foreach (var cardSO in cardModel.CurrentPlayerCard)
        {
            Card newCard = hpBarPool.Get();
            newCard.Init(cardSO);
        }
    }
    
    private void OnAddCardToHand(AddCardToHandEvent @event)
    {
        hpBarPool.ReturnAllToPool(); // Clear existing cards before adding new ones
        foreach (var cardSO in @event.CardSO)
        {
            Card newCard = hpBarPool.Get();
            newCard.Init(cardSO);
        }
    }
}
