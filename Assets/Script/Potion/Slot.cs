using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item; // J : 아이템 정보
    public int itemCount; // J : 아이템 개수
    private Image itemImage;  // J : 아이템 이미지

    [SerializeField] private GameObject itemImg;
    [SerializeField] private TextMeshProUGUI countText;

    private void Awake()
    {
        itemImage = itemImg.GetComponent<Image>();
    }


    // 인벤토리에 새로운 아이템 슬롯 추가
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImg.SetActive(true);
        itemImage.sprite = item.itemImage;
        countText.text = itemCount.ToString();
    }

    // J : 이미 가지고 있는 아이템을 획득 or 사용한 경우
    // J : 아이템 개수 업데이트
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        countText.text = itemCount.ToString();

        // 아이템을 모두 사용한 경우
        if (itemCount <= 0)
            ClearSlot();
    }

    // J : 해당 슬롯에 있던 아이템을 모두 사용한 경우
    // J : 슬롯 초기화
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImg.SetActive(false);
        itemImage.sprite = null;
        countText.text = "0";
    }
}
