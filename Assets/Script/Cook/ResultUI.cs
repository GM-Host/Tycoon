using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    public GameObject result1, result2, result3;
    public void ShowResult(string result, string [] processes, int count)
    {
        if(result == "Success")
            switch(count)
            {
                case 1:
                result1.SetActive(true);
                result1.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load("Cook/result/" + processes[0]+"1", typeof(Sprite)) as Sprite;

                break;

                case 2:
                
                result2.SetActive(true);
                result2.transform.Find("Image_1").GetComponent<Image>().sprite = Resources.Load("Cook/result/" + processes[0]+"2", typeof(Sprite)) as Sprite;
                result2.transform.Find("Image_2").GetComponent<Image>().sprite = Resources.Load("Cook/result/" + processes[1]+"2", typeof(Sprite)) as Sprite;
                break;
                
                case 3:
                
                result3.SetActive(true);
                result3.transform.Find("Image_1").GetComponent<Image>().sprite = Resources.Load("Cook/result/" + processes[0]+"3", typeof(Sprite)) as Sprite;
                result3.transform.Find("Image_2").GetComponent<Image>().sprite = Resources.Load("Cook/result/" + processes[1]+"3", typeof(Sprite)) as Sprite;
                result3.transform.Find("Image_3").GetComponent<Image>().sprite = Resources.Load("Cook/result/" + processes[2]+"3", typeof(Sprite)) as Sprite;
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
