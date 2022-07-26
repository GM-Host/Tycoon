using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CalculateManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI dateText, correctText, wrongText;

    // Start is called before the first frame update
    void Start()
    {
        dateText.text = DataController.Instance.gameData.date.ToString();
        correctText.text = HospitalityScore.Instance.correctAnswer.ToString();
        wrongText.text = HospitalityScore.Instance.wrongAnswer.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickHospitality()
    {
        SceneManager.LoadScene("Hospitality");
    }
}
