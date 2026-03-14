using System;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : BaseUI
{
    [SerializeField] private Button endTurnBtn;
    [SerializeField] private Button deckBtn;
    [SerializeField] private Button graveyardBtn;
    [SerializeField] private Text infoText;
    [SerializeField] private CardCombat cardCombat;
    [SerializeField] private UnitHPHandler unitHPHandler;

    private CardModel cardModel => GameModel.Instance.GetModel<CardModel>();
    private UISystem uiSystem => GameManager.Instance.GetSystem<UISystem>();
    private CombatSystem combatSystem => GameManager.Instance.GetSystem<CombatSystem>();

    protected override void OnInialize()
    {
        endTurnBtn.onClick.AddListener(OnEndTurnClicked);
        deckBtn.onClick.AddListener(OnOpenDeck);
        graveyardBtn.onClick.AddListener(OnOpenGraveyard);
        EventBusSystem.Subscribe<CombatInfoEvent>(OnCombatInfo);
        cardCombat.Init();
        unitHPHandler.Init();
    }

    private void OnOpenGraveyard()
    {
        CardShow cardDeck = uiSystem.ShowUI<CardShow>();
        cardDeck.ShowCards(cardModel.PlayerGraveyard); 
    }

    private void OnOpenDeck()
    {
        CardShow cardDeck = uiSystem.ShowUI<CardShow>();
        cardDeck.ShowCards(cardModel.PlayerDeck);
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
        combatSystem.RequestEndTurn();
    }
}
