using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class BaseSpawner<T> : MonoBehaviour where T : PoolableObject<T>
{
    [SerializeField] private T _prefab;
    [SerializeField] protected int _poolMaxSize;

    protected ObjectPool<T> Pool;

    private int _spawnedCount;

    public event Action<int> ObjectSpawned;
    public event Action<int> ObjectInstantiated;
    public event Action<int> ActiveObjectCountChanged;

    protected T Prefab => _prefab;

    private void Awake()
    {
        Pool = new ObjectPool<T>(
            createFunc: CreateObject,
            actionOnGet: T => ActionOnGet(T),
            actionOnRelease: T => ActionOnRelease(T),
            actionOnDestroy: T => Destroy(T),
            collectionCheck: true,
            defaultCapacity: _poolMaxSize,
            maxSize: _poolMaxSize);

        _spawnedCount = 0;
    }

    protected virtual void ActionOnGet(T t)
    {
        t.gameObject.SetActive(true);
        _spawnedCount++;
        ObjectSpawned?.Invoke(_spawnedCount);
        ActiveObjectCountChanged?.Invoke(Pool.CountActive);
        t.Disabled += ReturnInPool;
    }


    protected virtual void ReturnInPool(T t)
    {
        t.Disabled -= ReturnInPool;
    }

    private void ActionOnRelease(T obj)
    {
        obj.gameObject.SetActive(false);
        ActiveObjectCountChanged?.Invoke(Pool.CountActive);
    }

    private T CreateObject()
    {
        int objectsInPoolCount = Pool.CountAll + 1;

        T obj = Instantiate(Prefab);
        ObjectInstantiated?.Invoke(objectsInPoolCount);
        return obj;
    }
}
