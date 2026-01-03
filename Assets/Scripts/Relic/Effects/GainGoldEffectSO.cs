using UnityEngine;

[CreateAssetMenu(menuName = "Relic/Effects/GainGold")]
public class GainGoldEffectSO : RelicEffectSO
{
    public int Amount;

    public override void Execute(GameEvent gameEvent, RelicInstance relicInstance)
    {
        Debug.Log($"[Relic Effect] {relicInstance.Data.relicName} gave player {Amount} Gold.");
        // PlayerStats.Instance.Gold += Amount;
    }
}
