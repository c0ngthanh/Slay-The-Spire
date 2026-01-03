using UnityEngine;

[CreateAssetMenu(menuName = "Relic/Effects/Heal")]
public class HealEffectSO : RelicEffectSO
{
    public int Amount;

    public override void Execute(GameEvent gameEvent, RelicInstance relicInstance)
    {
        // In a real implementation, we would access the Player object.
        // For now, checks if we have a valid target or just logs it.
        
        Debug.Log($"[Relic Effect] {relicInstance.Data.relicName} healed player for {Amount} HP.");
        
        // Example: 
        // GameSystem.Instance.GetSystem<CombatSystem>().PlayerUnit.Heal(Amount);
    }
}
