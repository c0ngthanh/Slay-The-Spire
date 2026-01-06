public interface IGameContext
{
    void DrawCards(int amount);
    void GainGold(int amount);
    void HealPlayer(int amount);
    void DamageRandomEnemy(int amount);
    void AddMana(int amount);
    // Add more as needed
}
