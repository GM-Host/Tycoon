using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public bool isRunning;  // 대화 진행 중인지 여부

    [SerializeField]
    private float spawnTime, deleteTime;    // 말풍선이 출력되는 간격, 대화가 끝난 후 말풍선 오브젝트 삭제까지의 시간
    [SerializeField]
    private Vector2 spawnPos, spawnPos2;    // 말풍선 위치

    // 필요한 컴포넌트
    [SerializeField]
    private GameObject guestBubblePrefab, myBubblePrefab, permitBubblePrefab, refuseBubblePrefab;   // 말풍선 프리팹
    [SerializeField]
    private GameObject parent;  // paper 오브젝트의 부모 오브젝트(캔버스)

    // code : 0->안내, 1->승인, 2->거절
    // code에 따른 대화 출력
    public IEnumerator GuestDialogueCoroutine(GuestDB.ProfessionType _profession, int code)
    {
        string profession = _profession.ToString();
        TalkData[] talkDatas = DialogueParse.GetDialogue(profession + code.ToString());  // 대화 데이터 가져오기
        List<GameObject> speechBubbleList = new List<GameObject>();     // 생성할 말풍선 오브젝트를 담을 리스트 (추후 오브젝트 삭제를 위함)

        // '나'의 말풍선 프리팹 설정
        GameObject bubblePrefab = null;
        switch (code)
        {
            case 0:
                bubblePrefab = myBubblePrefab;
                break;
            case 1:
                bubblePrefab = permitBubblePrefab;
                break;
            case 2:
                bubblePrefab = refuseBubblePrefab;
                break;
            default:
                break;
        }

        foreach (var talkData in talkDatas)
        {
            GameObject speechBubble;    // 생성할 말풍선 오브젝트

            foreach (var context in talkData.contexts)
            {
                // 대화자에 따라 다른 말풍선 출력
                if (talkData.name != "나") // 대화자 : 모험가
                {
                    speechBubble = Instantiate(guestBubblePrefab, Vector2.zero, Quaternion.identity);
                    speechBubble.transform.SetParent(parent.transform);
                    speechBubble.transform.localPosition = spawnPos;
                }
                else    // 대화자 : 나
                {
                    speechBubble = Instantiate(bubblePrefab, Vector2.zero, Quaternion.identity);
                    speechBubble.transform.SetParent(parent.transform);
                    speechBubble.transform.localPosition = spawnPos2;
                }

                speechBubble.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = context; // 텍스트 지정
                speechBubbleList.Add(speechBubble); // 말풍선 관리를 위해 리스트에 추가
            }
            yield return new WaitForSeconds(spawnTime);
        }

        StartCoroutine(DeleteSpeechBubble(speechBubbleList));
    }

    // 말풍선 오브젝트 삭제
    private IEnumerator DeleteSpeechBubble(List<GameObject> speechBubbleList)
    {
        yield return new WaitForSeconds(deleteTime);

        foreach (var bubble in speechBubbleList)
            Destroy(bubble);
    }
}
