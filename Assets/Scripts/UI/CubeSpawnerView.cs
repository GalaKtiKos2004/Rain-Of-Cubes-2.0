using UnityEngine;

public class CubeSpawnerView : SpawnerView
{
    [SerializeField] private CubeSpawner _spawner;

    private void OnEnable()
    {
        _spawner.CubeSpawned += UpdateSpawnedObjects;
        _spawner.CubeInstantiated += UpdateInstantiatedObjects;
        _spawner.ActiveCubeCountChanged += UpdateActiveObjects;
    }

    private void OnDisable()
    {
        _spawner.CubeSpawned -= UpdateSpawnedObjects;
        _spawner.CubeInstantiated -= UpdateInstantiatedObjects;
        _spawner.ActiveCubeCountChanged -= UpdateActiveObjects;
    }
}
