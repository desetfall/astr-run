using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    private AudioSource asSounds;

    private void Start()
    {
        asSounds = gameObject.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            asSounds.Play();
            PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") + 30);
            Destroy(gameObject);
        }
    }
}
