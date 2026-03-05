// Maps Inspector selections to actual Game Events
public enum GameTriggerType
{
    // Combat Phase
    OnCombatEnd,
    OnCombatStart,
    OnTurnStart,
    OnTurnEnd,
    OnFirstAttack, // First attack of combat
    OnLoseHP,
    OnDamageDealt, // When player deals unblocked damage
    OnEnemyDeath,

    // Card Events
    OnCardPlayed,
    OnAttackPlayed,
    OnSkillPlayed,
    OnPowerPlayed,
    OnCardDraw,
    OnCardDiscard,
    OnCardExhaust,
    OnShuffleDeck,

    // Exploration / Map
    OnRestSiteEntered,
    OnRest, // The action of resting
    OnSmith, // The action of smithing
    OnShopEntered,
    OnTreasureRoomEntered,
    OnUnknownRoomEntered, // ? rooms
    OnClimbFloor,

    // Meta / Inventory
    OnObtainCurse,
    OnObtainPotion,
    OnObtainGold,
    OnObtainCard,

    // Special
    OnTurnNumber,
    OnXAttacksPlayed,
    OnXCardsPlayed,
    OnHpThreshold, // Health drops below X%
    // Add more as needed based on Events
}
