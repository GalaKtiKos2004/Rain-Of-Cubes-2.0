using System;
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

    public event Action<int> CubeSpawned;
    public event Action<int> CubeInstantiated;
    public event Action<int> ActiveCubeCountChanged;

    private void Start()
    {
        StartCoroutine(CreateObject(_delay));
    }

    protected override Cube CreateObject()
    {
        Cube cube = Instantiate(Prefab);
        CubeInstantiated?.Invoke(Pool.CountAll);
        return cube;
    }

    protected override void ActionOnGet(Cube cube)
    {
        cube.transform.position = new Vector3(Random.Range(_minXPosition, _maxXPosition), _positionY, _positionZ);

        cube.Init();
        cube.Disappearing += ReturnInPool;

        cube.gameObject.SetActive(true);

        CubeSpawned?.Invoke(++_spawnedCubesCount);
        ActiveCubeCountChanged?.Invoke(Pool.CountActive);
    }

    protected override void ReturnInPool(Cube cube)
    {
        cube.Disappearing -= ReturnInPool;

        _bombSpawner.CreateBomb(cube.transform.position);
        Pool.Release(cube);

        ActiveCubeCountChanged?.Invoke(Pool.CountActive);
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
