using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private CardSO cardData;
    [SerializeField] private Text descriptionText;
    private void Start()
    {
        descriptionText.text = cardData.CardDescription;
    }
    public void PlayCard()
    {
        foreach (var behavior in cardData.CardBehavior)
        {
            behavior.Execute(new CardExecutionContext());
        }
    }
}
