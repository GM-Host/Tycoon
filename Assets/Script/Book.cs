using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    private int curPage = 0;
    private GameObject leftObj = null, rightObj = null;


    [SerializeField]
    private List<Sprite> backgrounds = new List<Sprite>();  // �� �������� ��� �̹���
    [SerializeField]
    private Image LeftBackground, RightBackground;


    // Start is called before the first frame update
    void Awake()
    {
        // J : ��ư ������ ����
        LeftBackground.gameObject.GetComponentInChildren<Button>().onClick.AddListener(delegate { TurnOver(false); });
        RightBackground.gameObject.GetComponentInChildren<Button>().onClick.AddListener(delegate { TurnOver(true); });

        SetPage();
    }

    // J : å �ѱ�� �õ�
    public void TurnOver(bool right)
    {
        Debug.Log("å �ѱ�� �õ�");

        int offset = right ? 2 : -2;
        int nextPage = curPage + offset;

        // J : å �ѱ�� ����
        if (nextPage >= 0 && nextPage < backgrounds.Count)
        {
            curPage = nextPage;
            SetPage();
        }
    }

    private void SetPage()
    {
        // J : ��� ����
        if (backgrounds[curPage] != null)
            LeftBackground.sprite = backgrounds[curPage];
        if (backgrounds[curPage + 1] != null)
            RightBackground.sprite = backgrounds[curPage + 1];

        // J : ���� ������ ����
        if (leftObj != null)
            Destroy(leftObj);
        if (rightObj != null)
            Destroy (rightObj);

        // J : ������ ����
        leftObj = Resources.Load<GameObject>("Book/Page" + curPage.ToString());
        rightObj = Resources.Load<GameObject>("Book/Page" + (curPage + 1).ToString());

        if (leftObj != null)
            leftObj = SpawnContents(leftObj, LeftBackground);
        if (rightObj != null)
            rightObj = SpawnContents(rightObj, RightBackground);
    }

    // J : ������ ������Ʈ ����, ������ ������ ������Ʈ ����
    private GameObject SpawnContents(GameObject obj, Image background)
    {
        GameObject createObj = Instantiate(obj, Vector2.zero, Quaternion.identity);
        createObj.transform.parent = background.transform;
        createObj.transform.localPosition = Vector2.zero;

        return createObj;
    }
}
