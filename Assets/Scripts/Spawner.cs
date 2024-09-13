using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cubePrefab),
            actionOnGet: (cube) => OnGet(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            defaultCapacity: 10,
            maxSize: 20
            );
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private void OnGet(Cube cube)
    {
        cube.CubeRelease += OnRelease;
        cube.Init(GetRandomPosition());
    }

    private void OnRelease(Cube cube)
    {
        cube.CubeRelease -= OnRelease;
        cube.ResetColor();
        _pool.Release(cube);
    }

    private Vector3 GetRandomPosition()
    {
        float minOffset = -8f;
        float maxOffset = 8f;

        float positionY = 12f;

        float positionX = Random.Range(minOffset, maxOffset);
        float positionZ = Random.Range(minOffset, maxOffset);

        return new Vector3(positionX, positionY, positionZ);
    }

    private IEnumerator Spawn()
    {
        float delay = 1;

        WaitForSeconds wait = new (delay);

        while (true)
        {
            _pool.Get();
            yield return wait;
        }
    }
}
