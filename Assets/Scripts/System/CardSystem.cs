using System;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : SystemBase
{
    private CardSO currentCardSO = null;
    private Card currentSelectedCard = null;
    [SerializeField] private List<CardSO> currentPlayerCard = new List<CardSO>();
    [SerializeField] private List<CardSO> playerDeck = new List<CardSO>();

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
        if(currentCardSO != null && currentCardSO.NeedToTarget)
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
    //         currentPlayerCard.Add(cardSO);
    //         EventBusSystem.Publish(new AddCardToHandEvent(cardSO));
    //     }
    // }

    public void DrawCardFromDeck(int number = 5)
    {
        Debug.Log("CardSystem: Drawing " + number + " cards from deck." + " Current deck size: " + playerDeck.Count);
        currentPlayerCard.Clear();
        for(int i = 0; i < number; i++)
        {
            if(playerDeck.Count > 0)
            {
                CardSO drawnCard = playerDeck[0];
                playerDeck.RemoveAt(0);
                currentPlayerCard.Add(drawnCard);
            }
            else
            {
                Debug.LogWarning("CardSystem: Deck is empty, cannot draw more cards.");
                break;
            }
        }
        EventBusSystem.Publish(new AddCardToHandEvent(currentPlayerCard));
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

    internal void InitDeck(List<CardSO> cardSOList)
    {
        playerDeck.Clear();
        currentCardNumber = 40;
        for(int i = 0; i < currentCardNumber; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, cardSOList.Count);
            playerDeck.Add(cardSOList[randomIndex]);
        }
    }
}
