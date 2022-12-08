using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item; // J : ������ ����
    public int itemCount; // J : ������ ����
    private Image itemImage;  // J : ������ �̹���

    [SerializeField] private GameObject itemImg;
    [SerializeField] private TextMeshProUGUI countText;

    private void Awake()
    {
        itemImage = itemImg.GetComponent<Image>();
    }


    // �κ��丮�� ���ο� ������ ���� �߰�
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImg.SetActive(true);
        itemImage.sprite = item.itemImage;
        countText.text = itemCount.ToString();
    }

    // J : �̹� ������ �ִ� �������� ȹ�� or ����� ���
    // J : ������ ���� ������Ʈ
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        countText.text = itemCount.ToString();

        // �������� ��� ����� ���
        if (itemCount <= 0)
            ClearSlot();
    }

    // J : �ش� ���Կ� �ִ� �������� ��� ����� ���
    // J : ���� �ʱ�ȭ
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImg.SetActive(false);
        itemImage.sprite = null;
        countText.text = "0";
    }
}
