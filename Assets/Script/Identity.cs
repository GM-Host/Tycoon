using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Identity : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    [Range(0,1)]
    private float deskAreaY;
    private float deskHeight;
    private float dragTime;     // �巡���� �ð�
    private bool isCloseUp;   // ���� Ȯ�� ��������
    private bool sealing;   // ������ �������� ����
    private bool permit; // ���� ����
    private static Vector2 defaultPos;

    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;
    private GameObject canvas;

    void Start()
    {
        deskHeight = Screen.height * deskAreaY;

        canvas = GameObject.Find("Canvas");
        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        pointerEventData = new PointerEventData(null);
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        defaultPos = this.transform.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (!isCloseUp)
        {
            Vector2 currentPos = eventData.position;
            this.transform.position = currentPos;
            dragTime += Time.deltaTime; // �巡�� �ð� ������Ʈ
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (!isCloseUp)
        {
            Vector2 currentPos = eventData.position;
            this.transform.position = pointerEventData.position = currentPos;

            List<RaycastResult> results = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerEventData, results);

            if (currentPos.y > deskHeight)    // å�� �ܺ��� ���
            {
                if (results.Count > 1 && results[1].gameObject.name == "Character" && sealing)  // ������ ���� �ſ����� ĳ���Ϳ��� �ִ� ���
                {
                    RequestManager requestManager = GameObject.Find("RequestManager").GetComponent<RequestManager>();
                    requestManager.StartCoroutine(requestManager.DecisionComplete(permit));
                }
                else
                {
                    this.transform.position = defaultPos;   // ����ġ�� �̵�
                }
            }
        }
    }

    public void Click()
    {
        if (dragTime == 0)  // �巡�� ���� Ŭ���� �� ���
        {
            isCloseUp = true;
            canvas.transform.Find("CloseUpIdentity").gameObject.SetActive(true);
        }
        dragTime = 0;
    }

    public void SetPermit(bool _permit)
    {
        permit = _permit;
    }

    // ���� ��� (������ ��� ���̸� false, �̹� ������ �����ٸ� true ����)
    public bool Sealing()
    {
        if (!sealing)
        {
            sealing = !sealing;
            return !sealing;
        }
        return sealing;
    }

    public void ReleaseCloseUp()
    {
        isCloseUp = false;
    }
}