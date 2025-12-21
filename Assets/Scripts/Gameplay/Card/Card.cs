using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private CardSO cardData;

    public void PlayCard()
    {
        foreach (var behavior in cardData.CardBehavior)
        {
            behavior.Execute(new CardExecutionContext());
        }
    }
}
