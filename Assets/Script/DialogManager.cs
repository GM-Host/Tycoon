using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private Vector2 spawnPos, spawnPos2;    // 말풍선 위치

    // 필요한 컴포넌트
    [SerializeField]
    private DialogData dialogData;
    [SerializeField]
    private GameObject speechBubblePrefab, speechBubblePrefab2;
    [SerializeField]
    private GameObject parent;  // paper 오브젝트의 부모 오브젝트(캔버스)

    private List<GameObject> speechBubbleList;  // 지금까지 출력한 말풍선 오브젝트 리스트

    private void Start()
    {
        speechBubbleList = new List<GameObject>();
    }
    public void StartGuideDialog(GuestDB.ProfessionType profession)
    {
        Debug.Log("StartGuideDialog");
        StartCoroutine("GuideDialogCoroutine", profession);
    }

    IEnumerator GuideDialogCoroutine(GuestDB.ProfessionType profession)
    {
        int id = 1000 + (int)profession;
        int index = 0;
        Tuple<string, string> tuple;    // 대화자, 대화내용

        while (true)
        {
            tuple = dialogData.GetDialogData(id, index);    // 대화 데이터 가져오기

            if (tuple.Item1 == null)    // 대화 종료
            {
                break;
            }
            else
            {
                GameObject speechBubble;

                // 대화자에 따라 다른 말풍선 출력
                if (tuple.Item1 != "나") // 대화자 : 모험가
                {
                    speechBubble = Instantiate(speechBubblePrefab, Vector2.zero, Quaternion.identity);
                    speechBubble.transform.SetParent(parent.transform);
                    speechBubble.transform.localPosition = spawnPos;
                }
                else    // 대화자 : 나
                {
                    speechBubble = Instantiate(speechBubblePrefab2, Vector2.zero, Quaternion.identity);
                    speechBubble.transform.SetParent(parent.transform);
                    speechBubble.transform.localPosition = spawnPos2;
                }

                speechBubble.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = tuple.Item2; // 텍스트 지정
                speechBubbleList.Add(speechBubble); // 말풍선 관리를 위해 리스트에 추가

                index++;
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
