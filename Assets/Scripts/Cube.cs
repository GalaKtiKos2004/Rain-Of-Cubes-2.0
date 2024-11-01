using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent (typeof(CubeColorChanger))]
[RequireComponent (typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Color _startColor;
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    private CubeColorChanger _colorChanger;

    private List<Platform> _platforms = new List<Platform>();

    public event Action<GameObject> Disappearing;

    private void Awake()
    {
        _colorChanger = GetComponent<CubeColorChanger>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform))
        {
            if (_platforms.Contains(platform) == false)
            {
                _platforms.Add(platform);
                _colorChanger.Change(platform.DeathColor);
                StartCoroutine(CountDeathDelay(UnityEngine.Random.Range(_minLifeTime, _maxLifeTime)));
            }
        }
    }

    public void Init()
    {
        _colorChanger.Change(_startColor);
        _platforms.Clear();
    }

    private IEnumerator CountDeathDelay(float lifeTime)
    {
        var wait = new WaitForSeconds(lifeTime);

        yield return wait;

        Disappearing?.Invoke(gameObject);
    }
}
