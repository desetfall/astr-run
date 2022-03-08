using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comet : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    private float speed = -0.005f;
    private Transform tr;

    private void Start()
    {
        tr = transform;
    }

    private void Update()
    {
        tr.Translate(0, 0, speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            other.GetComponent<Player>().PlayerDead();
        }
    }
}
