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
            Debug.Log("������: " + PlayerPrefs.GetInt("bestScore"));
            Debug.Log("����: " + PlayerPrefs.GetInt("money"));
            Debug.Log("������� ���: " + PlayerPrefs.GetInt("currentSuite"));
            Debug.Log("��� ����: " + PlayerPrefs.GetString("allPlayerSuites"));
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
