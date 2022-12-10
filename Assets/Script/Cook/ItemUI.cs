using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public CookDataManager.CookInventory item;
    /********************
        # 8 -> # CDM
    ********************/
    public void ClickedItem()
    {
        // CookDataManager.Instance.ItemSelected(item);
        CookDataManager.Instance.SendFlavorData(int.Parse(Regex.Replace(item.imgId, @"\D", "")));
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        CookDataManager.Instance.DraggingItem(item);
        CookDataManager.Instance.ok=false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // drop 되지 않았을 때, 원위치로 복귀
        if(!CookDataManager.Instance.ok)
        {
            this.transform.position = original.position;
        }
    }

    private Transform original;
    // Start is called before the first frame update
    void Start()
    {
        original = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
