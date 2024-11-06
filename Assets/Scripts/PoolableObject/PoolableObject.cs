using System;
using UnityEngine;

public class PoolableObject<T> : MonoBehaviour where T : PoolableObject<T>
{
    public event Action<T> Disabled;

    protected void Disable()
    {
        Disabled?.Invoke((T)this);
    }
}
