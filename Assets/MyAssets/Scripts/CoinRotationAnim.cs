using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotationAnim : MonoBehaviour
{
    private Transform selfTransform;
    private float rotSpeed = 1.0f;

    void Start()
    {
        selfTransform = transform;
    }

    void Update()
    {
        selfTransform.Rotate(Vector3.up, rotSpeed);
    }
}
