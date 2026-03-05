using UnityEngine;

[CreateAssetMenu(menuName = "Relic/Effects/Heal")]
public class HealEffectSO : RelicEffectSO
{
    public override void Execute(GameEvent gameEvent, RelicRuntime relicInstance, RelicEffectData data, IGameContext ctx)
    {
        // In a real implementation, we would access the Player object.
        // For now, checks if we have a valid target or just logs it.
        if (data == null)
        {
            Debug.LogError("[Relic Effect] No data provided for HealEffectSO.");
            return;
        }
        if (data.IntValue <= 0)
        {
            Debug.LogError("[Relic Effect] Invalid data provided for HealEffectSO.");
            return;
        }
        if (ctx == null)
        {
            Debug.LogError("[Relic Effect] No context provided for HealEffectSO.");
            return;
        }
        if (relicInstance == null)
        {
            Debug.LogError("[Relic Effect] No relic instance provided for HealEffectSO.");
            return;
        }
        Debug.Log($"[Relic Effect] {relicInstance.Data.relicName} healed player for {data.IntValue} HP.");
        ctx.HealPlayer(data.IntValue);
    }
}
