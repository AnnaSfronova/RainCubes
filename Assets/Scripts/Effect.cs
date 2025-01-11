using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(CountLifetime());
    }

    private IEnumerator CountLifetime()
    {
        yield return new WaitForSeconds(1f);

        Destroy(this);
    }
}
