using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class CardSystem : SystemBase
{
    private CardSO currentCardSO = null;
    private Card currentSelectedCard = null;
    public override void Initialize()
    {
        EventBusSystem.Subscribe<CardSelectedEvent>(SelectCard);
    }
    List<CardSO> currentPlayerCard = new List<CardSO>();
    public void AddCardToHand(CardSO cardSO, int number = 1)
    {
        for(int i = 0; i < number; i++)
        {
            currentPlayerCard.Add(cardSO);
            EventBusSystem.Publish(new AddCardToHandEvent(cardSO));
        }
    }

    public void SelectCard(CardSelectedEvent @event)
    {
        currentCardSO = @event.cardSO;
        currentSelectedCard = @event.currentCard;
    }

    public override void Tick()
    {
        if(currentSelectedCard != null)
        {
            if(currentCardSO.NeedToTarget)
            {
                // Waiting for target selection
            }
            else
            {
                PlayCard();
                currentCardSO = null;
                currentSelectedCard = null;
            }
        }
    }

    public void PlayCard()
    {
        foreach (var behavior in currentCardSO.CardBehavior)
        {
            behavior.Execute(new CardExecutionContext());
        }
        currentSelectedCard.MoveToGraveyard();
    }
}
