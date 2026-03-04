using UnityEngine;
using UnityEngine.Pool;

public class SPool<T> where T : Component
{
    private T prefab;
    private Transform parent;
    private ObjectPool<T> pool;

    public SPool(T prefab, int initialSize, Transform parent = null, int maxSize = 1000)
    {
        this.prefab = prefab;
        this.parent = parent;

        pool = new ObjectPool<T>(
            createFunc: CreateSetup,
            actionOnGet: GetSetup,
            actionOnRelease: ReleaseSetup,
            actionOnDestroy: DestroySetup,
            collectionCheck: true,
            defaultCapacity: initialSize,
            maxSize: maxSize
        );

        // Pre-warm the pool
        T[] preWarmArray = new T[initialSize];
        for (int i = 0; i < initialSize; i++)
        {
            preWarmArray[i] = pool.Get();
        }
        for (int i = 0; i < initialSize; i++)
        {
            pool.Release(preWarmArray[i]);
        }
    }

    private T CreateSetup()
    {
        return Object.Instantiate(prefab, parent);
    }

    private void GetSetup(T instance)
    {
        instance.gameObject.SetActive(true);
        if (instance is IPoolable poolable)
        {
            poolable.OnSpawn();
        }
    }

    private void ReleaseSetup(T instance)
    {
        if (instance is IPoolable poolable)
        {
            poolable.OnDespawn();
        }
        instance.gameObject.SetActive(false);
    }

    private void DestroySetup(T instance)
    {
        if (instance != null)
        {
            Object.Destroy(instance.gameObject);
        }
    }

    public T Get()
    {
        return pool.Get();
    }

    public void ReturnToPool(T instance)
    {
        pool.Release(instance);
    }
}
