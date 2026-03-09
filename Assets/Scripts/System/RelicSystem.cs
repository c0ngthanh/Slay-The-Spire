using UnityEngine;
using System.Collections.Generic;

public class RelicSystem : SystemBase
{
    public List<RelicRuntime> ownedRelics = new();

    // Context
    public IGameContext Context { get; private set; }

    public void Init(IGameContext ctx)
    {
        Context = ctx;
    }

    public override void Initialize()
    {
        base.Initialize();
        if (Context == null) Init(new GameContext());
        EventBusSystem.Subscribe<CombatStartEvent>(OnCombatStart);
        EventBusSystem.Subscribe<CombatEndEvent>(OnCombatEnd);
        EventBusSystem.Subscribe<TurnStartEvent>(OnTurnStart);
        EventBusSystem.Subscribe<TurnEndEvent>(OnTurnEnd);
        EventBusSystem.Subscribe<PlayerLoseHPEvent>(OnLoseHP);

        // Map/Room Events
        EventBusSystem.Subscribe<RestSiteEnteredEvent>(OnRestSiteEntered);
        EventBusSystem.Subscribe<ShopEnteredEvent>(OnShopEntered);

        // Card Events
        EventBusSystem.Subscribe<CardPlayedEvent>(OnCardPlayed);
        EventBusSystem.Subscribe<CardDrawEvent>(OnCardDraw);
        EventBusSystem.Subscribe<DeckShuffledEvent>(OnDeckShuffled);

        // Meta
        EventBusSystem.Subscribe<ObtainCurseEvent>(OnObtainCurse);
    }

    public override void Dispose()
    {
        base.Dispose();
        EventBusSystem.Unsubscribe<CombatStartEvent>(OnCombatStart);
        EventBusSystem.Unsubscribe<CombatEndEvent>(OnCombatEnd);
        EventBusSystem.Unsubscribe<TurnStartEvent>(OnTurnStart);
        EventBusSystem.Unsubscribe<TurnEndEvent>(OnTurnEnd);
        EventBusSystem.Unsubscribe<PlayerLoseHPEvent>(OnLoseHP);

        EventBusSystem.Unsubscribe<RestSiteEnteredEvent>(OnRestSiteEntered);
        EventBusSystem.Unsubscribe<ShopEnteredEvent>(OnShopEntered);

        EventBusSystem.Unsubscribe<CardPlayedEvent>(OnCardPlayed);
        EventBusSystem.Unsubscribe<CardDrawEvent>(OnCardDraw);
        EventBusSystem.Unsubscribe<DeckShuffledEvent>(OnDeckShuffled);

        EventBusSystem.Unsubscribe<ObtainCurseEvent>(OnObtainCurse);
    }

    // --- Handlers ---

    private void OnCombatStart(CombatStartEvent e) => Dispatch(GameTriggerType.OnCombatStart, e);
    private void OnCombatEnd(CombatEndEvent e) => Dispatch(GameTriggerType.OnCombatEnd, e);
    private void OnTurnStart(TurnStartEvent e)
    {
        Dispatch(GameTriggerType.OnTurnStart, e);
        Dispatch(GameTriggerType.OnTurnNumber, e);
    }
    private void OnTurnEnd(TurnEndEvent e) => Dispatch(GameTriggerType.OnTurnEnd, e);
    private void OnLoseHP(PlayerLoseHPEvent e) => Dispatch(GameTriggerType.OnLoseHP, e);

    private void OnRestSiteEntered(RestSiteEnteredEvent e) => Dispatch(GameTriggerType.OnRestSiteEntered, e);
    private void OnShopEntered(ShopEnteredEvent e) => Dispatch(GameTriggerType.OnShopEntered, e);

    private void OnCardPlayed(CardPlayedEvent e)
    {
        Dispatch(GameTriggerType.OnCardPlayed, e);

        // Dispatch specific card type triggers
        if (e.Type == CardType.Attack) Dispatch(GameTriggerType.OnAttackPlayed, e);
        else if (e.Type == CardType.Skill) Dispatch(GameTriggerType.OnSkillPlayed, e);
        else if (e.Type == CardType.Power) Dispatch(GameTriggerType.OnPowerPlayed, e);
    }

