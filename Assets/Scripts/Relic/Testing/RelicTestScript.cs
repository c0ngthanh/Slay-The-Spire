using UnityEngine;
using System.Collections.Generic;

public class RelicTestScript : MonoBehaviour
{
    private RelicManager relicManager;

    private void Start()
    {
        // Setup Manager
        relicManager = gameObject.AddComponent<RelicManager>();

        // Create "Burning Blood" Relic at Runtime for testing
        RelicSO burningBlood = ScriptableObject.CreateInstance<RelicSO>();
        burningBlood.relicName = "Burning Blood";
        burningBlood.abilities = new List<RelicAbilitySO>();

        // Create Heal Effect
        HealEffectSO healEffect = ScriptableObject.CreateInstance<HealEffectSO>();
        healEffect.Amount = 6;

        // Create Ability (Trigger: OnCombatEnd)
        RelicAbilitySO ability = ScriptableObject.CreateInstance<RelicAbilitySO>();
        ability.Trigger = GameTriggerType.OnCombatEnd;
        ability.Effects = new List<RelicEffectSO> { healEffect };
        
        burningBlood.abilities.Add(ability);

        // Add to Manager
        relicManager.AddRelic(burningBlood);

        // Test: Fire Combat End Event
        Debug.Log(" Test: Firing CombatEndEvent...");
        GlobalEventManager.Instance.Invoke(new CombatEndEvent { PlayerWon = true });
        
        // Test: Fire a different event (should NOT trigger)
        Debug.Log(" Test: Firing TurnStartEvent (Should NOT trigger Burning Blood)...");
        GlobalEventManager.Instance.Invoke(new TurnStartEvent { TurnNumber = 1 });
    }
}
