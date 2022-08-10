using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RequestManager : MonoBehaviour
{
    private Guest guest;    // 생성한 모험가 정보
    private bool correct;   // 모험가 진위 여부
    private bool decision;  // 승인/거절 여부
    private GameObject identity, tierSeal;  // 스폰한 신원서, 증표 오브젝트

    private float trueRatio = 0.6f;

    [SerializeField]
    private Vector2 spawnIdentityPos, spawnTierSealPos;     // 신원서, 증표 오브젝트의 스폰 위치

    // 필요한 컴포넌트
    [SerializeField]
    private GuestDB guestDB;
    [SerializeField]
    private GameObject closeUpIdentity;    // 클로즈업 오브젝트 (신원서)
    [SerializeField]
    private GameObject stampArea;   // 인장 삭제를 위한 부모 오브젝트
    [SerializeField]
    private GameObject parent;  // paper 오브젝트의 부모 오브젝트(캔버스)
    [SerializeField]
    private Image guestProfessionSeal, guestTierSeal;     // 직업 인장 이미지, 티어 증표 이미지
    [SerializeField]
    private GameObject identityPrefab, tierSealPrefab;  // 신분증 프리팹, 증표 프리팹
    [SerializeField]
    private DialogManager dialogManager;    // 대화 출력을 위한 DialogManager 오브젝트
    [SerializeField]
    private TextMeshProUGUI guestNameText, guestLocalText, guestPartyText, guestSpeciesText, guestProfessionText, guestTierText;


    // Start is called before the first frame update
    void Start()
    {
        UpdateDate();   // 날짜 증가
        VisitGuest();
    }

    public void VisitGuest()
    {
        // 신원서 오브젝트 스폰
        identity = Instantiate(identityPrefab, Vector2.zero, Quaternion.identity);
        identity.transform.SetParent(parent.transform);
        identity.transform.localPosition = spawnIdentityPos;
        identity.transform.SetSiblingIndex(2); // 3번째로 렌더링 (background, character 이후)

        // 증표 오브젝트 스폰
        tierSeal = Instantiate(tierSealPrefab, Vector2.zero, Quaternion.identity);
        tierSeal.transform.SetParent(parent.transform);
        tierSeal.transform.localPosition = spawnTierSealPos;
        tierSeal.transform.SetSiblingIndex(3); // 4번째로 렌더링 (background, character 이후)

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

        // 증표 이미지 세팅
        guestTierSeal.sprite=guest.GetTierSeal();

        // 안내 대화 출력
        dialogManager.StartGuideDialog(guest.GetProfession());
    }

    public void UpdateDate()
    {
        DataController.Instance.gameData.UpdateDate();    // 날짜 증가
    }

    public void SendIdentity(bool _decision)
    {
        decision = _decision;   // 승인/거절 여부 저장
        Destroy(identity);      // 축소 신원서 오브젝트 삭제
        identity = null;        // 명시적으로 null 대입
        Destroy(stampArea.transform.GetChild(0).gameObject);   // 인장 오브젝트 삭제

        CheckComplete();
    }

    public void SendTierSeal()
    {
        Destroy(tierSeal);      // 축소 신원서 오브젝트 삭제
        tierSeal = null;        // 명시적으로 null 대입

        CheckComplete();
    }

    // 신원서, 증표 모두 전달했는지 확인
    private void CheckComplete()
    {
        if (identity == null && tierSeal == null)     // 신원서, 증표 오브젝트 모두 없음
            StartCoroutine("DecisionComplete");
    }

    private IEnumerator DecisionComplete()
    {
        if (decision)   // 승인한 경우
        {
            if (correct)
            {
                HospitalityScore.Instance.correctAnswer++;
            }
            else
            {
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
