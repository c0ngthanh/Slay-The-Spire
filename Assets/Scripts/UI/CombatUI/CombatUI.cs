using System;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : BaseUI
{
    [SerializeField] private Button endTurnBtn;
    [SerializeField] private Button deckBtn;
    [SerializeField] private Text infoText;

    private void Awake()
    {
        endTurnBtn.onClick.AddListener(OnEndTurnClicked);
        deckBtn.onClick.AddListener(OnOpenDeck);
        EventBusSystem.Subscribe<CombatInfoEvent>(OnCombatInfo);
    }

    private void OnOpenDeck()
    {
        UISystem uiSystem = GameSystem.Instance.GetSystem<UISystem>();
        uiSystem.ShowUI<CardDeck>();
    }

    private void OnCombatInfo(CombatInfoEvent evt)
    {
        
        infoText.text = $"Phase: {evt.CurrentPhase}\n";
        foreach (var item in evt.PlayerTeam)
        {
            infoText.text += $"Player: {item.Attribute.CombatUnitObject.name} HP: {item.Attribute.HP}/{item.Attribute.MaxHP}\n";
        }
        foreach (var item in evt.EnemyTeam)
        {
            infoText.text += $"Enemy: {item.Attribute.CombatUnitObject.name} HP: {item.Attribute.HP}/{item.Attribute.MaxHP}\n";
        }
    }

    private void OnEndTurnClicked()
    {
        EventBusSystem.Publish(new EndTurnEvent());
    }
}
