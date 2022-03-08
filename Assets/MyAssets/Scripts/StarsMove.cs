using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsMove : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    private Transform selfTransform;

    private bool secondFrame = false;

    private float z;

    private void Start()
    {
        selfTransform = transform;
        z = selfTransform.position.z;
    }

    void Update()
    {
        secondFrame = !secondFrame;
        Vector3 playerPos = playerTransform.position;
        z = secondFrame ? playerPos.z / 2.0f : z;
        selfTransform.position = new Vector3(playerPos.x, playerPos.y, z);
    }
}
