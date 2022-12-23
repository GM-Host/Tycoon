using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    public Image resultFood;
    public Sprite successImg, failedImg;
    public void ShowResult(string result, int count)
    {
        if(result == "Success")
            resultFood.sprite = successImg;
        else
            resultFood.sprite = failedImg;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
