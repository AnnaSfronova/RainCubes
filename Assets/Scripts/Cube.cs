using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private Color _defaultColor;
    private Material _material;
    private bool _isPlatformTouched;

    public event Action<Cube> CubeRelease;

    public void Init(Vector3 position)
    {
        _material = GetComponent<MeshRenderer>().material;
        _defaultColor = _material.color;
        _isPlatformTouched = false;
        transform.position = position;
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isPlatformTouched)
            return;

        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform) == false)
            return;

        _isPlatformTouched = true;
        ChangeColor();
        StartCoroutine(Release());
    }

    private void ChangeColor()
    {
        _material.color = UnityEngine.Random.ColorHSV();
    }

    public void ResetColor()
    {
        _material.color = _defaultColor;
    }

    private IEnumerator Release()
    {
        int minValue = 2; 
        int maxValue = 6;

        WaitForSeconds wait = new(UnityEngine.Random.Range(minValue, maxValue));

        yield return wait;

        CubeRelease?.Invoke(this);
    }
}
