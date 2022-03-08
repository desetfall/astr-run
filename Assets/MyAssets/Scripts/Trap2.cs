using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap2 : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    private Transform selfTransform;
    [SerializeField] private float firstPos, endPos;
    private float speed = 0.0014f;

    void Start()
    {
        selfTransform = transform;
        selfTransform.position = new Vector3(selfTransform.position.x, 0.1f, selfTransform.position.z);
        firstPos = selfTransform.position.z - (0.1799999f * 3.0f);
        endPos = selfTransform.position.z + 0.1799999f;
        StartCoroutine(DirectionCheck());
    }

    IEnumerator DirectionCheck()
    {
        while (true)
        {
            yield return new WaitUntil(() => selfTransform.position.z > endPos);
            speed = -speed;
            yield return new WaitUntil(() => selfTransform.position.z < firstPos);
            speed = -speed;
        }
    }

    private void FixedUpdate()
    {
        selfTransform.Translate(0, 0, speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            other.GetComponent<Player>().PlayerDead();
        }
    }
}
