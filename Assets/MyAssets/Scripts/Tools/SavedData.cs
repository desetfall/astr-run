using UnityEngine;

public class SavedData : MonoBehaviour
{
    void Awake()
    {
        if (!PlayerPrefs.HasKey("firstLoad"))
        {
            PlayerPrefs.SetInt("firstLoad", 1);
            PlayerPrefs.SetInt("bestScore", 0);
            PlayerPrefs.SetInt("money", 10000);
            PlayerPrefs.SetInt("currentSuite", 9999); //Говнокод
            PlayerPrefs.SetString("allPlayerSuites", "");
        }
    }
}
