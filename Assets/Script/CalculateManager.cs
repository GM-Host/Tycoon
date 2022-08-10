using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CalculateManager : MonoBehaviour
{
    private bool update;    // 골드 업데이트 했는지 여부
    private int correctGold = 10, wrongGold = 10;

    [SerializeField]
    private TextMeshProUGUI dateText, correctText, wrongText, goldText;

    // Start is called before the first frame update
    void Start()
    {
        dateText.text = DataController.Instance.gameData.date.ToString();
        correctText.text = HospitalityScore.Instance.correctAnswer.ToString();
        wrongText.text = HospitalityScore.Instance.wrongAnswer.ToString();
        goldText.text = DataController.Instance.gameData.gold.ToString();
    }

    public void ClickHospitality()
    {
        SceneManager.LoadScene("Hospitality");
    }

    // 판넬 클릭
    public void ClickPanel()
    {
        if (!update)
        {
            UpdateGold();
            update = !update;
        }
    }

    private void UpdateGold()
    {
        int gold = HospitalityScore.Instance.correctAnswer * correctGold - HospitalityScore.Instance.wrongAnswer * wrongGold; // 골드 증감량
        DataController.Instance.gameData.UpdateGold(gold);                  // 골드 업데이트
        goldText.text = DataController.Instance.gameData.gold.ToString();   // 갱신된 골드 표시
    }
}
