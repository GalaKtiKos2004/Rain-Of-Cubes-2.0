using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : BaseSpawner<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;

    [SerializeField] private int _minXPosition = -4;
    [SerializeField] private int _maxXPosition = 4;
    [SerializeField] private float _positionY = 5f;
    [SerializeField] private float _positionZ = 0f;
    [SerializeField] private float _delay = 3f;

    private int _spawnedCubesCount;

    private void Start()
    {
        StartCoroutine(CreateObject(_delay));
    }

    protected override void ActionOnGet(Cube cube)
    {
        base.ActionOnGet(cube);

        cube.transform.position = new Vector3(Random.Range(_minXPosition, _maxXPosition), _positionY, _positionZ);
        cube.Init();
        cube.Disappearing += ReturnInPool;
    }

    protected override void ReturnInPool(Cube cube)
    {
        cube.Disappearing -= ReturnInPool;

        _bombSpawner.CreateBomb(cube.transform.position);
        Pool.Release(cube);
    }

    private IEnumerator CreateObject(float delay)
    {
        var wait = new WaitForSeconds(delay);

        while (enabled)
        {
            Pool.Get();
            yield return wait;
        }
    }
}
