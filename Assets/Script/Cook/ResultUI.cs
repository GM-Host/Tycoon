using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private Image resultFood;
    [SerializeField] private GameObject resultScene;
    [SerializeField] private Image cutScene;
    [SerializeField] private GameObject resultUI;
    [SerializeField] private Image food;
    public void ShowResult(string result, string [] processes, int count, string str_food)
    {
        if(result == "Success")
            {
                // 컷씬
                resultScene.SetActive(true);
                StartCoroutine(CookProcess(processes, count));
                // 결과창
                resultUI.SetActive(true);
                food.sprite = Resources.Load("Cook/result/food"+str_food, typeof(Sprite)) as Sprite;
            }

        else
            print("FAILED IMAGE TURN");
    }

    private IEnumerator CookProcess(string [] processes, int count)
    {
        for (int i=0; i<count; i++)
        {  
            cutScene.sprite = Resources.Load("Cook/result/" + processes[i]+"1", typeof(Sprite)) as Sprite;
            yield return new WaitForSeconds(3f);
        }
    }
}
