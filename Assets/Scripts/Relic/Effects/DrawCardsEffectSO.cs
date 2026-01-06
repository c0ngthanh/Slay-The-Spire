using UnityEngine;

[CreateAssetMenu(menuName = "Relic/Effects/DrawCards")]
public class DrawCardsEffectSO : RelicEffectSO
{
    public override void Execute(GameEvent gameEvent, RelicInstance relicInstance, RelicEffectData data)
    {
        Debug.Log($"[Relic Effect] {relicInstance.Data.relicName} drew {data.IntValue} cards.");
        // CombatSystem.Instance.DrawCards(data.IntValue);
    }
}
