using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBoost : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            other.GetComponent<Player>().RunSpeedBoost();
            Destroy(gameObject);
        }
    }
}
