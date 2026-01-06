using System;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    [SerializeField] private Card cardPrefab;

    void Awake()
    {
        EventBusSystem.Subscribe<AddCardToHandEvent>(OnAddCardToHand);
    }

    private void OnAddCardToHand(AddCardToHandEvent @event)
    {
        Card newCard = Instantiate(cardPrefab, transform);
        newCard.Init(@event.cardSO);
    }
}
