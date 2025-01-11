using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bomb : PoolableObject<Bomb>
{
    private float _minColorValue = 0f;
    private float _maxColorValue = 1f;

    private float _radius = 10f;
    private float _force = 2000f;

    public void Init(Vector3 position)
    {
        transform.position = position;
        SetDefaultColor();
        gameObject.SetActive(true);

        StartCoroutine(CountExplodeDelay());
    }

    public void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

        List<Rigidbody> rigidbodies = colliders.Select(collider => collider.attachedRigidbody).Where(collider => collider != null).ToList();

        foreach (Rigidbody rigidbody in rigidbodies)            
            rigidbody.AddExplosionForce(_force, transform.position, _radius);
    }

    private IEnumerator CountExplodeDelay()
    {
        int minValue = 2;
        int maxValue = 6;

        float delay = UnityEngine.Random.Range(minValue, maxValue);
        float time = 0f;

        Color color = Material.color;

        while (time < delay)
        {
            time += Time.deltaTime;

            color.a = Mathf.MoveTowards(_maxColorValue, _minColorValue, time / delay);
            Material.color = color;

            yield return null;
        }

        Explode();
        Release();
    }
}
