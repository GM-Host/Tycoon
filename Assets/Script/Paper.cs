using UnityEngine;
using UnityEngine.EventSystems;

public class Paper : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    [Range(0,1)]
    private float areaY;
    private float height, width;
    private float dragTime;     // 드래그한 시간
    private bool isExpansion;   // 현재 확대 상태인지
    //private static Vector2 defaultPos;

    void Start()
    {
        height = Screen.height * areaY;
        width = Screen.width;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        //defaultPos = this.transform.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (!isExpansion)
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
        if (!isExpansion)
        {
            Vector2 currentPos = eventData.position;
            currentPos.x = Mathf.Clamp(currentPos.x, 0, width);
            currentPos.y = Mathf.Clamp(currentPos.y, 0, height);
            this.transform.position = currentPos;
        }
    }

    public void Click()
    {
        if (dragTime == 0)  // 드래그 없이 클릭만 한 경우
        {
            isExpansion = true;
            GameObject.Find("Canvas").transform.Find("ExpansionPaper").gameObject.SetActive(true);
        }
        dragTime = 0;
    }
}