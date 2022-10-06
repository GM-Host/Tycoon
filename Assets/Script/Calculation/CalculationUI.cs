using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculationUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OpenCurtain(LeftCurtain, -1));
        StartCoroutine(OpenCurtain(RightCurtain, 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    [Header("Curtain UI")]
    public RectTransform LeftCurtain;
    public RectTransform RightCurtain;
    private float speedOfCurtain = 400f;
    private IEnumerator OpenCurtain(RectTransform curtain, int dir)
    {
        bool isStart = true;
        while(true)
        {
            if(isStart == true)
            {
                curtain.anchoredPosition += 
                        new Vector2(dir * speedOfCurtain * Time.deltaTime, 0);
                if(Mathf.Abs(curtain.anchoredPosition.x) > 380)
                {
                    isStart = false;
                    break;
                }
            }
            yield return null;
        }
    }
}
