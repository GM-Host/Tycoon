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
    private Vector2 spawnPos, spawnPos2;    // ��ǳ�� ��ġ

    private int currentProfession;

    // �ʿ��� ������Ʈ
    [SerializeField]
    private DialogData dialogData;
    [SerializeField]
    private GameObject speechBubblePrefab, speechBubblePrefab2, permitSpeechBubble, refuseSpeechBubble;
    [SerializeField]
    private GameObject parent;  // paper ������Ʈ�� �θ� ������Ʈ(ĵ����)

    private List<GameObject> speechBubbleList;  // ���ݱ��� ����� ��ǳ�� ������Ʈ ����Ʈ

    private void Start()
    {
        speechBubbleList = new List<GameObject>();
    }

    public void StartGuideDialog(GuestDB.ProfessionType profession)
    {
        Debug.Log("StartGuideDialog");
        currentProfession = (int)profession;
        StartCoroutine("GuideDialogCoroutine");
    }

    public void StartPermissionDialog(GuestDB.ProfessionType profession)
    {
        Debug.Log("StartPermissionDialog");
        StartCoroutine("PermissionDialogCoroutine");
    }

    public void StartRefuseDialog(GuestDB.ProfessionType profession)
    {
        Debug.Log("StartRefuseDialog");
        StartCoroutine("RefuseDialogCoroutine");
    }

    IEnumerator GuideDialogCoroutine()
    {
        int id = currentProfession;
        int index = 0;
        Tuple<string, string> tuple;    // ��ȭ��, ��ȭ����

        while (true)
        {
            tuple = dialogData.GetGuideDialogData(id, index);    // ��ȭ ������ ��������

            if (tuple.Item1 == null)    // ��ȭ ����
                break;
            else
            {
                GameObject speechBubble;

                // ��ȭ�ڿ� ���� �ٸ� ��ǳ�� ���
                if (tuple.Item1 != "��") // ��ȭ�� : ���谡
                {
                    speechBubble = Instantiate(speechBubblePrefab, Vector2.zero, Quaternion.identity);
                    speechBubble.transform.SetParent(parent.transform);
                    speechBubble.transform.localPosition = spawnPos;
                }
                else    // ��ȭ�� : ��
                {
                    speechBubble = Instantiate(speechBubblePrefab2, Vector2.zero, Quaternion.identity);
                    speechBubble.transform.SetParent(parent.transform);
                    speechBubble.transform.localPosition = spawnPos2;
                }

                speechBubble.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = tuple.Item2; // �ؽ�Ʈ ����
                speechBubbleList.Add(speechBubble); // ��ǳ�� ������ ���� ����Ʈ�� �߰�

                index++;
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }

    IEnumerator PermissionDialogCoroutine()
    {
        int id = currentProfession;
        int index = 0;
        Tuple<string, string> tuple;    // ��ȭ��, ��ȭ����

        while (true)
        {
            tuple = dialogData.GetPermissionDialogData(id, index);    // ��ȭ ������ ��������

            if (tuple.Item1 == null)    // ��ȭ ����
                break;
            else
            {
                GameObject speechBubble;

                // ��ȭ�ڿ� ���� �ٸ� ��ǳ�� ���
                if (tuple.Item1 != "��") // ��ȭ�� : ���谡
                {
                    speechBubble = Instantiate(speechBubblePrefab, Vector2.zero, Quaternion.identity);
                    speechBubble.transform.SetParent(parent.transform);
                    speechBubble.transform.localPosition = spawnPos;
                }
                else    // ��ȭ�� : ��
                {
                    speechBubble = Instantiate(permitSpeechBubble, Vector2.zero, Quaternion.identity);
                    speechBubble.transform.SetParent(parent.transform);
                    speechBubble.transform.localPosition = spawnPos2;
                }

                speechBubble.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = tuple.Item2; // �ؽ�Ʈ ����
                speechBubbleList.Add(speechBubble); // ��ǳ�� ������ ���� ����Ʈ�� �߰�

                index++;
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }

    IEnumerator RefuseDialogCoroutine()
    {
        int id = currentProfession;
        int index = 0;
        Tuple<string, string> tuple;    // ��ȭ��, ��ȭ����

        while (true)
        {
            tuple = dialogData.GetRefuseDialogData(id, index);    // ��ȭ ������ ��������

            if (tuple.Item1 == null)    // ��ȭ ����
                break;
            else
            {
                GameObject speechBubble;

                // ��ȭ�ڿ� ���� �ٸ� ��ǳ�� ���
                if (tuple.Item1 != "��") // ��ȭ�� : ���谡
                {
                    speechBubble = Instantiate(speechBubblePrefab, Vector2.zero, Quaternion.identity);
                    speechBubble.transform.SetParent(parent.transform);
                    speechBubble.transform.localPosition = spawnPos;
                }
                else    // ��ȭ�� : ��
                {
                    speechBubble = Instantiate(refuseSpeechBubble, Vector2.zero, Quaternion.identity);
                    speechBubble.transform.SetParent(parent.transform);
                    speechBubble.transform.localPosition = spawnPos2;
                }

                speechBubble.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = tuple.Item2; // �ؽ�Ʈ ����
                speechBubbleList.Add(speechBubble); // ��ǳ�� ������ ���� ����Ʈ�� �߰�

                index++;
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
