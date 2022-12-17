using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropCookItem : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject category;
    // 해당 슬롯에 무언가가 마우스 드롭 됐을 때 발생하는 이벤트
    public void OnDrop(PointerEventData eventData)
    {
        print("onDrop");
        // 현재 드래그 대상이 있다면
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<ItemUI>().isDroppedOnCook = true;
            CookDataManager.CookObject operation = new CookDataManager.CookObject();
            operation.id = category.name;
            CookDataManager.Instance.OperSelected(operation);
            CookDataManager.Instance.ItemSelected(CookDataManager.Instance.draggingItem);
        }

        
    }

    /// <summary>
	/// 마우스 포인트가 현재 아이템 슬롯 영역 내부로 들어갈 때 1회 호출
	/// </summary>
	public void OnPointerEnter(PointerEventData eventData)
	{

	}

	/// <summary>
	/// 마우스 포인트가 현재 아이템 슬롯 영역을 빠져나갈 때 1회 호출
	/// </summary>
	public void OnPointerExit(PointerEventData eventData)
	{
		
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
