using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private GameObject result;
    [SerializeField] private Image resultImg;
    public void ShowResult(string result, string [] processes, int count)
    {
        if(result == "Success")
            StartCoroutine(CookProcess(processes, count));
        else
            print("FAILED IMAGE TURN");
    }

    private IEnumerator CookProcess(string [] processes, int count)
    {
        for (int i=0; i<count; i++)
        {
            result.SetActive(true);
            resultImg.sprite = Resources.Load("Cook/result/" + processes[i]+"1", typeof(Sprite)) as Sprite;
            yield return new WaitForSeconds(3f);
        }
    }
}
