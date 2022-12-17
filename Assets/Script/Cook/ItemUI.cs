using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public CookDataManager.CookInventory item;
    public bool isDroppedOnCook = false;
    /********************
        # 8 -> # CDM
    ********************/
    public void ClickedItem()
    {
        // CookDataManager.Instance.ItemSelected(item);
        CookDataManager.Instance.SendFlavorData(int.Parse(Regex.Replace(item.imgId, @"\D", "")));
    }

    /// 현재 오브젝트를 드래그하기 시작할 때 1회 호출
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그 직전 현재 위치 저장
        original = this.transform;
        // 드래그 중인 아이템 정보 갱신
        CookDataManager.Instance.DraggingItem(item);
    }

    /// 현재 오브젝트를 드래그 중일 때 매 프레임 호출
    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }

    /// 현재 오브젝트의 드래그를 종료할 때 1회 호출
    public void OnEndDrag(PointerEventData eventData)
    {
        // cook 연산에 제대로 드롭됐을 때
        if(isDroppedOnCook)
        {
            
        }
    }


    private Transform original;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
