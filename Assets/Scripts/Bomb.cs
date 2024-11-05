using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Renderer))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private float _delay;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        StartCoroutine(CountExplodeDelay(_delay));
    }

    private IEnumerator CountExplodeDelay(float delay)
    {
        float elapsedTime = 0f;

        float previousAlphaValue = _renderer.material.color.a;
        float minAlphaValue = 0f;

        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;

            float normalizedPosition = elapsedTime / delay;

            _renderer.material.color = new Color(_renderer.material.color.r, _renderer.material.color.g, _renderer.material.color.b,
                Mathf.Lerp(previousAlphaValue, minAlphaValue, normalizedPosition));

            yield return null;
        }
    }
}
