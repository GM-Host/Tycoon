using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RequestManager : MonoBehaviour
{
    private Guest guest;
    public bool correct;
    private int score;
    public TextMeshProUGUI scoreText, guestNameText, guestPartyText, guestSpeciesText, guestProfessionText;

    [SerializeField]
    private Vector2 spawnPaperPos;
    [SerializeField]
    private GameObject CloseUp;
    [SerializeField]
    private GameObject parent;  // paper 오브젝트의 부모 오브젝트(캔버스)
    private GameObject paper;

    private float trueRatio = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        UpdateScore(0);
        VisitGuest();
    }

    public void VisitGuest()
    {
        // 신원서 오브젝트 스폰
        paper = Instantiate(Resources.Load("paper") as GameObject, Vector2.zero, Quaternion.identity);
        paper.transform.parent = parent.transform;
        paper.transform.localPosition = spawnPaperPos;
        paper.transform.SetSiblingIndex(1); // 2번째로 렌더링 (closeup보다 먼저)

        // 신원서 생성
        correct = new System.Random().NextDouble() < trueRatio ? true : false;
        guest = GuestDB.CreateGuest(correct);

        guestNameText.text = guest.GetName();
        guestPartyText.text = guest.GetParty();
        guestSpeciesText.text = GuestDB.GetSpeciesText(guest.GetSpecies());
        guestProfessionText.text = GuestDB.GetProfessiosText(guest.GetProfession());
    }

    public void UpdateScore(int num)
    {
        score += num;
        scoreText.text = score.ToString();
    }

    public void DecisionComplete(bool decision)
    {
        if (decision)   // 승인한 경우
        {
            if (correct)
                UpdateScore(1);
            else
                UpdateScore(-1);
        }
        Destroy(paper);
        CloseUp.SetActive(false);
        VisitGuest();
    }
}
