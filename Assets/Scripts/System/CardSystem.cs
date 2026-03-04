using System;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : SystemBase
{
    private CardSO currentCardSO = null;
    private Card currentSelectedCard = null;
    public override void Initialize()
    {
        EventBusSystem.Subscribe<CardSelectedEvent>(SelectCard);
        EventBusSystem.Subscribe<TargetSelectedEvent>(OnTargetSelected);
    }

    private void OnTargetSelected(TargetSelectedEvent @event)
    {
        if(currentCardSO != null && currentCardSO.NeedToTarget)
        {
            CardExecutionContext context = new CardExecutionContext();
            context.AddTarget(@event.Unit);
            PlayCard(context);
        }
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
        currentCardSO = @event.CardSO;
        currentSelectedCard = @event.CurrentCard;
        ActiveCard();
    }

    public void ActiveCard()
    {
        if(currentSelectedCard != null)
        {
            if(currentCardSO.NeedToTarget)
            {
                // Waiting for target selection
                // Currently dont need targeting System -> maybe add for later
                EventBusSystem.Publish(new StartTargetingEvent(currentCardSO.TargetLayerMask));
            }
            else
            {
                var allUnits = GameSystem.Instance.GetSystem<CombatSystem>().GetAllUnits();
                CardExecutionContext context = new CardExecutionContext();
                switch(currentCardSO.CardTarget)
                {
                    case CardTarget.All:
                        context.AddTarget(allUnits[CombatPhase.PlayerTurn]);
                        context.AddTarget(allUnits[CombatPhase.EnemyTurn]);
                        break;
                    case CardTarget.AllEnemy:
                        context.AddTarget(allUnits[CombatPhase.EnemyTurn]);
                        break;
                    case CardTarget.AllTeamMate:
                        context.AddTarget(allUnits[CombatPhase.PlayerTurn]);
                        break;
                    default:
                        Debug.LogError("CardSystem: Unsupported card target type " + currentCardSO.CardTarget.ToString());
                        break;
                }

                PlayCard(context);
            }
        }
    }

    public void PlayCard(CardExecutionContext context = null)
    {
        if(context == null)
        {
            context = new CardExecutionContext();
        }
        foreach (var behavior in currentCardSO.CardBehavior)
        {
            behavior.Execute(context);
        }
        currentSelectedCard.MoveToGraveyard();
        currentCardSO = null;
        currentSelectedCard = null;
    }
}
