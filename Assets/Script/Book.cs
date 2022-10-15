using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    private int curPage = 0;
    private GameObject leftObj = null, rightObj = null;


    [SerializeField]
    private List<Sprite> backgrounds = new List<Sprite>();  // 각 페이지의 배경 이미지
    [SerializeField]
    private Image LeftBackground, RightBackground;


    // Start is called before the first frame update
    void Awake()
    {
        // J : 버튼 리스너 설정
        LeftBackground.gameObject.GetComponentInChildren<Button>().onClick.AddListener(delegate { TurnOver(false); });
        RightBackground.gameObject.GetComponentInChildren<Button>().onClick.AddListener(delegate { TurnOver(true); });

        SetPage();
    }

    // J : 책 넘기기 시도
    public void TurnOver(bool right)
    {
        Debug.Log("책 넘기기 시도");

        int offset = right ? 2 : -2;
        int nextPage = curPage + offset;

        // J : 책 넘기기 가능
        if (nextPage >= 0 && nextPage < backgrounds.Count)
        {
            curPage = nextPage;
            SetPage();
        }
    }

    private void SetPage()
    {
        // J : 배경 설정
        if (backgrounds[curPage] != null)
            LeftBackground.sprite = backgrounds[curPage];
        if (backgrounds[curPage + 1] != null)
            RightBackground.sprite = backgrounds[curPage + 1];

        // J : 기존 컨텐츠 삭제
        if (leftObj != null)
            Destroy(leftObj);
        if (rightObj != null)
            Destroy (rightObj);

        // J : 컨텐츠 설정
        leftObj = Resources.Load<GameObject>("Book/Page" + curPage.ToString());
        rightObj = Resources.Load<GameObject>("Book/Page" + (curPage + 1).ToString());

        if (leftObj != null)
            leftObj = SpawnContents(leftObj, LeftBackground);
        if (rightObj != null)
            rightObj = SpawnContents(rightObj, RightBackground);
    }

    // J : 컨텐츠 오브젝트 스폰, 생성한 컨텐츠 오브젝트 리턴
    private GameObject SpawnContents(GameObject obj, Image background)
    {
        GameObject createObj = Instantiate(obj, Vector2.zero, Quaternion.identity);
        createObj.transform.parent = background.transform;
        createObj.transform.localPosition = Vector2.zero;

        return createObj;
    }
}
