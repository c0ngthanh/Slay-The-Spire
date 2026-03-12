using System;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : SystemBase
{
    private CardModel cardModel => GameModel.Instance.GetModel<CardModel>();

    private int currentCardNumber = 0;
    public override void Initialize()
    {
        EventBusSystem.Subscribe<CardSelectedEvent>(SelectCard);
        EventBusSystem.Subscribe<TargetSelectedEvent>(OnTargetSelected);
        EventBusSystem.Subscribe<TurnStartEvent>(OnTurnStart);
    }

    private void OnTurnStart(TurnStartEvent @event)
    {
        DrawCardFromDeck(5);
    }

    private void OnTargetSelected(TargetSelectedEvent @event)
    {
        if(cardModel.CurrentCardSO != null && cardModel.CurrentCardSO.NeedToTarget)
        {
            CardExecutionContext context = new CardExecutionContext();
            context.AddTarget(@event.Unit);
            PlayCard(context);
        }
    }

    // public void AddCardToHand(CardSO cardSO, int number = 1)
    // {
    //     for(int i = 0; i < number; i++)
    //     {
    //         cardModel.CurrentPlayerCard.Add(cardSO);
    //         EventBusSystem.Publish(new AddCardToHandEvent(cardSO));
    //     }
    // }

    public void DrawCardFromDeck(int number = 5)
    {
        Debug.Log("CardSystem: Drawing " + number + " cards from deck." + " Current deck size: " + cardModel.PlayerDeck.Count);
        cardModel.CurrentPlayerCard.Clear();
        for(int i = 0; i < number; i++)
        {
            if(cardModel.PlayerDeck.Count > 0)
            {
                CardSO drawnCard = cardModel.PlayerDeck[0];
                cardModel.PlayerDeck.RemoveAt(0);
                cardModel.CurrentPlayerCard.Add(drawnCard);
            }
            else
            {
                Debug.LogWarning("CardSystem: Deck is empty, cannot draw more cards.");
                break;
            }
        }
        EventBusSystem.Publish(new AddCardToHandEvent(cardModel.CurrentPlayerCard));
    }

    public void SelectCard(CardSelectedEvent @event)
    {
        cardModel.SetCurrentCard(@event.CardSO);
        ActiveCard();
    }

    public void ActiveCard()
    {
        if(cardModel.CurrentCardSO != null)
        {
            if(cardModel.CurrentCardSO.NeedToTarget)
            {
                // Waiting for target selection
                // Currently dont need targeting System -> maybe add for later
                EventBusSystem.Publish(new StartTargetingEvent(cardModel.CurrentCardSO.TargetLayerMask));
            }
            else
            {
                var allUnits = GameManager.Instance.GetSystem<CombatSystem>().GetAllUnits();
                CardExecutionContext context = new CardExecutionContext();
                switch(cardModel.CurrentCardSO.CardTarget)
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
                        Debug.LogError("CardSystem: Unsupported card target type " + cardModel.CurrentCardSO.CardTarget.ToString());
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
        foreach (var behavior in cardModel.CurrentCardSO.CardBehavior)
        {
            behavior.Execute(context);
        }
        EventBusSystem.Publish(new CardPlayedEvent(cardModel.CurrentCardSO));
        cardModel.PlayerGraveyard.Add(cardModel.CurrentCardSO);
        cardModel.SetCurrentCard(null);
    }

    public void InitDeck(List<CardSO> cardSOList)
    {
        cardModel.PlayerDeck.Clear();
        currentCardNumber = 40;
        for(int i = 0; i < currentCardNumber; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, cardSOList.Count);
            cardModel.PlayerDeck.Add(cardSOList[randomIndex]);
        }
    }
}
