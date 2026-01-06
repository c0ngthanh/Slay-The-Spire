using UnityEngine;
using System.Collections.Generic;

public class RelicManager : MonoBehaviour
{
    public List<RelicInstance> ownedRelics = new();

    // For testing/reference
    public List<RelicSO> startingRelics;

    // Context
    public IGameContext Context { get; private set; }

    private void Start()
    {
        // Inject Default Context
        Init(new GameContext());

        foreach (var relicSO in startingRelics)
        {
            AddRelic(relicSO);
        }
    }

    public void Init(IGameContext ctx)
    {
        Context = ctx;
    }

    private void OnEnable()
    {
        GlobalEventManager.Instance.AddListener<CombatStartEvent>(OnCombatStart);
        GlobalEventManager.Instance.AddListener<CombatEndEvent>(OnCombatEnd);
        GlobalEventManager.Instance.AddListener<TurnStartEvent>(OnTurnStart);
        GlobalEventManager.Instance.AddListener<TurnEndEvent>(OnTurnEnd);
        GlobalEventManager.Instance.AddListener<PlayerLoseHPEvent>(OnLoseHP);

        // Map/Room Events
        GlobalEventManager.Instance.AddListener<RestSiteEnteredEvent>(OnRestSiteEntered);
        GlobalEventManager.Instance.AddListener<ShopEnteredEvent>(OnShopEntered);

        // Card Events
        GlobalEventManager.Instance.AddListener<CardPlayedEvent>(OnCardPlayed);
        GlobalEventManager.Instance.AddListener<CardDrawEvent>(OnCardDraw);
        GlobalEventManager.Instance.AddListener<DeckShuffledEvent>(OnDeckShuffled);

        // Meta
        GlobalEventManager.Instance.AddListener<ObtainCurseEvent>(OnObtainCurse);
    }

    private void OnDisable()
    {
        GlobalEventManager.Instance.RemoveListener<CombatStartEvent>(OnCombatStart);
        GlobalEventManager.Instance.RemoveListener<CombatEndEvent>(OnCombatEnd);
        GlobalEventManager.Instance.RemoveListener<TurnStartEvent>(OnTurnStart);
        GlobalEventManager.Instance.RemoveListener<TurnEndEvent>(OnTurnEnd);
        GlobalEventManager.Instance.RemoveListener<PlayerLoseHPEvent>(OnLoseHP);

        GlobalEventManager.Instance.RemoveListener<RestSiteEnteredEvent>(OnRestSiteEntered);
        GlobalEventManager.Instance.RemoveListener<ShopEnteredEvent>(OnShopEntered);

        GlobalEventManager.Instance.RemoveListener<CardPlayedEvent>(OnCardPlayed);
        GlobalEventManager.Instance.RemoveListener<CardDrawEvent>(OnCardDraw);
        GlobalEventManager.Instance.RemoveListener<DeckShuffledEvent>(OnDeckShuffled);

        GlobalEventManager.Instance.RemoveListener<ObtainCurseEvent>(OnObtainCurse);
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
        if (e.Description == "Attack") Dispatch(GameTriggerType.OnAttackPlayed, e);
        else if (e.Description == "Skill") Dispatch(GameTriggerType.OnSkillPlayed, e);
        else if (e.Description == "Power") Dispatch(GameTriggerType.OnPowerPlayed, e);
    }

    private void OnCardDraw(CardDrawEvent e) => Dispatch(GameTriggerType.OnCardDraw, e);
    private void OnDeckShuffled(DeckShuffledEvent e) => Dispatch(GameTriggerType.OnShuffleDeck, e);
    private void OnObtainCurse(ObtainCurseEvent e) => Dispatch(GameTriggerType.OnObtainCurse, e);

    private void Dispatch(GameTriggerType trigger, GameEvent e)
    {
        // 1. Collect all valid (Relic, Ability) pairs
        var queue = new List<(RelicInstance relic, RelicAbilityData ability)>();

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

        RelicInstance newInstance = new(relicData);
        ownedRelics.Add(newInstance);

        Debug.Log($"Relic Added: {relicData.relicName}");

        // Handle "Upon pickup" effects immediately if any (optional, depends on design)
        // If we had an OnObtain relic trigger, we'd fire an event here.
    }

    public void RemoveRelic(RelicInstance relic)
    {
        if (ownedRelics.Contains(relic))
        {
            // relic.OnRemove(); // No longer needed as RelicInstance is dumb
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
