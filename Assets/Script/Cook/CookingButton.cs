using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CookingButton : MonoBehaviour, IDropHandler
{
    // 요리 종류
    [SerializeField] private GameObject category;
    // 해당 슬롯에 무언가가 마우스 드롭 됐을 때 발생하는 이벤트
    public void OnDrop(PointerEventData eventData)
    {
        print("onDrop");
        if(DragSlot.instance.dragSlot != null)
        {
            Debug.Log(DragSlot.instance.dragSlot.item.name + " 드롭!");

            DropItem.instance.CookDrop();
            //eventData.pointerDrag.GetComponent<ItemUI>().isDroppedOnCook = true;
            CookDataManager.CookObject operation = new CookDataManager.CookObject();
            operation.id = category.name;
            CookDataManager.Instance.OperSelected(operation);
            CookDataManager.Instance.ItemSelected(CookDataManager.Instance.draggingItem);
        }

        
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
