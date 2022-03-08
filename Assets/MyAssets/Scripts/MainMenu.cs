using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text txtGameName, txtBestScore, txtMoney;
    [SerializeField] private Animator cameraAnimator, mainPanelAnimator, inGameScoreAnimator;
    [SerializeField] private GameObject dLmenu, dLgame;
    [SerializeField] private GameObject touchSystem;
    private const string START_GAME_TRIG = "startGameTrigger";

    void Start()
    {
        txtGameName.fontSize = 300;
        StartCoroutine(GameNameAnim());
        txtBestScore.text = "Best score: " + PlayerPrefs.GetInt("bestScore").ToString(); //������
        txtMoney.text = PlayerPrefs.GetInt("money").ToString();
    }

    IEnumerator GameNameAnim() //�������� ����������-���������� �������� ���� � ������� ����
    {
        while (true)
        {
            for (int i = 300; i >= 280; i--)
            {
                txtGameName.fontSize = i;
                yield return new WaitForSeconds(0.015f);
            }
            for (int i = 280; i <= 300; i++)
            {
                txtGameName.fontSize = i;
                yield return new WaitForSeconds(0.015f);
            }
        }
    }

    public void StartGame() //������ �����
    {   
        cameraAnimator.SetTrigger(START_GAME_TRIG);
        mainPanelAnimator.SetTrigger(START_GAME_TRIG);
        inGameScoreAnimator.SetTrigger(START_GAME_TRIG);
        dLgame.SetActive(true);
        dLmenu.SetActive(false);
        touchSystem.SetActive(true);
    }
}
