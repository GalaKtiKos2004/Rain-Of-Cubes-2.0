using UnityEngine;

public class CubeColorChanger : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Change(Color color)
    {
        _renderer.material.color = color;
    }
}
