using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// J : ���� ���� ������ ����(����, ���̱�, ����)�� �����ϴ� ��ũ��Ʈ
public class PotionTool : MonoBehaviour, IDropHandler
{
    [SerializeField] private Vector2 moveRange; // J : (�������� �������� �����ϴ� ��ġ, ���ߴ� ��ġ) <-y�� ����
    [SerializeField] private Animator animator;

    // �� ���Կ� ���� ���콺 ���
    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
        {
            Debug.Log(DragSlot.instance.dragSlot.item.name + " ���!");

            DropItem.instance.Drop(transform.position.x, moveRange);
            animator.SetTrigger("Work");
        }
    }
}
