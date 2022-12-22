using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PotionTool : MonoBehaviour, IDropHandler
{
    private float gravity = 980f;

    [SerializeField] private Vector2 track;

    [SerializeField] private GameObject DropItem;

    // 이 슬롯에 무언가 마우스 드롭
    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
        {
            Debug.Log(DragSlot.instance.dragSlot.item.name + " 드롭!");
            DropItem.GetComponent<Image>().sprite = DragSlot.instance.dragSlot.item.itemImage;
            DragSlot.instance.dragSlot.SetSlotCount(-1);    // J : 재료 1개 소비

            StartCoroutine(MoveCoroutine());
        }
    }

    private IEnumerator MoveCoroutine()
    {
        float mVelocity = 500f;
        DropItem.transform.position = new Vector2(transform.position.x, track.x);
        Debug.Log(transform.position.x);
        Debug.Log(DropItem.transform.position.x);
        Vector3 pos = DropItem.transform.position;

        while (pos.y > track.y)
        {
            pos = DropItem.transform.position;
            mVelocity += gravity * Time.deltaTime;

            pos.y -= mVelocity * Time.deltaTime;
            DropItem.transform.position = pos;

            yield return null;
        }
    }
}
