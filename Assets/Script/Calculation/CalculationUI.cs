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

    // Trigger if the parchment is clicked
    public void ClickedParchment()
    {
        StartCoroutine(ScaleUpAndMove(parchment));
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

    [Header("Parchment UI")]
    public RectTransform parchment;
    // 반대 코드 통합 필요
    private IEnumerator ScaleUpAndMove(RectTransform parchment)
    {
        bool isStart = true;
        bool isPosY = true;
        while(true)
        {
            if(isStart == true)
            {
                // Scale Up
                parchment.sizeDelta += 
                        new Vector2(speedOfCurtain * Time.deltaTime, speedOfCurtain * Time.deltaTime);
                // Roatate
                parchment.transform.Rotate(new Vector3(0,0,50f * Time.deltaTime));
                // Translate
                if(isPosY == true)
                {
                    parchment.anchoredPosition += 
                        new Vector2(0, speedOfCurtain * Time.deltaTime);

                }
                if(parchment.anchoredPosition.y > 0)    // Translate until
                {
                    isPosY = false;
                }

                // Scale up and rotate until
                if(parchment.sizeDelta.x > 500 && parchment.rotation.z > 0)
                {
                    isStart = false;
                    break;
                }
            }
            yield return null;
        }
    }
}
