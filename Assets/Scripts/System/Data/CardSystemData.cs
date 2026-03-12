using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class CardSystemData : SystemData
{
    public CardSO currentCardSO = null;
    public Card currentSelectedCard = null;
    public List<CardSO> currentPlayerCard = new List<CardSO>();
}
