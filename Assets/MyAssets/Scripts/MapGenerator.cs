using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private const float DELTA_BTW_CYLS = 0.5f;
    private const float DELTA_BTW_BOXES = 0.1799999f;
    private const float DELTA_BTW_HALF_N_BOXES = 0.09f;
    private const float Y_POS = -0.0434f;
    private const string CYL_TAG = "cyl";

    [SerializeField] private GameObject cyl, box, startHalf, endHalf, trap, trap2, coin, runBoostCoin, comet;
    private float lastZposition = 0.0f, coinHeight = 0.5f;
    private Vector3 platformPos = Vector3.zero;

    void Start() //Создаём первые 15 платформ
    {
        platformPos.y = Y_POS;
        for (int i = 0; i < 15; i++)
        {
            lastZposition += DELTA_BTW_CYLS;
            platformPos.x = RandomXtool.GetRandomX();
            platformPos.z = lastZposition;
            Instantiate(cyl, platformPos, Quaternion.identity);
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        Player.PlayerScore++; //Добавить 1 очко игроку
        if (other.CompareTag(CYL_TAG)) //Если это обычный круглый цилиндр или конечная половинка дороги
        {
            Destroy(other.gameObject);
            int temp = Random.Range(1, 6); // 1, 2, 3, 4 - обычная платформа, 5 - дорога
            if (temp == 5)
            {               
                InstPrefab(DELTA_BTW_CYLS, true, startHalf);
                InstPrefab(DELTA_BTW_HALF_N_BOXES, false, box);
                int roadLenght = Random.Range(3, 8);
                int trapPosition = 0;
                if (roadLenght > 5)
                {
                    trapPosition = Mathf.RoundToInt(roadLenght / 2);
                }
                for (int i = 0; i < roadLenght - 1; i++)
                {                   
                    InstPrefab(DELTA_BTW_BOXES, false, box);
                    if (trapPosition != 0 && trapPosition == i)
                    {
                        InstTrap();
                    }
                }                
                InstPrefab(DELTA_BTW_HALF_N_BOXES, false, endHalf);
            }
            else
            {
                InstPrefab(DELTA_BTW_CYLS, true, cyl);               
                if (Random.Range(1, 8) == 7) //Создать коменту с шансом 1 к 7
                {
                    Vector3 cometPos = new Vector3(RandomXtool.GetRandomX(), 0.24f, platformPos.z + 5.0f);
                    Instantiate(comet, cometPos, Quaternion.identity);
                }
            }
        }
        else
        {
            Destroy(other.gameObject);
        }
    }

    void InstPrefab(float delta, bool isNewXNedeed, GameObject prefab) 
    {
        lastZposition += delta;
        platformPos.z = lastZposition;
        if (isNewXNedeed)
        {
            float newX = RandomXtool.GetRandomX();
            platformPos.x = newX;
            if (newX != 0.0f) //Если платформе не посередине (чтобы монеты спавнились противоположно крайним платформам)
            {
                if (Random.Range(1, 6) == 5) //Создание монеты с шансом 1 к 5
                {
                    Vector3 coinPos = new Vector3(0.0f, coinHeight, lastZposition + 0.15f);
                    Instantiate(coin, coinPos, Quaternion.identity);
                }
            }
        }
        Instantiate(prefab, platformPos, Quaternion.identity);
    }

    void InstTrap()
    {
        Vector3 trapPos = new Vector3(platformPos.x, platformPos.y + 0.02f, platformPos.z);
        int temp = Random.Range(1, 4);
        if (temp == 1)
        {
            Instantiate(trap, trapPos, Quaternion.identity);
        }
        else if (temp == 2)
        {
            Instantiate(trap2, trapPos, Quaternion.identity);
        }  
        else
        {
            Instantiate(runBoostCoin, trapPos, Quaternion.identity);
        }
    }

}
