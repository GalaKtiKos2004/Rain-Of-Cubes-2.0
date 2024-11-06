using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
public class Bomb : PoolableObject<Bomb>
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    private Renderer _renderer;

    private float _minAlphaValue = 0f;
    private float _maxAlphaValue = 1f;

    //public event Action<Bomb> Exploded;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Init(Vector3 position)
    {
        transform.position = position;

        Material material = _renderer.material;
        _renderer.material.color = new Color(material.color.r, material.color.g, material.color.b, _maxAlphaValue);

        StartCoroutine(CountExplodeDelay(Random.Range(_minLifeTime, _maxLifeTime)));
    }

    public void Explode()
    {
        List<Rigidbody> explodableObjects = GetExplodableObjects();

        //Exploded?.Invoke(this);
        Disable();

        foreach (Rigidbody explodableObject in explodableObjects)
        {
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                cubes.Add(hit.attachedRigidbody);
            }
        }
        
        return cubes;
    }

    private IEnumerator CountExplodeDelay(float delay)
    {
        Material material = _renderer.material;

        float elapsedTime = 0f;

        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;

            float normalizedPosition = elapsedTime / delay;

            _renderer.material.color = new Color(material.color.r, material.color.g, material.color.b,
                Mathf.Lerp(_maxAlphaValue, _minAlphaValue, normalizedPosition));

            yield return null;
        }

        Explode();
    }
}
