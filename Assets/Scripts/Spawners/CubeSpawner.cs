using System.Collections;
using UnityEngine;

public class CubeSpawner : BaseSpawner<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;

    private float _minPosition = -8f;
    private float _maxPosition = 8f;
    private float _positionY = 12f;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    protected override void ActionOnGet(Cube cube)
    {
        base.ActionOnGet(cube);
        cube.Init(GetRandomPosition());
    }

    protected override void ActionOnRelease(Cube cube)
    {
        base.ActionOnRelease(cube);
        _bombSpawner.CreateBomb(cube.transform.position);
    }

    private Vector3 GetRandomPosition()
    {
        float positionX = Random.Range(_minPosition, _maxPosition);
        float positionZ = Random.Range(_minPosition, _maxPosition);

        return new Vector3(positionX, _positionY, positionZ);
    }

    private IEnumerator Spawn()
    {
        float delay = 1;

        WaitForSeconds wait = new(delay);

        while (true)
        {
            Pool.Get();
            yield return wait;
        }
    }
}
