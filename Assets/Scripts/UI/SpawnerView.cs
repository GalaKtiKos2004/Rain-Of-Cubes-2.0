using System;
using TMPro;
using UnityEngine;

public abstract class SpawnerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _spawnedText;
    [SerializeField] private TextMeshProUGUI _instantiatedText;
    [SerializeField] private TextMeshProUGUI _activeText;

    protected void UpdateSpawnedObjects(int objectsCount)
    {
        _spawnedText.text = Convert.ToString(objectsCount);
    }

    protected void UpdateInstantiatedObjects(int objectsCount)
    {
        _instantiatedText.text = Convert.ToString(objectsCount);
    }

    protected void UpdateActiveObjects(int objectsCount)
    {
        _activeText.text = Convert.ToString(objectsCount);
    }
}
