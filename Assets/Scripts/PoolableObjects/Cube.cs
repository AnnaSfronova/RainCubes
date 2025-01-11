using System.Collections;
using UnityEngine;

public class Cube : PoolableObject<Cube>
{
    private bool _isTouch;

    public void Init(Vector3 position)
    {
        _isTouch = false;
        transform.position = position;
        SetDefaultColor();
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isTouch)
            return;

        if (collision.gameObject.TryGetComponent<Platform>(out _) == false)
            return;

        _isTouch = true;
        ChangeColor();
        StartCoroutine(CountLifetime());
    }

    private void ChangeColor()
    {
        Material.color = UnityEngine.Random.ColorHSV();
    }

    private IEnumerator CountLifetime()
    {
        int minValue = 2; 
        int maxValue = 6;

        WaitForSeconds wait = new(UnityEngine.Random.Range(minValue, maxValue));

        yield return wait;

        Release();
    }
}
