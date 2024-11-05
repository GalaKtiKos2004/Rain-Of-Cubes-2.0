using UnityEngine;

public class BombSpawner : BaseSpawner<Bomb>
{
    public void CreateBomb(Vector3 position)
    {
        Bomb bomb = Pool.Get();
        bomb.Init(position);
    }

    protected override void ActionOnGet(Bomb bomb)
    {
        base.ActionOnGet(bomb);

        bomb.Exploded += ReturnInPool;
    }

    protected override void ReturnInPool(Bomb bomb)
    {
        bomb.Exploded -= ReturnInPool;
        Pool.Release(bomb);
    }
}
