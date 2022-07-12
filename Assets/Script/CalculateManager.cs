using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalculateManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI correctText, wrongText;

    // Start is called before the first frame update
    void Start()
    {
        correctText.text = HospitalityScore.Instance.correctAnswer.ToString();
        wrongText.text = HospitalityScore.Instance.wrongAnswer.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
