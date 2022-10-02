using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public bool isRunning;  // ��ȭ ���� ������ ����

    [SerializeField]
    private float spawnTime, deleteTime;    // ��ǳ���� ��µǴ� ����, ��ȭ�� ���� �� ��ǳ�� ������Ʈ ���������� �ð�
    [SerializeField]
    private Vector2 spawnPos, spawnPos2;    // ��ǳ�� ��ġ

    // �ʿ��� ������Ʈ
    [SerializeField]
    private GameObject guestBubblePrefab, myBubblePrefab, permitBubblePrefab, refuseBubblePrefab;   // ��ǳ�� ������
    [SerializeField]
    private GameObject parent;  // paper ������Ʈ�� �θ� ������Ʈ(ĵ����)

    // code : 0->�ȳ�, 1->����, 2->����
    // code�� ���� ��ȭ ���
    public IEnumerator GuestDialogueCoroutine(GuestDB.ProfessionType _profession, int code)
    {
        string profession = _profession.ToString();
        TalkData[] talkDatas = DialogueParse.GetDialogue(profession + code.ToString());  // ��ȭ ������ ��������
        List<GameObject> speechBubbleList = new List<GameObject>();     // ������ ��ǳ�� ������Ʈ�� ���� ����Ʈ (���� ������Ʈ ������ ����)

        // '��'�� ��ǳ�� ������ ����
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
            GameObject speechBubble;    // ������ ��ǳ�� ������Ʈ

            foreach (var context in talkData.contexts)
            {
                // ��ȭ�ڿ� ���� �ٸ� ��ǳ�� ���
                if (talkData.name != "��") // ��ȭ�� : ���谡
                {
                    speechBubble = Instantiate(guestBubblePrefab, Vector2.zero, Quaternion.identity);
                    speechBubble.transform.SetParent(parent.transform);
                    speechBubble.transform.localPosition = spawnPos;
                }
                else    // ��ȭ�� : ��
                {
                    speechBubble = Instantiate(bubblePrefab, Vector2.zero, Quaternion.identity);
                    speechBubble.transform.SetParent(parent.transform);
                    speechBubble.transform.localPosition = spawnPos2;
                }

                speechBubble.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = context; // �ؽ�Ʈ ����
                speechBubbleList.Add(speechBubble); // ��ǳ�� ������ ���� ����Ʈ�� �߰�
            }
            yield return new WaitForSeconds(spawnTime);
        }

        StartCoroutine(DeleteSpeechBubble(speechBubbleList));
    }

    // ��ǳ�� ������Ʈ ����
    private IEnumerator DeleteSpeechBubble(List<GameObject> speechBubbleList)
    {
        yield return new WaitForSeconds(deleteTime);

        foreach (var bubble in speechBubbleList)
            Destroy(bubble);
    }
}
