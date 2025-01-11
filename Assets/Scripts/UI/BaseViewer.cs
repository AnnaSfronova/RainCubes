using System;
using TMPro;
using UnityEngine;

public class BaseViewer<T, K> : MonoBehaviour where T : BaseSpawner<K> where K : PoolableObject<K>
{
    [SerializeField] private TextMeshProUGUI _spawned;
    [SerializeField] private TextMeshProUGUI _created;
    [SerializeField] private TextMeshProUGUI _activated;
    [SerializeField] private T _spawner;

    private void OnEnable()
    {
        _spawner.Spawned += PrintSpawned;
        _spawner.Created += PrintCreated;
        _spawner.Activated += PrintActivated;
    }
    private void OnDisable()
    {
        _spawner.Spawned -= PrintSpawned;
        _spawner.Created -= PrintCreated;
        _spawner.Activated -= PrintActivated;
    }

    private void PrintSpawned(int value)
    {
        _spawned.text = Convert.ToString(value);
    }

    private void PrintCreated(int value)
    {
        _created.text = Convert.ToString(value);
    }

    private void PrintActivated(int value)
    {
        _activated.text = Convert.ToString(value);
    }
}
