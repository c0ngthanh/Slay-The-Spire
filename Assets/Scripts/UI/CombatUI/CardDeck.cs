using UnityEngine;
using UnityEngine.UI;

public class CardDeck : BaseUI
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
        cardPool.ReturnAllToPool(); // Clear existing cards before adding new ones
        GameSystem.Instance.GetSystem<CardSystem>().GetCurrentPlayerCard().ForEach(cardSO =>
        {
            Card card = cardPool.Get();
            card.Init(cardSO);
            card.gameObject.SetActive(true);
        });
    }

    protected override void OnHide()
    {
    }

    protected override void OnClose()
    {
    }
}
