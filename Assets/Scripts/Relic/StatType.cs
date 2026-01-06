public enum StatType
{
    // Economy
    ShopPrice, // Multiplier (e.g. 0.5 for 50% off)
    CardRemovalCost,
    GoldGain,
    
    // Combat
    DamageDealt,
    DamageTaken,
    BlockGained,
    HealingEffectiveness,
    StartingEnergy,
    CardDrawPerTurn,
    
    // Limits
    MaxHP,
    PotionSlots,
    
    // Rewards
    CardRewardCount,
    RareCardChance,
    
    // Enemies
    EliteMaxHP, // Multiplier
    EnemyDamageDealt,
    
    // Misc
    CampfireHealAmount,
    CampfireUpgradeCount
}
