// Maps Inspector selections to actual Game Events
public enum GameTriggerType
{
    OnCombatEnd,
    OnCombatStart,
    OnTurnStart,
    OnFirstAttack,
    OnLoseHP,
    OnRestSiteEntered,
    OnUnknownRoomEntered, // ? rooms
    OnAttackPlayed,
    OnSkillPlayed,
    OnPowerPlayed,
    OnCardPlayed, // Any card
    OnObtainCurse,
    OnTurnNumber, // e.g. every X turns
    OnXAttacksPlayed,
    // Add more as needed based on Events
}
