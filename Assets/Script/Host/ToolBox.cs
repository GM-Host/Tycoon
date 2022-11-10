using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBox : MonoBehaviour
{

    private Queue<GameObject> toolQueue = new Queue<GameObject>();

    [SerializeField]
    private Transform tools;
    [SerializeField]
    private Animator doorAnimator;
    [SerializeField]
    private Animator wheelAnimator;


    // Start is called before the first frame update
    void Start()
    {
        // J : ���� ���� �� ��� ���� ��ť
        foreach (Transform tool in tools)
            toolQueue.Enqueue(tool.gameObject);

        toolQueue.Peek().SetActive(true);   // J : ù��° ������Ʈ Ȱ��ȭ
    }

    // J : �������� ������ Ŭ��
    public void ClickDoorHandle()
    {
        // J : ���� �Ѳ� ���ݱ�
        doorAnimator.SetTrigger("Change");
    }

    // J : ���� ���� ��ư Ŭ��
    public void ClickArrowBtn()
    {
        // J : ���� ������Ʈ ��Ȱ��ȭ
        GameObject curObj = toolQueue.Dequeue();
        curObj.SetActive(false);
        toolQueue.Enqueue(curObj);

        // J : ���� ������Ʈ Ȱ��ȭ
        curObj = toolQueue.Peek();
        curObj.SetActive(true);

        // J : ���� ȸ��
        wheelAnimator.SetTrigger("Rotate");
    }
}
