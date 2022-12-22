using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Slot[] slots;  // J : 슬롯 배열

    [SerializeField] private GameObject Grid;  // J : Slot들의 부모


    void Awake()
    {
        slots = Grid.GetComponentsInChildren<Slot>();
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.itemName == _item.itemName)   // J : 이미 인벤토리에 있는 아이템
                {
                    slots[i].SetSlotCount(_count);  // J : 개수 업데이트
                    return;
                }
            }
        }

        // J : 인벤토리에 없던 아이템이므로 가장 앞의 빈칸에 추가
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }
}
