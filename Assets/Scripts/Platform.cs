using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Color _deathColor;

    public Color DeathColor => _deathColor;
}
