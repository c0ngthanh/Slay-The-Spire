public interface IPoolable
{
    void OnSpawn(); // Called automatically when Get() is called
    void OnDespawn(); // Called automatically when ReturnToPool() is called
}
