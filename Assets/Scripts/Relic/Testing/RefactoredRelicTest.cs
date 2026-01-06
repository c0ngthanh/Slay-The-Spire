using UnityEngine;
using System.Collections.Generic;

public class RefactoredRelicTest : MonoBehaviour
{
    private RelicManager relicManager;

    private void Start()
    {
        // Setup Manager
        relicManager = gameObject.AddComponent<RelicManager>();

        // Inject Mock Context
        relicManager.Init(new MockGameContext());

        // Create "Low Priority Relic" (Priority 10)
        RelicSO lowPriorityRelic = ScriptableObject.CreateInstance<RelicSO>();
        lowPriorityRelic.relicName = "Low Priority Relic";
        lowPriorityRelic.abilities = new List<RelicAbilityData>();

        RelicAbilityData lowAbility = new RelicAbilityData();
        lowAbility.Trigger = GameTriggerType.OnCombatEnd;
        lowAbility.Priority = 10;
        // Mock effect that heals 5
        HealEffectSO healEffect = ScriptableObject.CreateInstance<HealEffectSO>();
        lowAbility.Effects = new List<RelicEffectData>
        {
            new RelicEffectData { Effect = healEffect, IntValue = 5 }
        };
        lowPriorityRelic.abilities.Add(lowAbility);


        // Create "High Priority Relic" (Priority 0)
        RelicSO highPriorityRelic = ScriptableObject.CreateInstance<RelicSO>();
        highPriorityRelic.relicName = "High Priority Relic";
        highPriorityRelic.abilities = new List<RelicAbilityData>();

        RelicAbilityData highAbility = new RelicAbilityData();
        highAbility.Trigger = GameTriggerType.OnCombatEnd;
        highAbility.Priority = 0;
        // Mock effect that adds 10 Gold
        GainGoldEffectSO goldEffect = ScriptableObject.CreateInstance<GainGoldEffectSO>();
        highAbility.Effects = new List<RelicEffectData>
        {
            new RelicEffectData { Effect = goldEffect, IntValue = 10 }
        };
        highPriorityRelic.abilities.Add(highAbility);

        // Add to Manager
        relicManager.AddRelic(lowPriorityRelic);
        relicManager.AddRelic(highPriorityRelic);

        // Test: Fire Combat End Event
        Debug.Log(" Test: Firing CombatEndEvent (Priority + Context Test)...");
        Debug.Log(" Expectation: \n1) [Mock] Gained 10 Gold (High Prio)\n2) [Mock] Healed 5 HP (Low Prio)");
        GlobalEventManager.Instance.Invoke(new CombatEndEvent { PlayerWon = true });
    }

    // Mock Context
    public class MockGameContext : IGameContext
    {
        public void DrawCards(int amount) => Debug.Log($"[Mock] Draw {amount} cards");
        public void GainGold(int amount) => Debug.Log($"[Mock] Gained {amount} Gold");
        public void HealPlayer(int amount) => Debug.Log($"[Mock] Healed {amount} HP");
        public void DamageRandomEnemy(int amount) => Debug.Log($"[Mock] Damage random enemy {amount}");
        public void AddMana(int amount) => Debug.Log($"[Mock] Added {amount} Mana");
    }
}
