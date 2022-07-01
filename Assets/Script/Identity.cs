using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Identity : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    [Range(0,1)]
    private float areaY;
    private float height, width;
    private float dragTime;     // �巡���� �ð�
    private bool isCloseUp;   // ���� Ȯ�� ��������
    private bool sealing;   // ������ �������� ����
    private bool permit; // ���� ����
    //private static Vector2 defaultPos;

    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;
    private GameObject canvas;

    void Start()
    {
        height = Screen.height * areaY;
        width = Screen.width;

        canvas = GameObject.Find("Canvas");
        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        pointerEventData = new PointerEventData(null);
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        //defaultPos = this.transform.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (!isCloseUp)
        {
            Vector2 currentPos = eventData.position;
            currentPos.x = Mathf.Clamp(currentPos.x, 0, width);
            currentPos.y = Mathf.Clamp(currentPos.y, 0, height);
            this.transform.position = currentPos;
            dragTime += Time.deltaTime; // �巡�� �ð� ������Ʈ
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (!isCloseUp)
        {
            Vector2 currentPos = eventData.position;
            currentPos.x = Mathf.Clamp(currentPos.x, 0, width);
            currentPos.y = Mathf.Clamp(currentPos.y, 0, height);
            this.transform.position = pointerEventData.position = currentPos;

            List<RaycastResult> results = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerEventData, results);

            if (results.Count > 1 && results[1].gameObject.name == "Character")
            {
                Debug.Log("ĳ����");
                if (sealing)    // ������ ���� �ſ����� ���
                {
                    Debug.Log("sealing");
                    GameObject.Find("RequestManager").GetComponent<RequestManager>().DecisionComplete(permit);
                }
            }
            else
            {
                for (int i = 0; i < results.Count; i++)
                {
                    Debug.Log(i+results[i].gameObject.name);
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