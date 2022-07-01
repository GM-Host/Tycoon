using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Stamp : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private bool permit;    // 승인 도장인지, 거절 도장인지

    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;

    private static Vector2 defaultPos;  // 도장 기존 위치

    // 필요한 컴포넌트
    [SerializeField]
    private RequestManager requestManager;
    [SerializeField]
    private Canvas canvas;

    void Start()
    {
        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        pointerEventData = new PointerEventData(null);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        defaultPos = this.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 currentPos = eventData.position;
        this.transform.position = currentPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 currentPos = eventData.position;
        this.transform.position = pointerEventData.position = currentPos;

        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, results);

        if (results.Count > 1 && results[1].gameObject.name == "StampArea")
        {
            Debug.Log("스탬프 영역");
            requestManager.DecisionComplete(permit);
        }
        else
        {
            Debug.Log("null");
            this.transform.position = defaultPos;
        }
    }

    
}
