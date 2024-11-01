using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private int _poolCapcity = 10;
    [SerializeField] private int _maxPoolSize = 10;
    [SerializeField] private int _minXPosition = -4;
    [SerializeField] private int _maxXPosition = 4;
    [SerializeField] private float _positionY = 5f;
    [SerializeField] private float _positionZ = 0f;
    [SerializeField] private float _delay = 3f;

    private ObjectPool<GameObject> _cubes;

    private void Awake()
    {
        _cubes = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(_cubePrefab),
            actionOnGet: cube => ActionOnGet(cube),
            actionOnRelease: cube => cube.SetActive(false),
            actionOnDestroy: cube => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapcity,
            maxSize: _maxPoolSize);
    }

    private void Start()
    {
        StartCoroutine(CreateObject(_delay));
    }

    private void ActionOnGet(GameObject cube)
    {
        cube.transform.position = new Vector3(Random.Range(_minXPosition, _maxXPosition), _positionY, _positionZ);

        if (cube.TryGetComponent(out Cube cubeComponent))
        {
            cubeComponent.Init();
            cubeComponent.Disappearing += ReturnInPool;
        }

        cube.SetActive(true);
    }

    private void ReturnInPool(GameObject cube)
    {
        if (cube.TryGetComponent(out Cube cubeComponent))
            cubeComponent.Disappearing -= ReturnInPool;

        _cubes.Release(cube);
    }

    private IEnumerator CreateObject(float delay)
    {
        var wait = new WaitForSeconds(delay);

        while (enabled)
        {
            _cubes.Get();
            yield return wait;
        }
    }
}
