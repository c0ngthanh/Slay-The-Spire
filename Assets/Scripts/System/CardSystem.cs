using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class CardSystem : SystemBase
{
    List<CardSO> currentPlayerCard = new List<CardSO>();
    public void AddCardToHand(CardSO cardSO, int number = 1)
    {
        for(int i = 0; i < number; i++)
        {
            currentPlayerCard.Add(cardSO);
            EventBusSystem.Publish(new AddCardToHandEvent(cardSO));
        }
    }
}
