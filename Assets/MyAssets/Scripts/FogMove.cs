using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogMove : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    private Transform fogPlaneTransform;

    [SerializeField]
    private float zOffset;

    void Start()
    {
        fogPlaneTransform = transform;
    }

    void Update()
    {
        fogPlaneTransform.position = new Vector3(0.0f, -0.0129f, playerTransform.position.z + zOffset);
    }
}
