using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    public GameObject result1, result2, result3;
    public Sprite stirfry1, boil1, fry1,
                stirfry2, boil2, fry2,
                stirfry3, boil3, fry3;
    public Sprite successImg, failedImg;
    public void ShowResult(string result, string [] processes, int count)
    {
        if(result == "Success")
            switch(count)
            {
                case 1:
                result1.SetActive(true);
                //result1.GetComponentsInChildren<Image>()[0].sprite = 

                break;
            }
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
