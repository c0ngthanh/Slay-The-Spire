using System;
using UnityEngine;
using UnityEngine.UI;

public class TempUI : MonoBehaviour
{
    [SerializeField] private Button endTurnBtn;
    [SerializeField] private Text infoText;

    private void Awake()
    {
        endTurnBtn.onClick.AddListener(OnEndTurnClicked);
        EventBusSystem.Subscribe<CombatInfoEvent>(OnCombatInfo);
    }

    private void OnCombatInfo(CombatInfoEvent evt)
    {
        
        infoText.text = $"Phase: {evt.currentPhase}\n";
        foreach (var item in evt.playerTeam)
        {
            infoText.text += $"Player: {item.Attribute.tempCharacterModel.name} HP: {item.Attribute.HP}/{item.Attribute.MaxHP}\n";
        }
        foreach (var item in evt.enemyTeam)
        {
            infoText.text += $"Enemy: {item.Attribute.tempCharacterModel.name} HP: {item.Attribute.HP}/{item.Attribute.MaxHP}\n";
        }
    }

    private void OnEndTurnClicked()
    {
        EventBusSystem.Publish(new EndTurnEvent());
    }
}
