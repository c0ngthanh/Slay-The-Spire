using System;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    [SerializeField] private Card cardPrefab;

    private SPool<Card> hpBarPool;
    private List<Card> cardList = new List<Card>();

    void Awake()
    {
        EventBusSystem.Subscribe<AddCardToHandEvent>(OnAddCardToHand);
        hpBarPool = new SPool<Card>(cardPrefab, 3, transform);
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
