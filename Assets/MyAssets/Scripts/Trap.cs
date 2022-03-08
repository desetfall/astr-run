using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    void Start()
    {
        transform.rotation = Quaternion.Euler(0.0f, Random.Range(0.0f, 1000.0f), 0.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            other.GetComponent<Player>().PlayerDead();
        }   
    }
}
