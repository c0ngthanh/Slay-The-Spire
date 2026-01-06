using UnityEngine;

[CreateAssetMenu(menuName = "Relic/Effects/GainGold")]
public class GainGoldEffectSO : RelicEffectSO
{
    public override void Execute(GameEvent gameEvent, RelicInstance relicInstance, RelicEffectData data)
    {
        Debug.Log($"[Relic Effect] {relicInstance.Data.relicName} gave player {data.IntValue} Gold.");
        // PlayerStats.Instance.Gold += data.IntValue;
    }
}
