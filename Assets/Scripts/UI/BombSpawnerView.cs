using UnityEngine;

public class BombSpawnerView : SpawnerView
{
    [SerializeField] private BombSpawner _spawner;

    private void OnEnable()
    {
        _spawner.BombSpawned += UpdateSpawnedObjects;
        _spawner.BombInstantiated += UpdateInstantiatedObjects;
        _spawner.ActiveBombCountChanged += UpdateActiveObjects;
    }

    private void OnDisable()
    {
        _spawner.BombSpawned -= UpdateSpawnedObjects;
        _spawner.BombInstantiated -= UpdateInstantiatedObjects;
        _spawner.ActiveBombCountChanged -= UpdateActiveObjects;
    }
}
