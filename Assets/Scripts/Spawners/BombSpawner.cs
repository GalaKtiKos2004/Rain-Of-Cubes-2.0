using System;
using UnityEngine;

public class BombSpawner : BaseSpawner<Bomb>
{
    public event Action<int> BombSpawned;
    public event Action<int> BombInstantiated;
    public event Action<int> ActiveBombCountChanged;

    private int _spawnedBombsCount;

    public void CreateBomb(Vector3 position)
    {
        Bomb bomb = Pool.Get();
        bomb.Init(position);
    }

    protected override Bomb CreateObject()
    {
        Bomb bomb = Instantiate(Prefab);
        BombInstantiated?.Invoke(Pool.CountAll);
        return bomb;
    }

    protected override void ActionOnGet(Bomb bomb)
    {
        bomb.Exploded += ReturnInPool;
        bomb.gameObject.SetActive(true);
        BombSpawned?.Invoke(++_spawnedBombsCount);
        ActiveBombCountChanged?.Invoke(Pool.CountActive);
    }

    protected override void ReturnInPool(Bomb bomb)
    {
        bomb.Exploded -= ReturnInPool;
        Pool.Release(bomb);
        ActiveBombCountChanged?.Invoke(Pool.CountActive);
    }
}
