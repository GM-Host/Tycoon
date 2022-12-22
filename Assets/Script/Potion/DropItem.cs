using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

// J : ���� ���� ������ ������ ������ �巡�� �� �������� ������Ʈ(DropItem)�� �����ϴ� ��ũ��Ʈ
public class DropItem : MonoBehaviour
{
    static public DropItem instance;

    private float gravity = 980f;   // J : �߷°��ӵ�

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void Drop(float x, Vector2 moveRange)
    {
        Slot slot = DragSlot.instance.dragSlot;

        GetComponent<Image>().sprite = slot.item.itemImage; // J : �巡���� �������� �̹��� ����
        slot.SetSlotCount(-1);    // J : ��� 1�� �Һ�

        StartCoroutine(MoveCoroutine(x, moveRange));
    }

    // J : ������Ʈ�� �Ʒ��� ������
    private IEnumerator MoveCoroutine(float x, Vector2 moveRange)
    {
        float velocity = 500f;  // J : �ʱ� �ӵ�
        transform.position = new Vector2(x, moveRange.x);   // J : �������� �����ϴ� ��ġ
        Vector3 pos = transform.position;

        while (pos.y > moveRange.y) // J : moveRange.y���� ���������� �ݺ�
        {
            pos = transform.position;
            velocity += gravity * Time.deltaTime;

            pos.y -= velocity * Time.deltaTime;
            transform.position = pos;

            yield return null;
        }
    }
}
