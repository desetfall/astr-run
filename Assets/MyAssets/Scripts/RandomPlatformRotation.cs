using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlatformRotation : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0.0f, Random.Range(0.0f, 1000.0f), 0.0f);
        enabled = false;
    }
}
