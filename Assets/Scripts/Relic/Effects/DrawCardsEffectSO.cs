using UnityEngine;

[CreateAssetMenu(menuName = "Relic/Effects/DrawCards")]
public class DrawCardsEffectSO : RelicEffectSO
{
    public override void Execute(GameEvent gameEvent, RelicInstance relicInstance, RelicEffectData data, IGameContext ctx)
    {
        Debug.Log($"[Relic Effect] {relicInstance.Data.relicName} drew {data.IntValue} cards.");
        ctx.DrawCards(data.IntValue);
    }
}
