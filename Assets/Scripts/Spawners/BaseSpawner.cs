using UnityEngine;
using UnityEngine.Pool;

public abstract class BaseSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] protected int _poolMaxSize;

    protected ObjectPool<T> Pool;

    protected virtual void Awake()
    {
        Pool = new ObjectPool<T>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: T => ActionOnGet(T),
            actionOnRelease: T => T.gameObject.SetActive(false),
            actionOnDestroy: T => Destroy(T),
            collectionCheck: true,
            defaultCapacity: _poolMaxSize, 
            maxSize: _poolMaxSize);
    }

    protected abstract void ActionOnGet(T t);
}
