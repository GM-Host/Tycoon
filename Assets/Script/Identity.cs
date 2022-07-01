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
    private float dragTime;     // 드래그한 시간
    private bool isCloseUp;   // 현재 확대 상태인지
    private bool sealing;   // 인장이 찍혔는지 여부
    private bool permit; // 승인 여부
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
            dragTime += Time.deltaTime; // 드래그 시간 업데이트
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
                Debug.Log("캐릭터");
                if (sealing)    // 인장이 찍힌 신원서인 경우
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
        if (dragTime == 0)  // 드래그 없이 클릭만 한 경우
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

    // 인장 찍기 (인장을 찍기 전이면 false, 이미 인장이 찍혔다면 true 리턴)
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