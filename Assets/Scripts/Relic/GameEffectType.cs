
public enum GameEffectType
{
    // Player Stats
    Heal,
    IncreaseMaxHP,
    GainBlock,
    GainEnergy,
    GainGold,
    GainPotionSlots,

    // Cards / Hand / Draw
    AddCardToHand,
    DrawCards,
    UpgradeRandomCards,

    // Combat Powers / Status
    ApplyDebuff, // Vulnerable, Weak, Poison
    GainBuff, // Strength, Dexterity, Focus, Mantra, Thorns
    ChannelOrb,

    // Damage Modifiers
    DealBonusDamage,
    DoubleDamage,

    // World / Misc
    ModifyEnemyHP,
    ModifyShopCost,

    // Custom
    Custom
}