    private void OnCardDraw(CardDrawEvent e) => Dispatch(GameTriggerType.OnCardDraw, e);
    private void OnDeckShuffled(DeckShuffledEvent e) => Dispatch(GameTriggerType.OnShuffleDeck, e);
    private void OnObtainCurse(ObtainCurseEvent e) => Dispatch(GameTriggerType.OnObtainCurse, e);

    private void Dispatch(GameTriggerType trigger, GameEvent e)
    {
        // 1. Collect all valid (Relic, Ability) pairs
        var queue = new List<(RelicRuntime relic, RelicAbilityData ability)>();

        foreach (var relic in ownedRelics)
        {
            if (!relic.IsActive) continue;
            if (relic.Data?.abilities == null) continue;

            foreach (var ability in relic.Data.abilities)
            {
                if (ability.Trigger == trigger)
                {
                    queue.Add((relic, ability));
                }
            }
        }

        // 2. Sort by Priority (Lower = Earlier)
        if (queue.Count > 1)
        {
            queue.Sort((x, y) => x.ability.Priority.CompareTo(y.ability.Priority));
        }

        // 3. Execute in order
        foreach (var item in queue)
        {
            var relic = item.relic;
            var ability = item.ability;

            // Conditions
            bool conditionsMet = true;
            if (ability.Conditions != null)
            {
                foreach (var conditionData in ability.Conditions)
                {
                    if (conditionData.Condition != null &&
                        !conditionData.Condition.Check(e, relic, conditionData))
                    {
                        conditionsMet = false;
                        break;
                    }
                }
            }
            if (!conditionsMet) continue;

            // Effects
            if (ability.Effects != null)
            {
                foreach (var effectData in ability.Effects)
                {
                    effectData.Effect?.Execute(e, relic, effectData, Context);
                }
            }
        }
    }

    public void AddRelic(RelicSO relicData)
    {
        if (relicData == null) return;

        RelicRuntime newInstance = new(relicData);
        ownedRelics.Add(newInstance);

        Debug.Log($"Relic Added: {relicData.relicName}");

        // Handle "Upon pickup" effects immediately if any (optional, depends on design)
        // If we had an OnObtain relic trigger, we'd fire an event here.
    }

    public void RemoveRelic(RelicRuntime relic)
    {
        if (ownedRelics.Contains(relic))
        {
            ownedRelics.Remove(relic);
        }
    }

    /// <summary>
    /// Calculates the final value of a stat by applying all relic modifiers.
    /// </summary>
    /// <param name="stat">The stat to calculate.</param>
    /// <param name="baseValue">The base value to start with (default 0 for add/subtract, 1 for multipliers).</param>
    /// <returns>The modified value.</returns>
    public float GetStatValue(StatType stat, float baseValue)
    {
        float finalValue = baseValue;

        // Apply "Add" modifiers first
        foreach (var relic in ownedRelics)
        {
            if (!relic.IsActive || relic.Data.passives == null) continue;

            foreach (var passive in relic.Data.passives)
            {
                if (passive.Stat == stat && passive.Type == ModifierType.Add)
                {
                    finalValue += passive.ModifierValue;
                }
            }
        }

        // Apply "Multiply" modifiers
        foreach (var relic in ownedRelics)
        {
            if (!relic.IsActive || relic.Data.passives == null) continue;

            foreach (var passive in relic.Data.passives)
            {
                if (passive.Stat == stat && passive.Type == ModifierType.Multiply)
                {
                    finalValue *= passive.ModifierValue;
                }
            }
        }

        // Apply "Override" modifiers (last one wins)
        foreach (var relic in ownedRelics)
        {
            if (!relic.IsActive || relic.Data.passives == null) continue;

            foreach (var passive in relic.Data.passives)
            {
                if (passive.Stat == stat && passive.Type == ModifierType.Override)
                {
                    finalValue = passive.ModifierValue;
                }
            }
        }

        return finalValue;
    }
}
