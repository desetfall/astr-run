using System.Collections;
using UnityEngine;

public class StandartPlatformHard : MonoBehaviour
{
    private float speed = 0.0014f;
    private Transform platformTransform;

    private void Start()
    {
        int chance = Random.Range(1, 7);
        if (chance != 6) //рср 6
        {
            enabled = false;
        }
        platformTransform = transform;
        StartCoroutine(DirectionCheck());
    }

    IEnumerator DirectionCheck()
    {
        while (true)
        {
            yield return new WaitUntil(() => platformTransform.position.x > 0.25f);
            speed = -speed;
            yield return new WaitUntil(() => platformTransform.position.x < -0.25);
            speed = -speed;  
        }
    }

    private void FixedUpdate()
    {
        platformTransform.Translate(speed, 0, 0);
    }
}
