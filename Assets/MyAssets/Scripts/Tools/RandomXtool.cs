using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomXtool : MonoBehaviour
{
    public static float GetRandomX() //������������ ��������� ��������� ����� ��������� �� ��� � (�����, ����������, ������)
    {
        float X;
        int temp = Random.Range(1, 4); //1 - �����, 2 - ����������, 3 - ������
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
