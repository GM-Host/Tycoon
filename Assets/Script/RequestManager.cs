using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RequestManager : MonoBehaviour
{
    private Guest guest;
    public bool correct;
    private int score;
    public TextMeshProUGUI scoreText, guestNameText, guestLocalText, guestPartyText, guestSpeciesText, guestProfessionText;

    [SerializeField]
    private Vector2 spawnPaperPos;
    [SerializeField]
    private GameObject closeUpIdentity;
    [SerializeField]
    private GameObject parent;  // paper ������Ʈ�� �θ� ������Ʈ(ĵ����)
    [SerializeField]
    private GameObject identityPrefab;  // �ź��� ������

    private GameObject identity;

    private float trueRatio = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        UpdateScore(0);
        VisitGuest();
    }

    public void VisitGuest()
    {
        // �ſ��� ������Ʈ ����
        identity = Instantiate(identityPrefab, Vector2.zero, Quaternion.identity);
        identity.transform.parent = parent.transform;
        identity.transform.localPosition = spawnPaperPos;
        identity.transform.SetSiblingIndex(1); // 2��°�� ������ (closeup���� ����)

        // �ſ��� ����
        correct = new System.Random().NextDouble() < trueRatio ? true : false;
        guest = GuestDB.CreateGuest(correct);

        guestNameText.text = guest.GetName();
        guestLocalText.text = guest.GetLocal();
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
        if (decision)   // ������ ���
        {
            if (correct)
                UpdateScore(1);
            else
                UpdateScore(-1);
        }
        Destroy(identity);
        closeUpIdentity.SetActive(false);
        VisitGuest();
    }
}
