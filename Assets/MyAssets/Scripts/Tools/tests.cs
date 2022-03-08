using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tests : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Debug.ClearDeveloperConsole();
            Debug.Log("Рекорд: " + PlayerPrefs.GetInt("bestScore"));
            Debug.Log("Лавэ: " + PlayerPrefs.GetInt("money"));
            Debug.Log("Текущий сет: " + PlayerPrefs.GetInt("currentSuite"));
            Debug.Log("Все сеты: " + PlayerPrefs.GetString("allPlayerSuites"));
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            Debug.ClearDeveloperConsole();
            PlayerPrefs.DeleteAll();
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            Debug.ClearDeveloperConsole();
            PlayerPrefs.SetInt("money", 2000);
        }
    }
}
