using System;
using TMPro;
using UnityEngine;

public class SpawnerView<T, K> : MonoBehaviour where T : BaseSpawner<K> where K : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _spawnedText;
    [SerializeField] private TextMeshProUGUI _instantiatedText;
    [SerializeField] private TextMeshProUGUI _activeText;
    [SerializeField] private T _spawner;

    private void OnEnable()
    {
        _spawner.ObjectSpawned += UpdateSpawnedObjects;
        _spawner.ObjectInstantiated += UpdateInstantiatedObjects;
        _spawner.ActiveObjectCountChanged += UpdateActiveObjects;
    }

    private void OnDisable()
    {
        _spawner.ObjectSpawned -= UpdateSpawnedObjects;
        _spawner.ObjectInstantiated -= UpdateInstantiatedObjects;
        _spawner.ActiveObjectCountChanged -= UpdateActiveObjects;
    }

    private void UpdateSpawnedObjects(int objectsCount)
    {
        _spawnedText.text = Convert.ToString(objectsCount);
    }

    private void UpdateInstantiatedObjects(int objectsCount)
    {
        _instantiatedText.text = Convert.ToString(objectsCount);
    }

    private void UpdateActiveObjects(int objectsCount)
    {
        _activeText.text = Convert.ToString(objectsCount);
    }
}
