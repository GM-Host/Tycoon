using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

// J : 포션 제조 씬에서 아이템 도구로 드래그 시 떨어지는 오브젝트(DropItem)에 부착하는 스크립트
public class DropItem : MonoBehaviour
{
    static public DropItem instance;

    private float gravity = 980f;   // J : 중력가속도

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void PotionDrop(float x, Vector2 moveRange)
    {
        Slot slot = DragSlot.instance.dragSlot;

        GetComponent<Image>().sprite = slot.item.itemImage; // J : 드래그한 아이템의 이미지 세팅
        slot.SetSlotCount(-1);    // J : 재료 1개 소비

        StartCoroutine(MoveCoroutine(x, moveRange));
    }

    // 요리 씬에서 드롭 시 인벤토리 업데이트
    public void CookDrop()
    {
        Slot slot = DragSlot.instance.dragSlot;

        GetComponent<Image>().sprite = slot.item.itemImage; // J : 드래그한 아이템의 이미지 세팅
        slot.SetSlotCount(-1);    // J : 재료 1개 소비

        print("slot.itemImgName : "+slot.item.itemImage.name);
        CookDataManager.Instance.draggingItem = slot.item.itemImage.name;
        
        print("draggingItem : "+CookDataManager.Instance.draggingItem);
        // 내 생각엔 인벤토리 DB는 안바뀌는 것 같다. 이거를 매번 바꿀지 아니면 완성이나 Clean/Delete 후에 한꺼번에 바꿀지...
    }

    // J : 오브젝트가 아래로 떨어짐
    private IEnumerator MoveCoroutine(float x, Vector2 moveRange)
    {
        float velocity = 500f;  // J : 초기 속도
        transform.position = new Vector2(x, moveRange.x);   // J : 떨어지기 시작하는 위치
        Vector3 pos = transform.position;

        while (pos.y > moveRange.y) // J : moveRange.y까지 떨어지도록 반복
        {
            pos = transform.position;
            velocity += gravity * Time.deltaTime;

            pos.y -= velocity * Time.deltaTime;
            transform.position = pos;

            yield return null;
        }
    }
}
