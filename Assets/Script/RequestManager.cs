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
    public TextMeshProUGUI scoreText, guestNameText, guestLocalText, guestPartyText, guestSpeciesText, guestProfessionText, guestTierText;

    [SerializeField]
    private Vector2 spawnPaperPos;
    

    private GameObject identity;

    private float trueRatio = 0.6f;

    // 필요한 컴포넌트
    [SerializeField]
    private GuestDB guestDB;
    [SerializeField]
    private GameObject closeUpIdentity;
    [SerializeField]
    private GameObject stampArea;   // 인장 삭제를 위한 부모 오브젝트
    [SerializeField]
    private GameObject parent;  // paper 오브젝트의 부모 오브젝트(캔버스)
    [SerializeField]
    private Image guestProfessionSeal, guestTierSeal;     // 직업 인장 이미지, 티어 증표 이미지
    [SerializeField]
    private GameObject identityPrefab;  // 신분증 프리팹
    [SerializeField]
    private DialogManager dialogManager;    // 대화 출력을 위한 DialogManager 오브젝트

    // Start is called before the first frame update
    void Start()
    {
        UpdateDate();   // 날짜 증가
        UpdateScore(0);
        VisitGuest();
    }

    public void VisitGuest()
    {
        // 신원서 오브젝트 스폰
        identity = Instantiate(identityPrefab, Vector2.zero, Quaternion.identity);
        identity.transform.SetParent(parent.transform);
        identity.transform.localPosition = spawnPaperPos;
        identity.transform.SetSiblingIndex(2); // 3번째로 렌더링 (background, character보다 먼저)
        
        // 신원서 데이터 생성
        correct = new System.Random(System.Guid.NewGuid().GetHashCode()).NextDouble() < trueRatio ? true : false;
        guest = guestDB.CreateGuest(correct);
        
        // 신원서(클로즈업)에 표시
        guestNameText.text = guest.GetName();
        guestLocalText.text = guest.GetLocal();
        guestPartyText.text = guest.GetParty();
        guestSpeciesText.text = guestDB.GetSpeciesText(guest.GetSpecies());
        guestProfessionText.text = guestDB.GetProfessiosText(guest.GetProfession());
        guestProfessionSeal.sprite = guest.GetProfessionSeal();
        guestTierText.text=guest.GetTier().ToString();
        guestTierSeal.sprite = guest.GetTierSeal();


        // 안내 대화 출력
        dialogManager.StartGuideDialog(guest.GetProfession());
    }

    public void UpdateDate()
    {
        DataController.Instance.gameData.date++;    // 날짜 증가
    }

    public void UpdateScore(int num)
    {
        score += num;
        scoreText.text = score.ToString();
    }

    public IEnumerator DecisionComplete(bool decision)
    {
        Destroy(identity);  // 축소 신원서 삭제
        Destroy(stampArea.transform.GetChild(0).gameObject);   // 인장 삭제
        closeUpIdentity.SetActive(false);

        if (decision)   // 승인한 경우
        {
            if (correct)
            {
                UpdateScore(1);
                HospitalityScore.Instance.correctAnswer++;
            }
            else
            {
                UpdateScore(-1);
                HospitalityScore.Instance.wrongAnswer++;
            }
            yield return dialogManager.StartCoroutine(dialogManager.PermissionDialogCoroutine());   // 승인 대화 출력
        }
        else    // 거절한 경우
        {
            yield return dialogManager.StartCoroutine(dialogManager.RefuseDialogCoroutine());   // 거절 대화 출력
        }

        // dialogManager의 코루틴이 종료되면 호출
        VisitGuest();
    }
}
