using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RequestManager : MonoBehaviour
{
    private Guest guest;    // ������ ���谡 ����
    private bool correct;   // ���谡 ���� ����
    private bool decision;  // ����/���� ����
    private GameObject identity, tierSeal;  // ������ �ſ���, ��ǥ ������Ʈ

    private float trueRatio = 0.6f;

    [SerializeField]
    private Vector2 spawnIdentityPos, spawnTierSealPos;     // �ſ���, ��ǥ ������Ʈ�� ���� ��ġ

    // �ʿ��� ������Ʈ
    [SerializeField]
    private GuestDB guestDB;
    [SerializeField]
    private GameObject closeUpIdentity;    // Ŭ����� ������Ʈ (�ſ���)
    [SerializeField]
    private GameObject stampArea;   // ���� ������ ���� �θ� ������Ʈ
    [SerializeField]
    private GameObject parent;  // paper ������Ʈ�� �θ� ������Ʈ(ĵ����)
    [SerializeField]
    private Image guestProfessionSeal, guestTierSeal;     // ���� ���� �̹���, Ƽ�� ��ǥ �̹���
    [SerializeField]
    private GameObject identityPrefab, tierSealPrefab;  // �ź��� ������, ��ǥ ������
    [SerializeField]
    private DialogManager dialogManager;    // ��ȭ ����� ���� DialogManager ������Ʈ
    [SerializeField]
    private TextMeshProUGUI guestNameText, guestLocalText, guestPartyText, guestSpeciesText, guestProfessionText, guestTierText;


    // Start is called before the first frame update
    void Start()
    {
        UpdateDate();   // ��¥ ����
        VisitGuest();
    }

    public void VisitGuest()
    {
        // �ſ��� ������Ʈ ����
        identity = Instantiate(identityPrefab, Vector2.zero, Quaternion.identity);
        identity.transform.SetParent(parent.transform);
        identity.transform.localPosition = spawnIdentityPos;
        identity.transform.SetSiblingIndex(2); // 3��°�� ������ (background, character ����)

        // ��ǥ ������Ʈ ����
        tierSeal = Instantiate(tierSealPrefab, Vector2.zero, Quaternion.identity);
        tierSeal.transform.SetParent(parent.transform);
        tierSeal.transform.localPosition = spawnTierSealPos;
        tierSeal.transform.SetSiblingIndex(3); // 4��°�� ������ (background, character ����)

        // �ſ��� ������ ����
        correct = new System.Random(System.Guid.NewGuid().GetHashCode()).NextDouble() < trueRatio ? true : false;
        guest = guestDB.CreateGuest(correct);
        
        // �ſ���(Ŭ�����)�� ǥ��
        guestNameText.text = guest.GetName();
        guestLocalText.text = guest.GetLocal();
        guestPartyText.text = guest.GetParty();
        guestSpeciesText.text = guestDB.GetSpeciesText(guest.GetSpecies());
        guestProfessionText.text = guestDB.GetProfessiosText(guest.GetProfession());
        guestProfessionSeal.sprite = guest.GetProfessionSeal();
        guestTierText.text=guest.GetTier().ToString();

        // ��ǥ �̹��� ����
        guestTierSeal.sprite=guest.GetTierSeal();

        // �ȳ� ��ȭ ���
        dialogManager.StartGuideDialog(guest.GetProfession());
    }

    public void UpdateDate()
    {
        DataController.Instance.gameData.UpdateDate();    // ��¥ ����
    }

    public void SendIdentity(bool _decision)
    {
        decision = _decision;   // ����/���� ���� ����
        Destroy(identity);      // ��� �ſ��� ������Ʈ ����
        identity = null;        // ��������� null ����
        Destroy(stampArea.transform.GetChild(0).gameObject);   // ���� ������Ʈ ����

        CheckComplete();
    }

    public void SendTierSeal()
    {
        Destroy(tierSeal);      // ��� �ſ��� ������Ʈ ����
        tierSeal = null;        // ��������� null ����

        CheckComplete();
    }

    // �ſ���, ��ǥ ��� �����ߴ��� Ȯ��
    private void CheckComplete()
    {
        if (identity == null && tierSeal == null)     // �ſ���, ��ǥ ������Ʈ ��� ����
            StartCoroutine("DecisionComplete");
    }

    private IEnumerator DecisionComplete()
    {
        if (decision)   // ������ ���
        {
            if (correct)
            {
                HospitalityScore.Instance.correctAnswer++;
            }
            else
            {
                HospitalityScore.Instance.wrongAnswer++;
            }
            yield return dialogManager.StartCoroutine(dialogManager.PermissionDialogCoroutine());   // ���� ��ȭ ���
        }
        else    // ������ ���
        {
            yield return dialogManager.StartCoroutine(dialogManager.RefuseDialogCoroutine());   // ���� ��ȭ ���
        }

        // dialogManager�� �ڷ�ƾ�� ����Ǹ� ȣ��
        VisitGuest();
    }
}
