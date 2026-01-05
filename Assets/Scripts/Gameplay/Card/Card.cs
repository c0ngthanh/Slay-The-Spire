using System;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private CardSO cardData;
    [SerializeField] private Text descriptionText;

    private Toggle toggle;
    private bool isTargeting;
    public void Init(CardSO cardData)
    {
        this.cardData = cardData;
        descriptionText.text = cardData.CardDescription;
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnCardSelected);
    }

    private void OnCardSelected(bool isOn)
    {
        if (isOn)
        {
            GetComponent<Image>().color = new Color(154/255f, 154/255f, 154/255f);
            // Implement target selection logic here
            if(cardData.NeedToTarget)
            {
                
            }
            else
            {
                PlayCard();
            }
        }
        else
        {
            // Deselect targets logic here
            GetComponent<Image>().color = Color.white;
            if(cardData.NeedToTarget)
            {
                
            }
        }
    }
    void Update()
    {
        if(isTargeting)
        {
            // Handle target selection input here
        }
    }

    public void PlayCard()
    {
        foreach (var behavior in cardData.CardBehavior)
        {
            behavior.Execute(new CardExecutionContext());
        }
        toggle.isOn = false;
    }
}
