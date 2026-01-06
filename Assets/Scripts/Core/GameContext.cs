using UnityEngine;

public class GameContext : IGameContext
{
    // In a real implementation, these would call actual Singletons.
    // e.g. CombatSystem.Instance.DrawCards(amount);
    
    public void DrawCards(int amount)
    {
        Debug.Log($"[GameContext] Drawing {amount} cards.");
        // CombatSystem.Instance?.DrawCards(amount);
    }

    public void GainGold(int amount)
    {
        Debug.Log($"[GameContext] Gained {amount} Gold.");
        // GameManager.Instance?.AddGold(amount);
    }

    public void HealPlayer(int amount)
    {
        Debug.Log($"[GameContext] Player healed for {amount}.");
        // PlayerController.Instance?.Heal(amount);
    }

    public void DamageRandomEnemy(int amount)
    {
        Debug.Log($"[GameContext] Dealt {amount} damage to a random enemy.");
        // CombatSystem.Instance?.DamageRandomEnemy(amount);
    }

    public void AddMana(int amount)
    {
        Debug.Log($"[GameContext] Added {amount} mana.");
    }
}
