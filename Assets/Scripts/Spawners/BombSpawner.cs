using UnityEngine;

public class BombSpawner : BaseSpawner<Bomb>
{
    public void CreateBomb(Vector3 position)
    {
        Bomb bomb = Pool.Get();
        bomb.Init(position);
    }

    protected override void ReturnInPool(Bomb bomb)
    {
        base.ReturnInPool(bomb);
        Pool.Release(bomb);
    }
}
