using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private CardSO cardData;
    [SerializeField] private Text descriptionText;

    private Toggle toggle;
    public void Init(CardSO cardData)
    {
        this.cardData = cardData;
        descriptionText.text = cardData.CardDescription;
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggleCard);
        toggle.group = GetComponentInParent<ToggleGroup>();
    }

    private void OnToggleCard(bool value)
    {
        if(value)
        {
            EventBusSystem.Publish(new CardSelectedEvent(cardData, this));
            GetComponent<Image>().color = Color.gray;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
        }
    }

    public bool IsNeedToTarget()
    {
        return cardData.NeedToTarget;
    }

    public void MoveToGraveyard()
    {
        toggle.isOn = false;
        toggle.interactable = false;
    }
}
