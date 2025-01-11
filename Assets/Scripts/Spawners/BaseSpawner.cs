using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class BaseSpawner<T> : MonoBehaviour where T : PoolableObject<T>
{
    [SerializeField] private T _prefab;

    private int _createdObjects;
    private int _spawnedObjects;

    protected ObjectPool<T> Pool;

    public event Action<int> Spawned;
    public event Action<int> Created;
    public event Action<int> Activated;

    private void Awake()
    {
        Pool = new ObjectPool<T>(
            createFunc: ActionCreate,
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            defaultCapacity: 10,
            maxSize: 20
            );
    }

    private T ActionCreate()
    {
        _createdObjects++;
        Created?.Invoke(_createdObjects);

        return Instantiate(_prefab);
    }

    protected virtual void ActionOnGet(T obj)
    {
        obj.Released += ActionOnRelease;

        _spawnedObjects++;
        Spawned?.Invoke(_spawnedObjects);
        Activated?.Invoke(Pool.CountActive);
    }

    protected virtual void ActionOnRelease(T obj)
    {
        obj.Released -= ActionOnRelease;
        Pool.Release(obj);

        Activated?.Invoke(Pool.CountActive);
    }
}
