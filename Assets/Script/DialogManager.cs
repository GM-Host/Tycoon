using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField]
    private DialogData dialogData;
    [SerializeField]
    private TextMeshProUGUI dialogText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DialogCoroutine");
    }

    IEnumerator DialogCoroutine()
    {
        int index = 0;
        Tuple<string, string> tuple;
        while (true)
        {
            tuple = dialogData.GetDialogData(1001, index);
            if (tuple.Item1 == null)    // 대화의 끝
            {
                break;
            }
            else
            {
                dialogText.text = tuple.Item2;
                index++;
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}
