using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public abstract class PoolableObject<T> : MonoBehaviour where T : PoolableObject<T>
{
    protected Material Material;
    private Color DefaultColor;

    public event Action<T> Released;

    private void Awake()
    {
        Material = GetComponent<MeshRenderer>().material;
        DefaultColor = Material.color;
    }

    public void SetDefaultColor()
    {
        Material.color = DefaultColor;
    }

    protected void Release()
    {        
        Released?.Invoke((T)this);
    }
}
