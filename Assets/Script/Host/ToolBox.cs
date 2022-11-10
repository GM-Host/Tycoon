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
        // J : 도구 상자 내 모든 도구 인큐
        foreach (Transform tool in tools)
            toolQueue.Enqueue(tool.gameObject);

        toolQueue.Peek().SetActive(true);   // J : 첫번째 오브젝트 활성화
    }

    // J : 도구상자 손잡이 클릭
    public void ClickDoorHandle()
    {
        // J : 상자 뚜껑 여닫기
        doorAnimator.SetTrigger("Change");
    }

    // J : 도구 변경 버튼 클릭
    public void ClickArrowBtn()
    {
        // J : 현재 오브젝트 비활성화
        GameObject curObj = toolQueue.Dequeue();
        curObj.SetActive(false);
        toolQueue.Enqueue(curObj);

        // J : 다음 오브젝트 활성화
        curObj = toolQueue.Peek();
        curObj.SetActive(true);

        // J : 원판 회전
        wheelAnimator.SetTrigger("Rotate");
    }
}
