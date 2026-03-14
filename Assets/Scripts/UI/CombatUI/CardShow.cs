using System.Collections.Generic;
using ObservableCollections;
using R3;
using UnityEngine;
using UnityEngine.UI;

public class CardShow : BaseUI
{
    [SerializeField] private Card cardPrefab;
    [SerializeField] private Transform content;
    [SerializeField] private Button closeBtn;
    private SPool<Card> cardPool;

    

    protected override void OnInialize()
    {
        closeBtn.onClick.AddListener(() => { Hide(); });
        cardPool = new SPool<Card>(cardPrefab, 10, content);
    }

    protected override void OnShow()
    {
        // Now wait for ShowCards to be called to populate the list.
        cardPool.ReturnAllToPool(); 
    }

    public void ShowCards(ObservableList<CardSO> cards)
    {
        cardPool.ReturnAllToPool(); // Clear existing cards before adding new ones
        foreach (var cardSO in cards)
        {
            Card card = cardPool.Get();
            card.Init(cardSO);
            card.gameObject.SetActive(true);
        }
    }

    protected override void OnHide()
    {
    }

    protected override void OnClose()
    {
    }
}
