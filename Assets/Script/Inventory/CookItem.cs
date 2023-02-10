using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/Cook item")]
public class CookItem : Item, IPointerEnterHandler, IPointerExitHandler
{
    public int itemId;
    public void OnPointerEnter(PointerEventData eventData)
    {
        CookDataManager.Instance.SendFlavorData(itemId);
        throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}