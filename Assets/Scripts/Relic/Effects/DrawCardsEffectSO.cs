using UnityEngine;

[CreateAssetMenu(menuName = "Relic/Effects/DrawCards")]
public class DrawCardsEffectSO : RelicEffectSO
{
    public int Amount;

    public override void Execute(GameEvent gameEvent, RelicInstance relicInstance)
    {
        Debug.Log($"[Relic Effect] {relicInstance.Data.relicName} drew {Amount} cards.");
        // CombatSystem.Instance.DrawCards(Amount);
    }
}
