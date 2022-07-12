using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RequestManager : MonoBehaviour
{
    private Guest guest;
    public bool correct;
    private int score;
    public TextMeshProUGUI scoreText, guestNameText, guestLocalText, guestPartyText, guestSpeciesText, guestProfessionText;

    [SerializeField]
    private Vector2 spawnPaperPos;
    

    private GameObject identity;

    private float trueRatio = 0.6f;

    // �ʿ��� ������Ʈ
    [SerializeField]
    private GuestDB guestDB;
    [SerializeField]
    private GameObject closeUpIdentity;
    [SerializeField]
    private GameObject stampArea;   // ���� ������ ���� �θ� ������Ʈ
    [SerializeField]
    private GameObject parent;  // paper ������Ʈ�� �θ� ������Ʈ(ĵ����)
    [SerializeField]
    private Image professionSeal;
    [SerializeField]
    private GameObject identityPrefab;  // �ź��� ������

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
        identity.transform.SetParent(parent.transform);
        identity.transform.localPosition = spawnPaperPos;
        identity.transform.SetSiblingIndex(2); // 3��°�� ������ (background, character���� ����)

        // �ſ��� ����
        correct = new System.Random(System.Guid.NewGuid().GetHashCode()).NextDouble() < trueRatio ? true : false;
        guest = guestDB.CreateGuest(correct);

        guestNameText.text = guest.GetName();
        guestLocalText.text = guest.GetLocal();
        guestPartyText.text = guest.GetParty();
        guestSpeciesText.text = guestDB.GetSpeciesText(guest.GetSpecies());
        guestProfessionText.text = guestDB.GetProfessiosText(guest.GetProfession());
        professionSeal.sprite = guest.GetProfessionSeal();
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
        Destroy(identity);  // ��� �ſ��� ����
        Destroy(stampArea.transform.GetChild(0).gameObject);   // ���� ����
        closeUpIdentity.SetActive(false);
        VisitGuest();
    }
}
