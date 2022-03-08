using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomXtool : MonoBehaviour
{
    public static float GetRandomX() //Возвращается случайное положение новой платформы по оси Х (слева, посередине, справа)
    {
        float X;
        int temp = Random.Range(1, 4); //1 - Слева, 2 - посередине, 3 - справа
        switch (temp)
        {
            case 1:
                X = -0.25f;
                break;
            case 2:
                X = 0.0f;
                break;
            case 3:
                X = 0.25f;
                break;
            default:
                X = 0.0f;
                break;
        }
        return X;
    }


}
